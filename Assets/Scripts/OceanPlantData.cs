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
    public PlantSpecies[] species = new PlantSpecies[]
    {
        new PlantSpecies
        {
            speciesName = "Giant Kelp",
            scientificName = "Macrocystis pyrifera",
            description = "The largest marine algae, forming underwater forests that support diverse ecosystems.",
            habitat = "Cold, nutrient-rich coastal waters",
            maxHeight = 30f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Can grow up to 100 feet tall and grows about 2 feet per day",
                "Creates underwater forests that provide habitat for thousands of species",
                "Has air-filled bladders called pneumatocysts that help it float upright"
            }
        },
        new PlantSpecies
        {
            speciesName = "Turtle Grass",
            scientificName = "Thalassia testudinum",
            description = "A flowering marine plant that forms vast underwater meadows in tropical waters.",
            habitat = "Tropical coastal waters",
            maxHeight = 0.35f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Can grow leaves up to 14 inches long",
                "Provides essential feeding grounds for sea turtles and manatees",
                "Has an extensive root system that helps stabilize ocean sediments"
            }
        },
        new PlantSpecies
        {
            speciesName = "Sea Lettuce",
            scientificName = "Ulva lactuca",
            description = "A bright green algae that forms thin, lettuce-like sheets.",
            habitat = "Coastal waters worldwide",
            maxHeight = 0.4f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Can be eaten by humans and is cultivated for food in some countries",
                "Grows rapidly and can double its size in less than two weeks",
                "Acts as a natural bioindicator of water quality"
            }
        },
        new PlantSpecies
        {
            speciesName = "Eelgrass",
            scientificName = "Zostera marina",
            description = "A flowering marine plant that forms dense underwater meadows in temperate waters.",
            habitat = "Temperate coastal waters",
            maxHeight = 1.2f,
            isToxic = false,
            isEdible = false,
            interestingFacts = new string[]
            {
                "Can reproduce both sexually through seeds and asexually through rhizome growth",
                "Provides crucial nursery habitat for many commercial fish species",
                "One square meter can contain up to 4,000 individual plants"
            }
        },
        new PlantSpecies
        {
            speciesName = "Bladder Wrack",
            scientificName = "Fucus vesiculosus",
            description = "A common seaweed with distinctive air bladders that help it float.",
            habitat = "Rocky shores in temperate waters",
            maxHeight = 0.6f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Contains air bladders in pairs along its fronds",
                "Has been used traditionally in medicine and as a source of iodine",
                "Can survive several days of exposure during low tides"
            }
        }
    };
}
