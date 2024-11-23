using UnityEngine;

public class SpeciesDiscoveryTest : MonoBehaviour
{
    [SerializeField] private DiscoveryCollectionUI discoveryUI;

    private string[] testSpeciesNames = {
        "Blue Whale",
        "Clownfish",
        "Dolphin",
        "Sea Turtle",
        "Octopus"
    };

    private int currentSpeciesIndex = 0;

    private void Start()
    {
        // Validate reference
        if (discoveryUI == null)
        {
            Debug.LogError("DiscoveryCollectionUI reference is missing!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        // Press space to discover next species
        if (Input.GetKeyDown(KeyCode.Space) && currentSpeciesIndex < testSpeciesNames.Length)
        {
            string speciesName = testSpeciesNames[currentSpeciesIndex];
            discoveryUI.AddSpecies(speciesName, null); // Using null for now since we don't have icons
            currentSpeciesIndex++;

            Debug.Log($"Discovered {speciesName}!");
        }
    }
}
