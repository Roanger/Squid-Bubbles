using UnityEngine;
using System.Collections.Generic;

public class OceanChunkManager : MonoBehaviour
{
    [Header("Chunk Settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private float chunkSize = 20f; // Each chunk is 20 units wide/tall
    [SerializeField] private int viewDistance = 2;  // How many chunks to load in each direction
    
    [Header("Ocean Settings")]
    [SerializeField] private float minOceanDepth = 10f;
    [SerializeField] private float maxOceanDepth = 30f;
    
    private Dictionary<Vector2Int, OceanChunk> activeChunks = new Dictionary<Vector2Int, OceanChunk>();
    private Transform playerTransform;
    private Vector2Int currentPlayerChunk;
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure player has 'Player' tag.");
            return;
        }
        
        // Set initial chunk position
        UpdateChunks();
    }
    
    private void Update()
    {
        Vector2Int newChunk = GetChunkCoordFromPosition(playerTransform.position);
        if (newChunk != currentPlayerChunk)
        {
            currentPlayerChunk = newChunk;
            UpdateChunks();
        }
    }
    
    private Vector2Int GetChunkCoordFromPosition(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }
    
    private void UpdateChunks()
    {
        // Get chunks that should be active
        HashSet<Vector2Int> chunksToKeep = new HashSet<Vector2Int>();
        
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int coord = currentPlayerChunk + new Vector2Int(x, y);
                chunksToKeep.Add(coord);
                
                if (!activeChunks.ContainsKey(coord))
                {
                    CreateChunk(coord);
                }
            }
        }
        
        // Remove chunks that are too far
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();
        foreach (var chunk in activeChunks)
        {
            if (!chunksToKeep.Contains(chunk.Key))
            {
                chunksToRemove.Add(chunk.Key);
            }
        }
        
        foreach (var coord in chunksToRemove)
        {
            DestroyChunk(coord);
        }
    }
    
    private void CreateChunk(Vector2Int coord)
    {
        Vector3 position = new Vector3(
            coord.x * chunkSize,
            coord.y * chunkSize,
            0f
        );
        
        GameObject chunkObj = Instantiate(chunkPrefab, position, Quaternion.identity, transform);
        chunkObj.name = $"OceanChunk_{coord.x}_{coord.y}";
        chunkObj.transform.localScale = new Vector3(chunkSize, chunkSize, 1f);
        
        OceanChunk chunk = chunkObj.GetComponent<OceanChunk>();
        if (chunk != null)
        {
            // Calculate ocean depth based on distance from surface
            float depthFactor = Mathf.PerlinNoise(coord.x * 0.5f, coord.y * 0.5f);
            float oceanDepth = Mathf.Lerp(minOceanDepth, maxOceanDepth, depthFactor);
            chunk.Initialize(oceanDepth);
            
            activeChunks.Add(coord, chunk);
        }
    }
    
    private void DestroyChunk(Vector2Int coord)
    {
        if (activeChunks.TryGetValue(coord, out OceanChunk chunk))
        {
            Destroy(chunk.gameObject);
            activeChunks.Remove(coord);
        }
    }
    
    // Helper method to get world position from chunk coordinates
    public Vector3 GetChunkWorldPosition(Vector2Int coord)
    {
        return new Vector3(coord.x * chunkSize, coord.y * chunkSize, 0f);
    }
    
    // Helper method to get chunk size
    public float GetChunkSize()
    {
        return chunkSize;
    }
}
