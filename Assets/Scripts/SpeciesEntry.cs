using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class SpeciesEntry : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image speciesIcon;
    [SerializeField] private TextMeshProUGUI speciesNameText;
    [SerializeField] private TextMeshProUGUI scientificNameText;
    [SerializeField] private TextMeshProUGUI habitatText;
    [SerializeField] private TextMeshProUGUI factPreviewText;
    
    [Header("Visual Settings")]
    [SerializeField] private Color discoveredTextColor = Color.white;
    [SerializeField] private float discoveredIconOpacity = 1f;
    
    private Button entryButton;
    private string[] allFacts;
    private SpeciesDiscoveryAnimation discoveryAnimation;
    public bool IsDiscovered { get; private set; }

    private void Awake()
    {
        entryButton = GetComponent<Button>();
        entryButton.onClick.AddListener(OnEntryClicked);
        discoveryAnimation = GetComponent<SpeciesDiscoveryAnimation>();
    }

    private void OnDestroy()
    {
        if (entryButton != null)
        {
            entryButton.onClick.RemoveListener(OnEntryClicked);
        }
    }

    public void SetDiscovered(string speciesName, string scientificName, string habitat, string factPreview, string[] facts = null)
    {
        bool wasDiscovered = IsDiscovered;
        IsDiscovered = true;
        allFacts = facts;

        // Update texts
        speciesNameText.text = speciesName;
        scientificNameText.text = $"<i>{scientificName}</i>";
        habitatText.text = habitat;
        factPreviewText.text = factPreview;

        // Update colors
        speciesNameText.color = discoveredTextColor;
        scientificNameText.color = discoveredTextColor;
        habitatText.color = discoveredTextColor;
        factPreviewText.color = discoveredTextColor;

        if (speciesIcon != null)
        {
            Color iconColor = speciesIcon.color;
            iconColor.a = discoveredIconOpacity;
            speciesIcon.color = iconColor;
        }

        // Enable button interaction
        if (entryButton != null)
        {
            entryButton.interactable = true;
        }

        // Play discovery animation if this is a new discovery
        if (!wasDiscovered && discoveryAnimation != null)
        {
            discoveryAnimation.PlayDiscoveryAnimation();
            discoveryAnimation.PlayTextRevealAnimation(new TextMeshProUGUI[] 
            {
                speciesNameText,
                scientificNameText,
                habitatText,
                factPreviewText
            });
            if (speciesIcon != null)
            {
                discoveryAnimation.PlayIconRevealAnimation(speciesIcon);
            }
        }
    }

    public void SetUndiscovered(string placeholder, Color undiscoveredColor)
    {
        IsDiscovered = false;
        allFacts = null;

        // Set placeholder text
        speciesNameText.text = placeholder;
        scientificNameText.text = placeholder;
        habitatText.text = placeholder;
        factPreviewText.text = placeholder;

        // Update colors
        speciesNameText.color = undiscoveredColor;
        scientificNameText.color = undiscoveredColor;
        habitatText.color = undiscoveredColor;
        factPreviewText.color = undiscoveredColor;

        if (speciesIcon != null)
        {
            Color iconColor = speciesIcon.color;
            iconColor.a = 0.5f;
            speciesIcon.color = iconColor;
        }

        // Disable button interaction
        if (entryButton != null)
        {
            entryButton.interactable = false;
        }
    }

    private void OnEntryClicked()
    {
        if (IsDiscovered)
        {
            DiscoveryCollectionUI collectionUI = FindObjectOfType<DiscoveryCollectionUI>();
            if (collectionUI != null)
            {
                collectionUI.ShowSpeciesDetails(
                    speciesNameText.text,
                    scientificNameText.text,
                    habitatText.text,
                    allFacts
                );
            }
        }
    }
}
