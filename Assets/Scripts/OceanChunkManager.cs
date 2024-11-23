using UnityEngine;
using System.Collections.Generic;

public class OceanChunkManager : MonoBehaviour
{
    [Header("Chunk Settings")]
    [SerializeField] private GameObject chunkPrefab;
    [SerializeField] private float chunkSize = 100f;
    [SerializeField] private int viewDistance = 3;
    [SerializeField] private Transform player;

    [Header("Ocean Settings")]
    [SerializeField] private float minDepth = 20f;
    [SerializeField] private float maxDepth = 100f;
    [SerializeField] private float noiseScale = 50f;
    [SerializeField] private float oceanFloorNoiseScale = 25f;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentChunk;
    private Vector2Int lastCheckedChunk;

    private void Start()
    {
        if (player == null)
        {
            player = Camera.main.transform;
        }
        
        // Generate initial chunks around player
        UpdateChunks();
    }

    private void Update()
    {
        Vector2Int chunk = GetChunkCoord(player.position);
        
        // Only update chunks if player has moved to a new chunk
        if (chunk != lastCheckedChunk)
        {
            UpdateChunks();
            lastCheckedChunk = chunk;
        }
    }

    private void UpdateChunks()
    {
        currentChunk = GetChunkCoord(player.position);
        HashSet<Vector2Int> chunksToKeep = new HashSet<Vector2Int>();

        // Generate or activate chunks in view distance
        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int z = -viewDistance; z <= viewDistance; z++)
            {
                Vector2Int coord = currentChunk + new Vector2Int(x, z);
                chunksToKeep.Add(coord);

                if (!activeChunks.ContainsKey(coord))
                {
                    CreateChunk(coord);
                }
            }
        }

        // Remove chunks outside view distance
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
            Destroy(activeChunks[coord]);
            activeChunks.Remove(coord);
        }
    }

    private void CreateChunk(Vector2Int coord)
    {
        Vector3 position = new Vector3(coord.x * chunkSize, 0, coord.y * chunkSize);
        GameObject chunk = Instantiate(chunkPrefab, position, Quaternion.identity, transform);
        
        // Generate ocean floor height using Perlin noise
        float oceanFloorHeight = Mathf.PerlinNoise(
            (position.x + 1000) / oceanFloorNoiseScale, 
            (position.z + 1000) / oceanFloorNoiseScale
        );
        oceanFloorHeight = Mathf.Lerp(minDepth, maxDepth, oceanFloorHeight);
        
        // Set chunk properties (you can customize this based on your needs)
        chunk.name = $"OceanChunk_{coord.x}_{coord.y}";
        
        // Add the chunk to active chunks
        activeChunks.Add(coord, chunk);
        
        // Optional: Generate ocean features like coral, rocks, etc.
        GenerateOceanFeatures(chunk, oceanFloorHeight);
    }

    private Vector2Int GetChunkCoord(Vector3 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.z / chunkSize)
        );
    }

    private void GenerateOceanFeatures(GameObject chunk, float oceanFloorHeight)
    {
        // Use noise to determine feature placement
        float featureNoise = Mathf.PerlinNoise(
            (chunk.transform.position.x + 2000) / noiseScale,
            (chunk.transform.position.z + 2000) / noiseScale
        );

        // You can add different features based on the noise value and ocean floor height
        // For example:
        // - Coral reefs in shallow areas
        // - Rock formations in deeper areas
        // - Seaweed patches
        // - Schools of fish
        // This is where you'll want to instantiate your marine life prefabs
    }
}
