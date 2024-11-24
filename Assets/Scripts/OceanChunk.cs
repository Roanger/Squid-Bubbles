using UnityEngine;

public class OceanChunk : MonoBehaviour
{
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
        if (fishPrefabs == null || fishPrefabs.Length == 0) return;
        
        // Random position within chunk
        Vector3 position = GetRandomSpawnPosition(true);
        
        // Spawn random fish prefab
        GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        GameObject fish = Instantiate(fishPrefab, position, Quaternion.identity, marineLifeContainer);
        
        // Add or get MarineLife component
        MarineLife marineLife = fish.GetComponent<MarineLife>();
        if (marineLife == null)
        {
            marineLife = fish.AddComponent<MarineLife>();
        }
        
        // Set species data
        if (fishDatabase != null && fishDatabase.species != null && fishDatabase.species.Length > 0)
        {
            var speciesData = fishDatabase.species[Random.Range(0, fishDatabase.species.Length)];
            marineLife.speciesName = speciesData.speciesName;
        }
        else
        {
            Debug.LogWarning("Fish database is missing or empty!");
            marineLife.speciesName = "Unknown Fish";
        }
        
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
    
    private void SpawnDecoration()
    {
        // Randomly choose between coral, rock, or plant
        float random = Random.value;
        GameObject[] prefabArray;
        
        if (random < 0.4f && coralPrefabs != null && coralPrefabs.Length > 0)
        {
            prefabArray = coralPrefabs;
        }
        else if (random < 0.7f && rockPrefabs != null && rockPrefabs.Length > 0)
        {
            prefabArray = rockPrefabs;
        }
        else if (plantPrefabs != null && plantPrefabs.Length > 0)
        {
            prefabArray = plantPrefabs;
        }
        else
            return;
        
        // Random position within chunk
        Vector3 position = GetRandomSpawnPosition(false);
        
        // Spawn random decoration prefab
        GameObject prefab = prefabArray[Random.Range(0, prefabArray.Length)];
        GameObject decoration = Instantiate(prefab, position, Quaternion.identity, marineLifeContainer);
        
        // Add component to store species data
        var marineLife = decoration.GetComponent<MarineLife>();
        if (marineLife == null)
        {
            marineLife = decoration.AddComponent<MarineLife>();
        }
        
        // Set species data based on type
        if (prefabArray == coralPrefabs && coralDatabase != null)
        {
            var species = coralDatabase.species[Random.Range(0, coralDatabase.species.Length)];
            marineLife.speciesName = species.speciesName;
        }
        else if (prefabArray == plantPrefabs && plantDatabase != null)
        {
            var species = plantDatabase.species[Random.Range(0, plantDatabase.species.Length)];
            marineLife.speciesName = species.speciesName;
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
