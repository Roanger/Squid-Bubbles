using UnityEngine;

[System.Serializable]
public class PlantSpecies
{
    public string speciesName;
    public string scientificName;
    public string description;
    public string habitat;
    public float maxHeight = 1f;
    public bool isToxic;
    public bool isEdible;
    public string[] interestingFacts;
}

[CreateAssetMenu(fileName = "OceanPlantDatabase", menuName = "Squid Bubbles/Ocean Plant Database")]
public class OceanPlantData : ScriptableObject
{
    public PlantSpecies[] species;
}
