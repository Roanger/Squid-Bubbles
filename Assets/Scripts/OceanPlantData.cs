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
        },
        new PlantSpecies
        {
            speciesName = "Red Algae",
            scientificName = "Rhodophyta",
            description = "Algae that appears red because of the pigment phycoerythrin.",
            habitat = "Deep tropical waters",
            maxHeight = 0.2f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Can grow deeper than other algae because it absorbs blue light.",
                "Used to make agar and carrageenan (food thickeners).",
                "Has been used in sushi wrappers (nori) for centuries."
            }
        },
        new PlantSpecies
        {
            speciesName = "Sargassum",
            scientificName = "Sargassum",
            description = "Brown algae that features berry-like gas bladders to keep it afloat.",
            habitat = "Open ocean surface",
            maxHeight = 5f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Forms massive floating rafts that can stretch for miles.",
                "Provides a unique habitat for specialized animals like the sargassum fish.",
                "Often washes up on beaches in large quantities."
            }
        },
        new PlantSpecies
        {
            speciesName = "Neptune Grass",
            scientificName = "Posidonia oceanica",
            description = "A seagrass species that is endemic to the Mediterranean Sea.",
            habitat = "Sandy seabeds",
            maxHeight = 1f,
            isToxic = false,
            isEdible = false,
            interestingFacts = new string[]
            {
                "Can clone itself - one colony is estimated to be over 100,000 years old!",
                "Produces 'sea balls' of fiber that wash up on beaches.",
                "Captures large amounts of carbon dioxide."
            }
        },
        new PlantSpecies
        {
            speciesName = "Sea Grapes",
            scientificName = "Caulerpa lentillifera",
            description = "Green algae that looks like tiny clusters of grapes.",
            habitat = "Shallow coastal waters",
            maxHeight = 0.15f,
            isToxic = false,
            isEdible = true,
            interestingFacts = new string[]
            {
                "Popular delicacy in Okinawa and Philippines, known as 'green caviar'.",
                "Pop when eaten, releasing a salty taste.",
                "Can be farmed in ponds."
            }
        },
        new PlantSpecies
        {
            speciesName = "Mermaid's Fan",
            scientificName = "Udotea",
            description = "Green algae with a distinct fan-shaped blade.",
            habitat = "Tropical sandy bottoms",
            maxHeight = 0.2f,
            isToxic = true,
            isEdible = false,
            interestingFacts = new string[]
            {
                "Contains calcium carbonate, making it hard and crunchy.",
                "Helps produce sand when it dies and crumbles.",
                "Often found in seagrass beds."
            }
        }
    };
}
