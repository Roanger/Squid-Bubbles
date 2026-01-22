using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages the player's inventory of discovered marine species.
/// Tracks which species have been discovered, what facts have been learned,
/// and provides methods to query discovery progress.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class DiscoveredSpeciesEntry
    {
        public string speciesName;
        public string scientificName;
        public string habitat;
        public float discoveryTime;  // World time when first discovered
        public HashSet<int> learnedFactIndices = new HashSet<int>();  // Which facts have been shown
        public int totalFacts;  // Total available facts for this species
        
        public float CompletionPercentage => totalFacts > 0 ? (learnedFactIndices.Count / (float)totalFacts) * 100f : 0f;
        
        public bool IsFullyExplored => learnedFactIndices.Count == totalFacts;
    }

    [SerializeField] private MarineSpeciesDatabase speciesDatabase;
    
    private Dictionary<string, DiscoveredSpeciesEntry> discoveries = new Dictionary<string, DiscoveredSpeciesEntry>();
    
    // Events for UI to subscribe to
    public static event Action<DiscoveredSpeciesEntry> OnSpeciesDiscovered;  // Called when a new species is first encountered
    public static event Action<string, int> OnFactLearned;  // Called when a fact is shown (speciesName, factIndex)
    public static event Action OnInventoryChanged;  // Called when inventory is modified
    
    private static InventoryManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize species database if not set
        if (speciesDatabase == null)
        {
            speciesDatabase = Resources.Load<MarineSpeciesDatabase>("MarineSpeciesDatabase");
            if (speciesDatabase == null)
            {
                Debug.LogError("[InventoryManager] Could not find MarineSpeciesDatabase!");
            }
        }
    }

    /// <summary>
    /// Records the discovery of a new species and/or learning of a fact.
    /// </summary>
    public void RecordDiscovery(string speciesName, int factIndex = -1)
    {
        if (!discoveries.ContainsKey(speciesName))
        {
            // First time discovering this species
            var entry = CreateDiscoveryEntry(speciesName);
            if (entry != null)
            {
                discoveries[speciesName] = entry;
                OnSpeciesDiscovered?.Invoke(entry);
                Debug.Log($"[Inventory] New species discovered: {speciesName}");
            }
        }

        // Record the fact if provided
        if (factIndex >= 0 && discoveries.ContainsKey(speciesName))
        {
            var entry = discoveries[speciesName];
            if (factIndex < entry.totalFacts && entry.learnedFactIndices.Add(factIndex))
            {
                OnFactLearned?.Invoke(speciesName, factIndex);
                Debug.Log($"[Inventory] Learned fact {factIndex + 1} of {entry.totalFacts} for {speciesName}");
            }
        }

        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Gets a discovered species entry by name.
    /// </summary>
    public bool TryGetDiscoveredSpecies(string speciesName, out DiscoveredSpeciesEntry entry)
    {
        return discoveries.TryGetValue(speciesName, out entry);
    }

    /// <summary>
    /// Check if a species has been discovered.
    /// </summary>
    public bool IsSpeciesDiscovered(string speciesName)
    {
        return discoveries.ContainsKey(speciesName);
    }

    /// <summary>
    /// Gets all discovered species in discovery order.
    /// </summary>
    public List<DiscoveredSpeciesEntry> GetAllDiscoveries()
    {
        var list = new List<DiscoveredSpeciesEntry>(discoveries.Values);
        return list;
    }

    /// <summary>
    /// Gets discovery statistics.
    /// </summary>
    public void GetDiscoveryStats(out int speciesDiscovered, out int totalSpeciesAvailable, out float completionPercentage)
    {
        speciesDiscovered = discoveries.Count;
        totalSpeciesAvailable = speciesDatabase?.species.Length ?? 0;
        
        // Calculate average completion across all discovered species
        float totalCompletion = 0f;
        foreach (var entry in discoveries.Values)
        {
            totalCompletion += entry.CompletionPercentage;
        }
        
        completionPercentage = speciesDiscovered > 0 ? (totalCompletion / speciesDiscovered) : 0f;
    }

    /// <summary>
    /// Gets the number of fully explored species.
    /// </summary>
    public int GetFullyExploredCount()
    {
        int count = 0;
        foreach (var entry in discoveries.Values)
        {
            if (entry.IsFullyExplored)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// Gets all species that have been partially explored (not all facts learned).
    /// </summary>
    public List<DiscoveredSpeciesEntry> GetPartiallyExploredSpecies()
    {
        var list = new List<DiscoveredSpeciesEntry>();
        foreach (var entry in discoveries.Values)
        {
            if (!entry.IsFullyExplored)
            {
                list.Add(entry);
            }
        }
        return list;
    }

    private DiscoveredSpeciesEntry CreateDiscoveryEntry(string speciesName)
    {
        // Find the species in the database
        if (speciesDatabase == null)
        {
            Debug.LogError("[InventoryManager] Species database is null!");
            return null;
        }

        foreach (var species in speciesDatabase.species)
        {
            if (species.speciesName == speciesName)
            {
                return new DiscoveredSpeciesEntry
                {
                    speciesName = species.speciesName,
                    scientificName = species.scientificName,
                    habitat = species.habitat,
                    discoveryTime = Time.timeSinceLevelLoad,
                    totalFacts = species.facts?.Length ?? 0
                };
            }
        }

        Debug.LogWarning($"[InventoryManager] Species '{speciesName}' not found in database!");
        return null;
    }

    // Public static access for convenience
    public static InventoryManager Instance => instance;

    public static void RecordDiscoveryStatic(string speciesName, int factIndex = -1)
    {
        if (instance != null)
        {
            instance.RecordDiscovery(speciesName, factIndex);
        }
    }

    public static bool IsSpeciesDiscoveredStatic(string speciesName)
    {
        return instance != null && instance.IsSpeciesDiscovered(speciesName);
    }
}
