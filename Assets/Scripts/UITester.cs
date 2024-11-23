using UnityEngine;

public class UITester : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MarineSpeciesDatabase speciesDatabase;

    private void Start()
    {
        if (speciesDatabase == null)
        {
            speciesDatabase = Resources.Load<MarineSpeciesDatabase>("MarineSpeciesDatabase");
        }
    }

    private void Update()
    {
        // Press space to simulate discovering a random species
        if (Input.GetKeyDown(KeyCode.Space) && speciesDatabase != null && speciesDatabase.species.Length > 0)
        {
            // Pick a random species
            var species = speciesDatabase.species[Random.Range(0, speciesDatabase.species.Length)];
            
            // Pick a random fact
            string randomFact = species.facts[Random.Range(0, species.facts.Length)];
            string formattedFact = $"{species.speciesName}\n\n{randomFact}\n\n<size=80%><i>{species.scientificName}</i>\n{species.habitat}</size>";
            
            // Show the fact
            bool isNew = uiManager.ShowFact(formattedFact, species.speciesName);
            Debug.Log($"{(isNew ? "Discovered" : "Encountered")} {species.speciesName}!");
        }
    }
}
