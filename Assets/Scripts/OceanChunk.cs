using UnityEngine;

public class OceanChunk : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material oceanFloorMaterial;
    
    [Header("Marine Life")]
    [SerializeField] private GameObject[] coralPrefabs;
    [SerializeField] private GameObject[] rockPrefabs;
    [SerializeField] private GameObject[] plantPrefabs;
    [SerializeField] private GameObject[] fishPrefabs;
    
    private MeshFilter waterMeshFilter;
    private MeshFilter floorMeshFilter;
    
    public void Initialize(float oceanFloorHeight)
    {
        // Create water surface
        CreateWaterSurface();
        
        // Create ocean floor
        CreateOceanFloor(oceanFloorHeight);
    }
    
    private void CreateWaterSurface()
    {
        GameObject waterPlane = new GameObject("WaterSurface");
        waterPlane.transform.SetParent(transform, false);
        
        waterMeshFilter = waterPlane.AddComponent<MeshFilter>();
        MeshRenderer waterRenderer = waterPlane.AddComponent<MeshRenderer>();
        
        // Create a simple plane mesh for water
        Mesh waterMesh = new Mesh();
        waterMeshFilter.mesh = waterMesh;
        
        // Set vertices for a flat plane
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-0.5f, 0, -0.5f);
        vertices[1] = new Vector3(0.5f, 0, -0.5f);
        vertices[2] = new Vector3(-0.5f, 0, 0.5f);
        vertices[3] = new Vector3(0.5f, 0, 0.5f);
        
        waterMesh.vertices = vertices;
        
        // Set triangles
        int[] triangles = new int[] { 0, 2, 1, 2, 3, 1 };
        waterMesh.triangles = triangles;
        
        // Set UVs
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
        waterMesh.uv = uvs;
        
        waterMesh.RecalculateNormals();
        waterMesh.RecalculateBounds();
        
        // Set material
        waterRenderer.material = waterMaterial;
    }
    
    private void CreateOceanFloor(float height)
    {
        GameObject floor = new GameObject("OceanFloor");
        floor.transform.SetParent(transform, false);
        floor.transform.localPosition = new Vector3(0, -height, 0);
        
        floorMeshFilter = floor.AddComponent<MeshFilter>();
        MeshRenderer floorRenderer = floor.AddComponent<MeshRenderer>();
        
        // Create a simple plane mesh for the floor
        Mesh floorMesh = new Mesh();
        floorMeshFilter.mesh = floorMesh;
        
        // Set vertices for a flat plane
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-0.5f, 0, -0.5f);
        vertices[1] = new Vector3(0.5f, 0, -0.5f);
        vertices[2] = new Vector3(-0.5f, 0, 0.5f);
        vertices[3] = new Vector3(0.5f, 0, 0.5f);
        
        floorMesh.vertices = vertices;
        
        // Set triangles
        int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        floorMesh.triangles = triangles;
        
        // Set UVs
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
        floorMesh.uv = uvs;
        
        floorMesh.RecalculateNormals();
        floorMesh.RecalculateBounds();
        
        // Set material
        floorRenderer.material = oceanFloorMaterial;
    }
    
    public void AddFeature(GameObject featurePrefab, Vector3 position, Quaternion rotation)
    {
        if (featurePrefab != null)
        {
            Instantiate(featurePrefab, position, rotation, transform);
        }
    }
}
