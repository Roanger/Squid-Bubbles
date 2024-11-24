using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WaterMovement : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float waveSpeed = 0.5f;
    [SerializeField] private Vector2 waveDirection = new Vector2(1f, 1f);
    
    [Header("Tiling")]
    [SerializeField] private Vector2 baseTiling = new Vector2(10f, 10f);
    [SerializeField] private Vector2 normalTiling = new Vector2(20f, 20f);
    
    private Material waterMaterial;
    private Vector2 currentOffset;
    
    private void Start()
    {
        // Get the material instance so we don't affect other objects using the same material
        waterMaterial = GetComponent<MeshRenderer>().material;
        
        // Set initial tiling
        waterMaterial.mainTextureScale = baseTiling;
        if (waterMaterial.HasProperty("_BumpMap"))
        {
            waterMaterial.SetTextureScale("_BumpMap", normalTiling);
        }
    }
    
    private void Update()
    {
        // Update the texture offset for wave movement
        currentOffset += waveDirection.normalized * waveSpeed * Time.deltaTime;
        
        // Keep the offset values from getting too large
        currentOffset.x = Mathf.Repeat(currentOffset.x, 1f);
        currentOffset.y = Mathf.Repeat(currentOffset.y, 1f);
        
        // Apply the offset to main texture
        waterMaterial.mainTextureOffset = currentOffset;
        
        // If using a normal map, apply the offset to it as well
        if (waterMaterial.HasProperty("_BumpMap"))
        {
            waterMaterial.SetTextureOffset("_BumpMap", currentOffset * 1.5f); // Slightly different speed for normal map
        }
    }
    
    private void OnDestroy()
    {
        // Clean up the material instance
        if (waterMaterial != null)
        {
            Destroy(waterMaterial);
        }
    }
}
