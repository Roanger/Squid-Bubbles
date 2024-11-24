using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Water2DEffect : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float waveSpeed = 0.5f;
    [SerializeField] private float waveAmplitude = 0.2f;
    [SerializeField] private float waveFrequency = 2f;
    
    [Header("Layers")]
    [SerializeField] private SpriteRenderer backgroundLayer; // Main water texture
    [SerializeField] private SpriteRenderer surfaceLayer;    // Optional surface detail/foam
    
    [Header("Animation")]
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0.05f);
    [SerializeField] private bool animateSurface = true;
    
    private Material backgroundMaterial;
    private Material surfaceMaterial;
    private Vector2 backgroundOffset;
    private Vector2 surfaceOffset;
    private float timeOffset;
    
    private void Start()
    {
        // Get material instances
        if (backgroundLayer != null)
        {
            backgroundMaterial = new Material(backgroundLayer.material);
            backgroundLayer.material = backgroundMaterial;
        }
        
        if (surfaceLayer != null)
        {
            surfaceMaterial = new Material(surfaceLayer.material);
            surfaceLayer.material = surfaceMaterial;
        }
        
        // Random start time for variation between chunks
        timeOffset = Random.Range(0f, 100f);
    }
    
    private void Update()
    {
        // Scroll background water texture
        if (backgroundMaterial != null)
        {
            backgroundOffset += scrollSpeed * Time.deltaTime;
            backgroundOffset.x = Mathf.Repeat(backgroundOffset.x, 1f);
            backgroundOffset.y = Mathf.Repeat(backgroundOffset.y, 1f);
            backgroundMaterial.mainTextureOffset = backgroundOffset;
        }
        
        // Animate surface layer if enabled
        if (animateSurface && surfaceLayer != null)
        {
            // Create gentle wave motion
            float time = Time.time + timeOffset;
            Vector3 pos = surfaceLayer.transform.localPosition;
            pos.y = Mathf.Sin(time * waveFrequency) * waveAmplitude;
            surfaceLayer.transform.localPosition = pos;
            
            // Scroll surface texture independently
            if (surfaceMaterial != null)
            {
                surfaceOffset += scrollSpeed * 1.5f * Time.deltaTime;
                surfaceOffset.x = Mathf.Repeat(surfaceOffset.x, 1f);
                surfaceOffset.y = Mathf.Repeat(surfaceOffset.y, 1f);
                surfaceMaterial.mainTextureOffset = surfaceOffset;
            }
        }
    }
    
    private void OnDestroy()
    {
        // Clean up material instances
        if (backgroundMaterial != null)
            Destroy(backgroundMaterial);
        if (surfaceMaterial != null)
            Destroy(surfaceMaterial);
    }
    
    // Optional: Add methods to sync wave motion across chunks
    public void SyncWaves(float globalTime)
    {
        timeOffset = globalTime;
    }
}
