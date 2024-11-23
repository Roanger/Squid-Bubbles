using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DiscoveryCollectionUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject speciesItemPrefab;
    [SerializeField] private Transform speciesContainer;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button toggleButton;

    private Dictionary<string, GameObject> discoveredSpecies = new Dictionary<string, GameObject>();
    private bool isVisible = false;

    private void Awake()
    {
        // Ensure references
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        // Hide UI initially
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void Start()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleCollection);
    }

    public void AddSpecies(string speciesName, Sprite speciesIcon)
    {
        if (discoveredSpecies.ContainsKey(speciesName) || speciesContainer == null || speciesItemPrefab == null)
            return;

        GameObject newSpeciesItem = Instantiate(speciesItemPrefab, speciesContainer);
        if (newSpeciesItem != null)
        {
            SpeciesItemUI itemUI = newSpeciesItem.GetComponent<SpeciesItemUI>();
            if (itemUI != null)
            {
                itemUI.Initialize(speciesName, speciesIcon);
                discoveredSpecies.Add(speciesName, newSpeciesItem);
            }
        }
    }

    public void ToggleCollection()
    {
        if (canvasGroup == null) return;

        isVisible = !isVisible;
        canvasGroup.alpha = isVisible ? 1f : 0f;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }

    private void OnDestroy()
    {
        if (toggleButton != null)
            toggleButton.onClick.RemoveListener(ToggleCollection);
    }
}
