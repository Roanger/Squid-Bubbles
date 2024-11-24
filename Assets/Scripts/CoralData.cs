using UnityEngine;

[System.Serializable]
public class CoralSpecies
{
    public string speciesName;
    public string scientificName;
    public string description;
    public string coralType; // e.g., "Hard Coral", "Soft Coral", "Sea Fan"
    public Color dominantColor = Color.white;
    public float maxSize = 1f;
    public float growthRate = 1f; // cm per year
    public string[] interestingFacts;
    public string[] symbiotic; // Names of species that live in/around this coral
    public bool isEndangered;
    public string conservationStatus; // e.g., "Least Concern", "Vulnerable", "Endangered"
}

[CreateAssetMenu(fileName = "CoralDatabase", menuName = "Squid Bubbles/Coral Database")]
public class CoralData : ScriptableObject
{
    public CoralSpecies[] species;
}
