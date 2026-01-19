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
    public CoralSpecies[] species = new CoralSpecies[]
    {
        new CoralSpecies
        {
            speciesName = "Elkhorn Coral",
            scientificName = "Acropora palmata",
            description = "A branching coral species that resembles elk antlers, crucial for reef building in the Caribbean.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.8f, 0.7f, 0.6f),
            maxSize = 4f,
            growthRate = 12.7f,
            interestingFacts = new string[]
            {
                "Can grow up to 2 meters in height and 4 meters in width",
                "Grows at a rate of up to 5 inches per year",
                "Provides essential habitat for many reef fish species"
            },
            symbiotic = new string[] { "Damselfish", "Parrotfish", "Zooxanthellae" },
            isEndangered = true,
            conservationStatus = "Critically Endangered"
        },
        new CoralSpecies
        {
            speciesName = "Brain Coral",
            scientificName = "Diploria labyrinthiformis",
            description = "Named for its appearance resembling a human brain, with characteristic grooved surface.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.85f, 0.8f, 0.7f),
            maxSize = 1.8f,
            growthRate = 0.8f,
            interestingFacts = new string[]
            {
                "Can live for several hundred years",
                "The grooves help channel food to the coral polyps",
                "Produces a protective mucus layer"
            },
            symbiotic = new string[] { "Zooxanthellae", "Cleaning Gobies" },
            isEndangered = false,
            conservationStatus = "Near Threatened"
        },
        new CoralSpecies
        {
            speciesName = "Fire Coral",
            scientificName = "Millepora alcicornis",
            description = "Not a true coral but a hydrozoan that can deliver a painful sting to humans.",
            coralType = "Hydrozoan",
            dominantColor = new Color(0.9f, 0.85f, 0.6f),
            maxSize = 0.5f,
            growthRate = 2.5f,
            interestingFacts = new string[]
            {
                "Contains stinging cells called nematocysts",
                "Forms branching colonies in various colors",
                "Important reef builder despite not being true coral"
            },
            symbiotic = new string[] { "Zooxanthellae" },
            isEndangered = false,
            conservationStatus = "Least Concern"
        },
        new CoralSpecies
        {
            speciesName = "Blue Coral",
            scientificName = "Heliopora coerulea",
            description = "One of the few coral species that has a blue skeleton made of aragonite.",
            coralType = "Octocoral",
            dominantColor = new Color(0.3f, 0.4f, 0.8f),
            maxSize = 7f,
            growthRate = 1.2f,
            interestingFacts = new string[]
            {
                "Only surviving species from the Cretaceous period",
                "Blue color comes from iron salts in skeleton",
                "Can form massive colonies"
            },
            symbiotic = new string[] { "Zooxanthellae", "Coral Crabs" },
            isEndangered = true,
            conservationStatus = "Vulnerable"
        },
        new CoralSpecies
        {
            speciesName = "Bubble Coral",
            scientificName = "Plerogyra sinuosa",
            description = "Known for its grape-like bubbles that are actually vesicles filled with water.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.9f, 0.9f, 0.85f),
            maxSize = 1f,
            growthRate = 1.5f,
            interestingFacts = new string[]
            {
                "Extends bubble-like vesicles during the day",
                "Retracts its bubbles when disturbed",
                "Bubbles contain symbiotic algae"
            },
            symbiotic = new string[] { "Zooxanthellae", "Coral Shrimp" },
            isEndangered = false,
            conservationStatus = "Near Threatened"
        },
        new CoralSpecies
        {
            speciesName = "Staghorn Coral",
            scientificName = "Acropora cervicornis",
            description = "Known for its cylindrical branches that range from a few centimeters to over two meters in length.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.8f, 0.6f, 0.4f),
            maxSize = 2f,
            growthRate = 10f,
            interestingFacts = new string[]
            {
                "One of the fastest growing corals, adding 4-8 inches per year.",
                "Forms dense thickets that provide critical cover for other reef organisms.",
                "Can reproduce asexually when broken branches reattach to the substrate."
            },
            symbiotic = new string[] { "Damselfish", "Snappers" },
            isEndangered = true,
            conservationStatus = "Critically Endangered"
        },
        new CoralSpecies
        {
            speciesName = "Table Coral",
            scientificName = "Acropora",
            description = "Forms large, flat plates that look like tables, maximizing sunlight exposure.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.6f, 0.8f, 0.6f),
            maxSize = 3f,
            growthRate = 5f,
            interestingFacts = new string[]
            {
                "Its flat shape shades out other corals growing below it.",
                "Provides shelter for fish to sleep under at night.",
                "Highly susceptible to damage from storms and anchors."
            },
            symbiotic = new string[] { "Parrotfish", "Butterflyfish" },
            isEndangered = false,
            conservationStatus = "Near Threatened"
        },
        new CoralSpecies
        {
            speciesName = "Sun Coral",
            scientificName = "Tubastraea",
            description = "Bright orange or yellow cup corals that do not rely on sunlight.",
            coralType = "Hard Coral",
            dominantColor = new Color(1f, 0.6f, 0f), // Bright Orange
            maxSize = 0.3f,
            growthRate = 3f,
            interestingFacts = new string[]
            {
                "Does not contain symbiotic algae (zooxanthellae).",
                "Extends tentacles at night to catch plankton.",
                "Can grow in dark caves and under overhangs."
            },
            symbiotic = new string[] { "Sea Snails" },
            isEndangered = false,
            conservationStatus = "Least Concern"
        },
        new CoralSpecies
        {
            speciesName = "Mushroom Coral",
            scientificName = "Fungiidae",
            description = "Solitary corals that look like the cap of a mushroom when tentacles are retracted.",
            coralType = "Hard Coral",
            dominantColor = new Color(0.7f, 0.5f, 0.9f),
            maxSize = 0.5f,
            growthRate = 2f,
            interestingFacts = new string[]
            {
                "Can detach from the reef and move around!",
                "Can flip themselves over if overturned by waves.",
                "Consists of a single giant polyp."
            },
            symbiotic = new string[] { "Pipefish", "Shrimp" },
            isEndangered = false,
            conservationStatus = "Vulnerable"
        },
        new CoralSpecies
        {
            speciesName = "Sea Fan",
            scientificName = "Gorgonia",
            description = "A soft coral that grows in a flat, fan-like pattern to catch currents.",
            coralType = "Soft Coral",
            dominantColor = new Color(0.8f, 0.3f, 0.6f), // Pink/Purple
            maxSize = 1.5f,
            growthRate = 4f,
            interestingFacts = new string[]
            {
                "Orient prevents itself perpendicular to the current to maximize feeding.",
                "Flexible skeleton made of a protein called gorgonin.",
                "Often hosts tiny specialized seahorses."
            },
            symbiotic = new string[] { "Pygmy Seahorse", "Flamingo Tongue Snail" },
            isEndangered = false,
            conservationStatus = "Near Threatened"
        }
    };
}
