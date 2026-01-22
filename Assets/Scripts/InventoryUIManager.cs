using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the animated inventory/Pokedex UI (Phone or Tablet interface).
/// Displays discovered species, completion progress, and allows browsing the collection.
/// </summary>
public class InventoryUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup inventoryCanvasGroup;  // For fade animations
    [SerializeField] private Transform speciesListContainer;    // Parent for species entries
    [SerializeField] private GameObject speciesEntryPrefab;     // Prefab for each species display
    [SerializeField] private Image speciesDetailPanel;          // Right panel showing species details
    [SerializeField] private TextMeshProUGUI speciesNameText;
    [SerializeField] private TextMeshProUGUI scientificNameText;
    [SerializeField] private TextMeshProUGUI habitatText;
    [SerializeField] private TextMeshProUGUI factsProgressText;  // "3/5 facts learned"
    [SerializeField] private Transform factsListContainer;      // Container for fact entries
    [SerializeField] private GameObject factEntryPrefab;        // Prefab for each fact

    [Header("Animation Settings")]
    [SerializeField] private float openDuration = 0.5f;
    [SerializeField] private float closeDuration = 0.3f;
    [SerializeField] private AnimationCurve openCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve closeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Display Settings")]
    [SerializeField] private bool usePhoneStyle = true;  // Phone (vertical) vs Tablet (horizontal)
    [SerializeField] private Color unlearmedFactColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    [SerializeField] private Color learnedFactColor = Color.white;

    private InventoryManager inventoryManager;
    private InventoryManager.DiscoveredSpeciesEntry selectedSpecies;
    private bool isOpen = false;
    private Coroutine animationCoroutine;
    private List<GameObject> speciesEntryObjects = new List<GameObject>();

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        
        if (inventoryCanvasGroup == null)
        {
            inventoryCanvasGroup = GetComponentInParent<CanvasGroup>();
            if (inventoryCanvasGroup == null)
            {
                Debug.LogError("[InventoryUIManager] No CanvasGroup found! Add one to parent of this UI.");
                return;
            }
        }

        // Start with inventory closed and invisible
        inventoryCanvasGroup.alpha = 0;
        gameObject.SetActive(false);

        // Subscribe to inventory changes
        if (inventoryManager != null)
        {
            InventoryManager.OnSpeciesDiscovered += OnSpeciesDiscovered;
            InventoryManager.OnInventoryChanged += RefreshInventoryList;
        }
    }

    private void OnDestroy()
    {
        if (inventoryManager != null)
        {
            InventoryManager.OnSpeciesDiscovered -= OnSpeciesDiscovered;
            InventoryManager.OnInventoryChanged -= RefreshInventoryList;
        }
    }

    private void OnSpeciesDiscovered(InventoryManager.DiscoveredSpeciesEntry entry)
    {
        // Optionally animate when a new species is added to the inventory
        Debug.Log($"[InventoryUI] New species added: {entry.speciesName}");
    }

    public void OpenInventory()
    {
        if (isOpen || animationCoroutine != null)
            return;

        gameObject.SetActive(true);
        RefreshInventoryList();
        animationCoroutine = StartCoroutine(AnimateOpen());
    }

    public void CloseInventory()
    {
        if (!isOpen || animationCoroutine != null)
            return;

        animationCoroutine = StartCoroutine(AnimateClose());
    }

    public void ToggleInventory()
    {
        if (isOpen)
            CloseInventory();
        else
            OpenInventory();
    }

    private IEnumerator AnimateOpen()
    {
        isOpen = true;
        float elapsed = 0;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float t = openCurve.Evaluate(elapsed / openDuration);
            inventoryCanvasGroup.alpha = t;
            yield return null;
        }

        inventoryCanvasGroup.alpha = 1;
        animationCoroutine = null;
    }

    private IEnumerator AnimateClose()
    {
        isOpen = false;
        float elapsed = 0;

        while (elapsed < closeDuration)
        {
            elapsed += Time.deltaTime;
            float t = closeCurve.Evaluate(elapsed / closeDuration);
            inventoryCanvasGroup.alpha = 1 - t;
            yield return null;
        }

        inventoryCanvasGroup.alpha = 0;
        gameObject.SetActive(false);
        animationCoroutine = null;
    }

    private void RefreshInventoryList()
    {
        if (inventoryManager == null)
            return;

        // Clear existing entries
        foreach (var entry in speciesEntryObjects)
        {
            Destroy(entry);
        }
        speciesEntryObjects.Clear();

        // Get all discoveries
        var discoveries = inventoryManager.GetAllDiscoveries();

        // Create UI entry for each discovered species
        foreach (var discovery in discoveries)
        {
            if (speciesEntryPrefab != null && speciesListContainer != null)
            {
                var entryObj = Instantiate(speciesEntryPrefab, speciesListContainer);
                speciesEntryObjects.Add(entryObj);

                // Set up the entry UI
                var button = entryObj.GetComponent<Button>();
                if (button != null)
                {
                    // Capture discovery in closure for the button callback
                    var capturedDiscovery = discovery;
                    button.onClick.AddListener(() => SelectSpecies(capturedDiscovery));
                }

                // Display species name and completion
                var nameText = entryObj.GetComponentInChildren<TextMeshProUGUI>();
                if (nameText != null)
                {
                    nameText.text = $"{discovery.speciesName}\n({discovery.learnedFactIndices.Count}/{discovery.totalFacts})";
                }
            }
        }

        // Select first species if none selected
        if (selectedSpecies == null && discoveries.Count > 0)
        {
            SelectSpecies(discoveries[0]);
        }
    }

    private void SelectSpecies(InventoryManager.DiscoveredSpeciesEntry species)
    {
        selectedSpecies = species;
        DisplaySpeciesDetails(species);
    }

    private void DisplaySpeciesDetails(InventoryManager.DiscoveredSpeciesEntry species)
    {
        // Update header info
        if (speciesNameText != null)
            speciesNameText.text = species.speciesName;
        
        if (scientificNameText != null)
            scientificNameText.text = species.scientificName;
        
        if (habitatText != null)
            habitatText.text = $"Habitat: {species.habitat}";

        // Update progress
        if (factsProgressText != null)
            factsProgressText.text = $"{species.learnedFactIndices.Count}/{species.totalFacts} Facts Learned";

        // Display facts
        if (factsListContainer != null && factEntryPrefab != null)
        {
            // Clear existing facts
            foreach (Transform child in factsListContainer)
            {
                Destroy(child.gameObject);
            }

            // Get the species data from database
            if (inventoryManager != null)
            {
                var db = Resources.Load<MarineSpeciesDatabase>("MarineSpeciesDatabase");
                if (db != null)
                {
                    foreach (var speciesData in db.species)
                    {
                        if (speciesData.speciesName == species.speciesName)
                        {
                            if (speciesData.facts != null)
                            {
                                for (int i = 0; i < speciesData.facts.Length; i++)
                                {
                                    var factObj = Instantiate(factEntryPrefab, factsListContainer);
                                    var factText = factObj.GetComponentInChildren<TextMeshProUGUI>();
                                    
                                    if (factText != null)
                                    {
                                        // Show fact if learned, otherwise show "?"
                                        bool learned = species.learnedFactIndices.Contains(i);
                                        if (learned)
                                        {
                                            factText.text = speciesData.facts[i];
                                            factText.color = learnedFactColor;
                                        }
                                        else
                                        {
                                            factText.text = "?????";
                                            factText.color = unlearmedFactColor;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
