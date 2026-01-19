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

    public enum InteractionBehavior
    {
        None,       // No special interaction
        Flee,       // Swim away from player
        Follow,     // Follow the player
        Curious     // Initially flee, then cautiously approach
    }

    [Header("Species Information")]
    public string speciesName;
    [SerializeField] private MarineSpeciesDatabase speciesDatabase;
    [SerializeField] private MarineLifePresets presets;
    
    [Header("Movement Settings")]
    [SerializeField] private MovementPattern _movementPattern;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _moveRadius = 3f;
    [SerializeField] private float pulseFrequency = 1f;
    [SerializeField] private float pulseAmplitude = 0.5f;
    [SerializeField] private Vector2 patrolPoints = new Vector2(5f, 5f);

    [Header("Interaction Settings")]
    [SerializeField] private InteractionBehavior _interactionBehavior;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _interactionSpeed = 3f;
    [SerializeField] private float _minPlayerDistance = 2f;

    // Public properties with get/set accessors
    public MovementPattern movementPattern
    {
        get { return _movementPattern; }
        set { _movementPattern = value; }
    }

    public float moveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public float moveRadius
    {
        get { return _moveRadius; }
        set { _moveRadius = value; }
    }

    public InteractionBehavior interactionBehavior
    {
        get { return _interactionBehavior; }
        set { _interactionBehavior = value; }
    }

    public float detectionRadius
    {
        get { return _detectionRadius; }
        set { _detectionRadius = value; }
    }

    public float interactionSpeed
    {
        get { return _interactionSpeed; }
        set { _interactionSpeed = value; }
    }

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

    private bool isInteracting = false;
    private float curiosityTimer = 0f;
    private bool isCuriousApproaching = false;

    private GameObject player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Load database if not set
        if (speciesDatabase == null)
        {
            speciesDatabase = Resources.Load<MarineSpeciesDatabase>("MarineSpeciesDatabase");
            if (speciesDatabase == null)
            {
                Debug.LogError("[MarineLife] Could not load MarineSpeciesDatabase from Resources!");
                return;
            }
        }

        // Load presets if not set
        if (presets == null)
        {
            presets = Resources.Load<MarineLifePresets>("MarineLifePresets");
        }

        startPosition = transform.position;
        originalScale = transform.localScale;
        
        // Find species data
        if (string.IsNullOrEmpty(speciesName))
        {
            // If no species name set, pick a random one
            if (speciesDatabase.species != null && speciesDatabase.species.Length > 0)
            {
                speciesData = speciesDatabase.species[Random.Range(0, speciesDatabase.species.Length)];
                speciesName = speciesData.speciesName;
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
            Debug.LogError($"[MarineLife] Species data not found for {speciesName}!");
            return;
        }

        ConfigureMovementPattern();
        
        // Find UIManager in scene
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("[MarineLife] UIManager not found in scene!");
        }
        
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
            col.radius = 1f;
            col.isTrigger = true;
        }
        
        // Set up initial patrol target if needed
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

                        /* Sprite loading disabled due to asset quality issues (fake transparency/scaling)
                        // Try to load sprite from Resources
                        // Convert display name (e.g. "Blue Whale" -> "blueWhale")
                        string spriteName = char.ToLower(speciesName[0]) + speciesName.Substring(1).Replace(" ", "");
                        string resourcePath = $"MarineSpecies/{spriteName}";
                        Sprite speciesSprite = Resources.Load<Sprite>(resourcePath);
                        
                        if (speciesSprite != null)
                        {
                            spriteRenderer.sprite = speciesSprite;
                            // Reset color to white so we see the sprite's actual colors
                            spriteRenderer.color = Color.white; 
                            Debug.Log($"[MarineLife] Successfully loaded sprite for {speciesName} at {resourcePath}");
                        }
                        else
                        {
                            Debug.LogWarning($"[MarineLife] Failed to load sprite for {speciesName} at path: {resourcePath}. Files in Resources/MarineSpecies should be checked.");
                        }
                        */
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && speciesData != null && uiManager != null)
        {
            bool isNewDiscovery = !uiManager.IsSpeciesDiscovered(speciesName);
            
            // Show species information
            if (speciesData.facts != null && speciesData.facts.Length > 0)
            {
                string randomFact = speciesData.facts[Random.Range(0, speciesData.facts.Length)];
                uiManager.ShowFact(randomFact, speciesName);
            }
            
            // Only trigger camera effect for new discoveries
            if (isNewDiscovery && cameraEffects != null)
            {
                cameraEffects.TriggerDiscoveryEffect(transform);
            }
        }
    }

    private void Update()
    {
        // First check for player interaction
        HandlePlayerInteraction();

        // Then apply regular movement pattern if not interacting
        if (!isInteracting)
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
    }

    private void HandlePlayerInteraction()
    {
        if (interactionBehavior == InteractionBehavior.None) return;

        // Find player if not already found
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        Vector2 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if player is within detection radius
        if (distanceToPlayer <= detectionRadius)
        {
            isInteracting = true;
            Vector2 moveDirection = Vector2.zero;

            switch (interactionBehavior)
            {
                case InteractionBehavior.Flee:
                    // Move away from player with some vertical movement preserved for jellyfish
                    moveDirection = -directionToPlayer.normalized;
                    if (movementPattern == MovementPattern.Pulse)
                    {
                        moveDirection = new Vector2(moveDirection.x, Mathf.Max(moveDirection.y, 0.3f));
                        moveDirection.Normalize();
                    }
                    break;

                case InteractionBehavior.Follow:
                    // Follow player but maintain minimum distance
                    if (distanceToPlayer > _minPlayerDistance)
                    {
                        moveDirection = directionToPlayer.normalized;
                    }
                    break;

                case InteractionBehavior.Curious:
                    HandleCuriousBehavior(directionToPlayer, distanceToPlayer);
                    return;
            }

            // Apply movement
            if (moveDirection != Vector2.zero)
            {
                // For jellyfish, maintain some of their pulsing movement while fleeing
                if (movementPattern == MovementPattern.Pulse)
                {
                    float pulse = Mathf.Sin(pulseTime * pulseFrequency) * pulseAmplitude;
                    transform.localScale = originalScale * (1 + pulse * 0.2f);
                    
                    // Blend between flee direction and natural upward movement
                    Vector2 naturalMovement = new Vector2(Mathf.Sin(pulseTime * 0.5f) * 0.5f, 1f);
                    moveDirection = Vector2.Lerp(moveDirection, naturalMovement.normalized, 0.3f);
                }

                transform.position += (Vector3)(moveDirection * interactionSpeed * Time.deltaTime);
                RotateTowardsMovement(moveDirection);
            }
        }
        else
        {
            isInteracting = false;
            // Reset curiosity state when player is out of range
            if (interactionBehavior == InteractionBehavior.Curious)
            {
                curiosityTimer = 0f;
                isCuriousApproaching = false;
            }
        }
    }

    private void PulseMovement()
    {
        // Combine gentle upward movement with pulsing
        pulseTime += Time.deltaTime;
        float pulse = Mathf.Sin(pulseTime * pulseFrequency) * pulseAmplitude;
        
        // Scale effect for pulsing
        transform.localScale = originalScale * (1 + pulse * 0.2f);
        
        // Calculate drift direction based on current position
        Vector2 currentPos = transform.position;
        Vector2 driftDirection = Vector2.up; // Base upward movement
        
        // Add slight horizontal movement based on sine wave
        driftDirection.x = Mathf.Sin(pulseTime * 0.5f) * 0.5f;
        
        // Apply movement
        Vector2 drift = driftDirection * moveSpeed * Time.deltaTime;
        transform.position += (Vector3)drift;
        
        // Gradually return to area if too far from start
        if (Vector2.Distance(transform.position, startPosition) > moveRadius)
        {
            Vector2 directionToStart = (startPosition - (Vector2)transform.position).normalized;
            Vector2 returnMovement = directionToStart * moveSpeed * 0.5f * Time.deltaTime;
            transform.position += (Vector3)returnMovement;
        }
    }

    private void HandleCuriousBehavior(Vector2 directionToPlayer, float distanceToPlayer)
    {
        curiosityTimer += Time.deltaTime;
        Vector2 moveDirection;

        if (!isCuriousApproaching)
        {
            // Initial flee phase
            if (curiosityTimer < 2f)
            {
                moveDirection = -directionToPlayer.normalized;
                transform.position += (Vector3)(moveDirection * interactionSpeed * Time.deltaTime);
                RotateTowardsMovement(moveDirection);
            }
            else
            {
                isCuriousApproaching = true;
                curiosityTimer = 0f;
            }
        }
        else
        {
            // Cautious approach phase
            if (distanceToPlayer > _minPlayerDistance)
            {
                moveDirection = directionToPlayer.normalized;
                transform.position += (Vector3)(moveDirection * (interactionSpeed * 0.5f) * Time.deltaTime);
                RotateTowardsMovement(moveDirection);
            }
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
}
