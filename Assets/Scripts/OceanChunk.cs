using UnityEngine;

public class OceanChunk : MonoBehaviour
{
    [System.Serializable]
    public class PrefabCategory
    {
        public string speciesName;
        public GameObject prefab;
        public MarineLife.MovementPattern movementPattern;
    }

    [Header("Visuals")]
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite surfaceSprite;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material oceanFloorMaterial;
    
    [Header("Marine Life Databases")]
    [SerializeField] private MarineSpeciesDatabase fishDatabase;
    [SerializeField] private OceanPlantData plantDatabase;
    [SerializeField] private CoralData coralDatabase;
    
    [Header("Marine Life Spawning")]
    [SerializeField] private GameObject[] coralPrefabs;
    [SerializeField] private GameObject[] rockPrefabs;
    [SerializeField] private GameObject[] plantPrefabs;
    [SerializeField] private GameObject[] fishPrefabs;
    
    [Header("Prefab Categories")]
    [SerializeField] private PrefabCategory[] fishCategories;
    [SerializeField] private PrefabCategory[] coralCategories;
    [SerializeField] private PrefabCategory[] plantCategories;
    
    [Header("Spawn Settings")]
    [SerializeField] private int minFishPerChunk = 3;
    [SerializeField] private int maxFishPerChunk = 7;
    [SerializeField] private int minDecorationsPerChunk = 5;
    [SerializeField] private int maxDecorationsPerChunk = 10;
    [SerializeField] private float minDistanceBetweenSpawns = 3f;
    
    private Transform marineLifeContainer;
    private float chunkSize;
    private System.Collections.Generic.List<Vector2> spawnedPositions = new System.Collections.Generic.List<Vector2>();
    
    public void Initialize(float depth)
    {
        chunkSize = transform.localScale.x;
        
        // Create container for marine life
        marineLifeContainer = new GameObject("Marine Life").transform;
        marineLifeContainer.parent = transform;
        marineLifeContainer.localPosition = Vector3.zero;
        
        CreateWaterSurface();
        SpawnMarineLife();
    }
    
    private void CreateWaterSurface()
    {
        // Create background water layer
        GameObject waterBackground = new GameObject("WaterBackground");
        waterBackground.transform.SetParent(transform, false);
        
        SpriteRenderer waterRenderer = waterBackground.AddComponent<SpriteRenderer>();
        waterRenderer.sprite = waterSprite;
        waterRenderer.material = waterMaterial;
        waterRenderer.sortingLayerName = "Water Background";
        waterRenderer.sortingOrder = 0;
        
        // Scale to chunk size
        waterBackground.transform.localScale = Vector3.one;
        
        // Add Water2DEffect
        Water2DEffect backgroundEffect = waterBackground.AddComponent<Water2DEffect>();
        backgroundEffect.enabled = true;
        
        // Create surface water layer
        if (surfaceSprite != null)
        {
            GameObject waterSurface = new GameObject("WaterSurface");
            waterSurface.transform.SetParent(transform, false);
            
            SpriteRenderer surfaceRenderer = waterSurface.AddComponent<SpriteRenderer>();
            surfaceRenderer.sprite = surfaceSprite;
            surfaceRenderer.material = waterMaterial;
            surfaceRenderer.sortingLayerName = "Water Surface";
            surfaceRenderer.sortingOrder = 0;
            surfaceRenderer.color = new Color(1, 1, 1, 0.3f);
            
            waterSurface.transform.localScale = Vector3.one;
            
            Water2DEffect surfaceEffect = waterSurface.AddComponent<Water2DEffect>();
            surfaceEffect.enabled = true;
        }
    }
    
    private void SpawnMarineLife()
    {
        // Spawn fish
        int fishCount = Random.Range(minFishPerChunk, maxFishPerChunk + 1);
        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish();
        }
        
        // Spawn decorations (coral, rocks, plants)
        int decorationCount = Random.Range(minDecorationsPerChunk, maxDecorationsPerChunk + 1);
        for (int i = 0; i < decorationCount; i++)
        {
            SpawnDecoration();
        }
    }
    
    private void SpawnFish()
    {
        // Early exit if we don't have the required components
        if (fishDatabase == null || fishDatabase.species == null || 
            (fishCategories == null || fishCategories.Length == 0) && (fishPrefabs == null || fishPrefabs.Length == 0)) 
        {
            Debug.LogError("Missing required fish components in OceanChunk!");
            return;
        }
        
        // Random position within chunk
        Vector3 position = GetRandomSpawnPosition(true);
        
        // First pick a random species from the database
        MarineSpeciesDatabase.SpeciesData speciesData = fishDatabase.species[Random.Range(0, fishDatabase.species.Length)];
        
        // Find matching prefab for this species
        GameObject fishPrefab = null;
        
        // Try to find exact match first by species name
        if (fishCategories != null && fishCategories.Length > 0)
        {
            // Case insensitive name matching
            string targetName = speciesData.speciesName.ToLower().Trim();
            foreach (var category in fishCategories)
            {
                if (category.speciesName != null && category.speciesName.ToLower().Trim() == targetName)
                {
                    if (category.prefab != null)
                    {
                        fishPrefab = category.prefab;
                        break;
                    }
                }
            }

            // If no exact match, try to find a prefab with matching movement pattern
            if (fishPrefab == null)
            {
                MarineLife.MovementPattern desiredPattern = GetMovementPatternForSpecies(speciesData.speciesName);
                var matchingCategories = new System.Collections.Generic.List<PrefabCategory>();
                
                foreach (var category in fishCategories)
                {
                    if (category.prefab != null && category.movementPattern == desiredPattern)
                    {
                        matchingCategories.Add(category);
                    }
                }
                
                // Randomly select from matching categories
                if (matchingCategories.Count > 0)
                {
                    var selectedCategory = matchingCategories[Random.Range(0, matchingCategories.Count)];
                    fishPrefab = selectedCategory.prefab;
                }
            }
        }
        
        // If still no match found, use a random fish prefab as last resort
        if (fishPrefab == null && fishPrefabs != null && fishPrefabs.Length > 0)
        {
            // Filter out null prefabs
            var validPrefabs = new System.Collections.Generic.List<GameObject>();
            foreach (var prefab in fishPrefabs)
            {
                if (prefab != null) validPrefabs.Add(prefab);
            }
            
            if (validPrefabs.Count > 0)
            {
                fishPrefab = validPrefabs[Random.Range(0, validPrefabs.Count)];
                Debug.LogWarning($"No specific prefab found for {speciesData.speciesName}, using random fish prefab");
            }
            else
            {
                Debug.LogError("No valid fish prefabs found in the fishPrefabs array!");
                return;
            }
        }
        
        if (fishPrefab == null)
        {
            Debug.LogError($"Could not find any valid prefab for species {speciesData.speciesName}!");
            return;
        }
        
        // Spawn the fish
        GameObject fish = Instantiate(fishPrefab, position, Quaternion.identity, marineLifeContainer);
        
        // Add or get MarineLife component
        MarineLife marineLife = fish.GetComponent<MarineLife>();
        if (marineLife == null)
        {
            marineLife = fish.AddComponent<MarineLife>();
        }
        
        // Set the species name
        marineLife.speciesName = speciesData.speciesName;
        
        // Random scale variation
        float scaleVariation = Random.Range(0.8f, 1.2f);
        fish.transform.localScale *= scaleVariation;
        
        // Random flip direction
        if (Random.value > 0.5f)
        {
            Vector3 scale = fish.transform.localScale;
            scale.x *= -1;
            fish.transform.localScale = scale;
        }
    }

    private MarineLife.MovementPattern GetMovementPatternForSpecies(string speciesName)
    {
        // Default patterns based on species type
        speciesName = speciesName.ToLower();
        if (speciesName.Contains("jellyfish"))
            return MarineLife.MovementPattern.Pulse;
        if (speciesName.Contains("shark") || speciesName.Contains("barracuda"))
            return MarineLife.MovementPattern.Patrol;
        if (speciesName.Contains("seahorse"))
            return MarineLife.MovementPattern.Hover;
        if (speciesName.Contains("ray") || speciesName.Contains("manta"))
            return MarineLife.MovementPattern.Glide;
        if (speciesName.Contains("whale") || speciesName.Contains("turtle"))
            return MarineLife.MovementPattern.Drift;
            
        return MarineLife.MovementPattern.Circular;
    }
    
    private void SpawnDecoration()
    {
        // Randomly choose between coral, rock, or plant
        float random = Random.value;
        GameObject[] prefabArray = null;
        string speciesName = null;
        PrefabCategory[] categories = null;
        
        // Validate and select decoration type
        if (random < 0.4f && coralPrefabs != null && coralPrefabs.Length > 0 && coralCategories != null && coralCategories.Length > 0)
        {
            prefabArray = coralPrefabs;
            categories = coralCategories;
            if (coralDatabase != null && coralDatabase.species != null && coralDatabase.species.Length > 0)
            {
                var species = coralDatabase.species[Random.Range(0, coralDatabase.species.Length)];
                speciesName = species.speciesName;
            }
        }
        else if (random < 0.7f && rockPrefabs != null && rockPrefabs.Length > 0)
        {
            // Filter out null rock prefabs
            var validRocks = new System.Collections.Generic.List<GameObject>();
            foreach (var rock in rockPrefabs)
            {
                if (rock != null) validRocks.Add(rock);
            }
            
            if (validRocks.Count > 0)
            {
                prefabArray = validRocks.ToArray();
            }
        }
        else if (plantPrefabs != null && plantPrefabs.Length > 0 && plantCategories != null && plantCategories.Length > 0)
        {
            prefabArray = plantPrefabs;
            categories = plantCategories;
            if (plantDatabase != null && plantDatabase.species != null && plantDatabase.species.Length > 0)
            {
                var species = plantDatabase.species[Random.Range(0, plantDatabase.species.Length)];
                speciesName = species.speciesName;
            }
        }
        
        // If we couldn't set up a valid prefab array, return
        if (prefabArray == null || prefabArray.Length == 0)
        {
            Debug.LogWarning("No valid prefabs available for decoration spawning!");
            return;
        }
            
        // Random position within chunk
        Vector3 position = GetRandomSpawnPosition(false);
        
        // Find matching prefab if we have species data
        GameObject prefab = null;
        if (speciesName != null && categories != null && categories.Length > 0)
        {
            // Try to find exact match by name (case insensitive)
            string targetName = speciesName.ToLower().Trim();
            var matchingCategories = new System.Collections.Generic.List<PrefabCategory>();
            
            foreach (var category in categories)
            {
                if (category.prefab != null && 
                    category.speciesName != null && 
                    category.speciesName.ToLower().Trim() == targetName)
                {
                    matchingCategories.Add(category);
                }
            }
            
            // Randomly select from matching categories
            if (matchingCategories.Count > 0)
            {
                var selectedCategory = matchingCategories[Random.Range(0, matchingCategories.Count)];
                prefab = selectedCategory.prefab;
            }
        }
        
        // Fallback to random prefab if no match found
        if (prefab == null)
        {
            // Filter out null prefabs
            var validPrefabs = new System.Collections.Generic.List<GameObject>();
            foreach (var p in prefabArray)
            {
                if (p != null) validPrefabs.Add(p);
            }
            
            if (validPrefabs.Count > 0)
            {
                prefab = validPrefabs[Random.Range(0, validPrefabs.Count)];
                if (speciesName != null)
                {
                    Debug.LogWarning($"No specific prefab found for {speciesName}, using random prefab");
                }
            }
            else
            {
                Debug.LogError("No valid prefabs found in the prefab array!");
                return;
            }
        }
        
        // Spawn the decoration
        GameObject decoration = Instantiate(prefab, position, Quaternion.identity, marineLifeContainer);
        
        // Add MarineLife component if we have species data
        if (speciesName != null)
        {
            var marineLife = decoration.GetComponent<MarineLife>();
            if (marineLife == null)
            {
                marineLife = decoration.AddComponent<MarineLife>();
            }
            marineLife.speciesName = speciesName;
        }
        
        // Random scale and rotation variation
        float scaleVariation = Random.Range(0.8f, 1.5f);
        decoration.transform.localScale *= scaleVariation;
        
        // Random rotation for rocks
        if (prefabArray == rockPrefabs)
        {
            float randomRotation = Random.Range(0f, 360f);
            decoration.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
    }
    
    private Vector3 GetRandomSpawnPosition(bool isFish)
    {
        int maxAttempts = 30;
        float cellSize = minDistanceBetweenSpawns;
        
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Use grid-based positioning for more even distribution
            int gridX = Mathf.FloorToInt(chunkSize / cellSize);
            int gridY = Mathf.FloorToInt(chunkSize / cellSize);
            
            int randomGridX = Random.Range(0, gridX);
            int randomGridY = Random.Range(0, gridY);
            
            // Add some random offset within the cell
            float offsetX = Random.Range(-cellSize/2f, cellSize/2f);
            float offsetY = Random.Range(-cellSize/2f, cellSize/2f);
            
            float x = (randomGridX * cellSize) - (chunkSize/2f) + offsetX;
            float y = (randomGridY * cellSize) - (chunkSize/2f) + offsetY;
            
            Vector2 newPosition = new Vector2(x, y);
            
            // Check minimum distance from other spawns
            bool tooClose = false;
            foreach (Vector2 existingPos in spawnedPositions)
            {
                if (Vector2.Distance(newPosition, existingPos) < minDistanceBetweenSpawns)
                {
                    tooClose = true;
                    break;
                }
            }
            
            if (!tooClose)
            {
                spawnedPositions.Add(newPosition);
                return new Vector3(newPosition.x, newPosition.y, 0f) + transform.position;
            }
        }
        
        // If we couldn't find a valid position after max attempts, just return a random position
        float fallbackX = Random.Range(-chunkSize / 2f, chunkSize / 2f);
        float fallbackY = Random.Range(-chunkSize / 2f, chunkSize / 2f);
        
        Vector2 fallbackPos = new Vector2(fallbackX, fallbackY);
        spawnedPositions.Add(fallbackPos);
        return new Vector3(fallbackX, fallbackY, 0f) + transform.position;
    }
    
    private void OnDestroy()
    {
        if (marineLifeContainer != null)
        {
            Destroy(marineLifeContainer.gameObject);
        }
    }
}
