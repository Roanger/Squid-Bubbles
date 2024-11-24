using UnityEngine;

public class MarineLife : MonoBehaviour
{
    public enum MovementPattern
    {
        Circular,    // Default circular pattern
        Pulse,       // For jellyfish
        Patrol,      // For sharks
        Hover,       // For seahorses
        Glide,       // For manta rays
        Drift        // For slow moving creatures like whales
    }

    [Header("Species Information")]
    public string speciesName;
    [SerializeField] private MarineSpeciesDatabase speciesDatabase;
    [SerializeField] private MarineLifePresets presets;
    
    [Header("Movement Settings")]
    [SerializeField] private MovementPattern movementPattern;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRadius = 3f;
    [SerializeField] private float pulseFrequency = 1f;
    [SerializeField] private float pulseAmplitude = 0.5f;
    [SerializeField] private Vector2 patrolPoints = new Vector2(5f, 5f);
    
    private Vector2 startPosition;
    private float angle;
    private float pulseTime;
    private Vector3 originalScale;
    private UIManager uiManager;
    private MarineSpeciesDatabase.SpeciesData speciesData;
    private int patrolDirection = 1;
    private Vector2 currentTarget;
    private SpriteRenderer spriteRenderer;
    private CameraEffectsManager cameraEffects;

    private void Start()
    {
        // Load database if not set
        if (speciesDatabase == null)
        {
            speciesDatabase = Resources.Load<MarineSpeciesDatabase>("MarineSpeciesDatabase");
            if (speciesDatabase == null)
            {
                Debug.LogError($"[MarineLife] Could not load MarineSpeciesDatabase from Resources!");
                return;
            }
        }

        // Load presets if not set
        if (presets == null)
        {
            presets = Resources.Load<MarineLifePresets>("MarineLifePresets");
        }

        // Set up sprite renderer sorting
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.sortingOrder = 0;
        }

        startPosition = transform.position;
        originalScale = transform.localScale;
        
        // Find species data
        MarineSpeciesDatabase.SpeciesData speciesData = null;
        
        if (string.IsNullOrEmpty(speciesName))
        {
            // If no species name set, pick a random one
            if (speciesDatabase.species != null && speciesDatabase.species.Length > 0)
            {
                var randomSpecies = speciesDatabase.species[Random.Range(0, speciesDatabase.species.Length)];
                speciesName = randomSpecies.speciesName;
                speciesData = randomSpecies;
            }
            else
            {
                Debug.LogError("[MarineLife] No species defined in database!");
                return;
            }
        }
        else
        {
            // Find the specific species
            foreach (var species in speciesDatabase.species)
            {
                if (species.speciesName == speciesName)
                {
                    speciesData = species;
                    break;
                }
            }
        }
        
        if (speciesData == null)
        {
            Debug.LogError($"[MarineLife] Species data not found for {speciesName}! Available species:");
            foreach (var species in speciesDatabase.species)
            {
                Debug.LogError($"  - {species.speciesName}");
            }
        }

        // Verify components
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"[MarineLife] No Collider2D found on {gameObject.name}!");
        }
        
        ConfigureMovementPattern();
        
        // Find UIManager in scene
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("[MarineLife] UIManager not found in scene! Make sure there is a GameObject with UIManager component.");
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Find CameraEffectsManager
        cameraEffects = FindObjectOfType<CameraEffectsManager>();
        if (cameraEffects == null)
        {
            Debug.LogError("[MarineLife] CameraEffectsManager not found in scene!");
        }
        
        // Set up collider if it doesn't exist
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 1f; // Adjust based on your sprite size
            col.isTrigger = true;
        }
        
        // Set up initial patrol target
        if (movementPattern == MovementPattern.Patrol)
        {
            currentTarget = startPosition + Vector2.right * patrolPoints.x * patrolDirection;
        }
    }

    private void ConfigureMovementPattern()
    {
        if (presets != null)
        {
            foreach (var preset in presets.presets)
            {
                if (preset.speciesName == speciesName)
                {
                    // Apply preset values
                    movementPattern = preset.movementPattern;
                    moveSpeed = preset.moveSpeed;
                    moveRadius = preset.moveRadius;
                    transform.localScale = preset.scale;
                    
                    // Apply color if we have a sprite renderer
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = preset.speciesColor;
                    }
                    
                    // Set specific values based on movement pattern
                    switch (movementPattern)
                    {
                        case MovementPattern.Pulse:
                            pulseFrequency = 1.5f;
                            pulseAmplitude = 0.2f;
                            break;
                        case MovementPattern.Patrol:
                            patrolPoints = new Vector2(8f, 0f);
                            break;
                    }
                    break;
                }
            }
        }
    }

    private void Update()
    {
        switch (movementPattern)
        {
            case MovementPattern.Circular:
                CircularMovement();
                break;
            case MovementPattern.Pulse:
                PulseMovement();
                break;
            case MovementPattern.Patrol:
                PatrolMovement();
                break;
            case MovementPattern.Hover:
                HoverMovement();
                break;
            case MovementPattern.Glide:
                GlideMovement();
                break;
            case MovementPattern.Drift:
                DriftMovement();
                break;
        }
    }

    private void CircularMovement()
    {
        angle += moveSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * moveRadius;
        
        transform.position = startPosition + offset;
        RotateTowardsMovement(offset.normalized);
    }

    private void PulseMovement()
    {
        // Combine gentle upward movement with pulsing
        pulseTime += Time.deltaTime;
        float pulse = Mathf.Sin(pulseTime * pulseFrequency) * pulseAmplitude;
        
        // Scale effect for pulsing
        transform.localScale = originalScale * (1 + pulse * 0.2f);
        
        // Gentle upward drift with sideways motion
        Vector2 drift = new Vector2(
            Mathf.Sin(pulseTime * 0.5f) * 0.5f,
            pulse + 0.5f
        ) * moveSpeed * Time.deltaTime;
        
        transform.position += (Vector3)drift;
        
        // Reset position if too far from start
        if (Vector2.Distance(transform.position, startPosition) > moveRadius)
        {
            transform.position = startPosition;
        }
    }

    private void PatrolMovement()
    {
        // Move towards current target
        Vector2 direction = (currentTarget - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        RotateTowardsMovement(direction);

        // Check if reached target
        if (Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            patrolDirection *= -1;
            currentTarget = startPosition + Vector2.right * patrolPoints.x * patrolDirection;
        }
    }

    private void HoverMovement()
    {
        // Small, gentle random movements
        Vector2 hover = new Vector2(
            Mathf.PerlinNoise(Time.time * 0.5f, 0) - 0.5f,
            Mathf.PerlinNoise(0, Time.time * 0.5f) - 0.5f
        ) * moveRadius;
        
        transform.position = startPosition + hover;
    }

    private void GlideMovement()
    {
        // Smooth, wave-like movement
        angle += moveSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(
            Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad) * 0.5f
        ) * moveRadius;
        
        transform.position = startPosition + offset;
        RotateTowardsMovement(offset.normalized);
    }

    private void DriftMovement()
    {
        // Slow, majestic movement
        angle += moveSpeed * 0.5f * Time.deltaTime;
        Vector2 offset = new Vector2(
            Mathf.Cos(angle * 0.3f * Mathf.Deg2Rad),
            Mathf.Sin(angle * 0.2f * Mathf.Deg2Rad)
        ) * moveRadius;
        
        transform.position = startPosition + offset;
        RotateTowardsMovement(offset.normalized);
    }

    private void RotateTowardsMovement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * 5f
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[MarineLife] Trigger entered by {other.gameObject.name}. Has Player tag: {other.CompareTag("Player")}");
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[MarineLife] Player collision detected with species: {speciesName}");
            bool isNewDiscovery = ShowRandomFact();
            Debug.Log($"[MarineLife] Is new discovery: {isNewDiscovery}");
            if (isNewDiscovery && cameraEffects != null)
            {
                cameraEffects.TriggerDiscoveryEffect(transform);
            }
        }
    }

    private bool ShowRandomFact()
    {
        if (uiManager != null && speciesData != null && speciesData.facts.Length > 0)
        {
            string randomFact = speciesData.facts[Random.Range(0, speciesData.facts.Length)];
            string formattedFact = $"{speciesName}\n\n{randomFact}\n\n<size=80%><i>{speciesData.scientificName}</i>\n{speciesData.habitat}</size>";
            return uiManager.ShowFact(formattedFact, speciesName);
        }
        return false;
    }
}
