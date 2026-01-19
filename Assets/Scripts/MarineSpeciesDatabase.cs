using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MarineSpeciesDatabase", menuName = "Squid Bubbles/Marine Species Database")]
public class MarineSpeciesDatabase : ScriptableObject
{
    [System.Serializable]
    public class SpeciesData
    {
        public string speciesName;
        public string scientificName;
        public string habitat;
        [TextArea(3, 5)]
        public string[] facts;
    }

    public SpeciesData[] species = new SpeciesData[]
    {
        new SpeciesData
        {
            speciesName = "Blue Whale",
            scientificName = "Balaenoptera musculus",
            habitat = "Open Ocean",
            facts = new string[]
            {
                "Blue whales are the largest animals ever known to exist on Earth!",
                "They can grow up to 100 feet long and weigh up to 200 tons.",
                "A blue whale's heart can be as big as a car!",
                "Baby blue whales drink 100 gallons of milk per day and gain 200 pounds daily.",
                "Their tongue alone can weigh as much as an elephant."
            }
        },
        new SpeciesData
        {
            speciesName = "Clownfish",
            scientificName = "Amphiprioninae",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Clownfish live in a special partnership with sea anemones.",
                "All clownfish are born male - the largest in a group becomes female!",
                "They never stray far from their home anemone.",
                "Baby clownfish find their way home by following the sound of coral reefs.",
                "They perform a special dance with anemones to avoid getting stung."
            }
        },
        new SpeciesData
        {
            speciesName = "Giant Pacific Octopus",
            scientificName = "Enteroctopus dofleini",
            habitat = "Coastal Waters",
            facts = new string[]
            {
                "They have three hearts and blue blood!",
                "They can change color and texture in less than a second.",
                "They're incredibly smart and can solve puzzles and open jars.",
                "They have nine brains - one central brain and eight in their arms.",
                "They can squeeze through any opening larger than their beak."
            }
        },
        new SpeciesData
        {
            speciesName = "Manta Ray",
            scientificName = "Mobula birostris",
            habitat = "Tropical Waters",
            facts = new string[]
            {
                "Manta rays have the largest brain of all fish!",
                "They can't swim backward but can do underwater somersaults.",
                "They filter feed by doing barrel rolls through plankton clouds.",
                "They can jump up to 7 feet out of the water!",
                "Each manta ray has a unique pattern of spots, like a fingerprint."
            }
        },
        new SpeciesData
        {
            speciesName = "Seahorse",
            scientificName = "Hippocampus",
            habitat = "Seagrass Beds",
            facts = new string[]
            {
                "Male seahorses are the ones who give birth to babies!",
                "They can change color to match their surroundings.",
                "They have no stomach - food passes through them very quickly.",
                "They use their tails to anchor themselves to seaweed or coral.",
                "They're terrible swimmers and can easily die of exhaustion."
            }
        },
        new SpeciesData
        {
            speciesName = "Hammerhead Shark",
            scientificName = "Sphyrna",
            habitat = "Tropical Waters",
            facts = new string[]
            {
                "Their wide-set eyes give them better 360-degree vision.",
                "They use their head like a metal detector to find prey!",
                "They often swim in large schools during the day.",
                "Their unique head shape helps them make sharper turns.",
                "They can detect electrical signals from prey hiding in sand."
            }
        },
        new SpeciesData
        {
            speciesName = "Sea Turtle",
            scientificName = "Chelonioidea",
            habitat = "Tropical and Temperate Seas",
            facts = new string[]
            {
                "Sea turtles can live to be over 100 years old!",
                "They use Earth's magnetic field to navigate the oceans.",
                "Baby turtles' gender is determined by nest temperature.",
                "They can hold their breath for up to 5 hours while resting.",
                "They return to the same beach where they were born to lay eggs."
            }
        },
        new SpeciesData
        {
            speciesName = "Jellyfish",
            scientificName = "Medusozoa",
            habitat = "All Ocean Layers",
            facts = new string[]
            {
                "They've been around for over 650 million years!",
                "They have no brain, heart, or bones.",
                "Some species are immortal - they can reverse their aging process.",
                "They are 95% water.",
                "Their tentacles can still sting even when detached."
            }
        },
        new SpeciesData
        {
            speciesName = "Dolphin",
            scientificName = "Delphinidae",
            habitat = "Oceans Worldwide",
            facts = new string[]
            {
                "They sleep with one half of their brain at a time.",
                "Each dolphin has its own unique whistle, like a name!",
                "They can swim at speeds of up to 25 mph.",
                "They use echolocation to find food and navigate.",
                "They're one of the few animals that can learn by watching."
            }
        },
        new SpeciesData
        {
            speciesName = "Anglerfish",
            scientificName = "Lophiiformes",
            habitat = "Deep Ocean",
            facts = new string[]
            {
                "Female anglerfish are much larger than males.",
                "They use a glowing lure to attract prey in the dark.",
                "Male anglerfish fuse to females, becoming parasites.",
                "They can swallow prey twice their own size!",
                "They live in the deepest, darkest parts of the ocean."
            }
        },
        new SpeciesData
        {
            speciesName = "Angelfish",
            scientificName = "Pomacanthidae",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Angelfish engage in monogamy and often bond for life.",
                "They can change their gender as they mature.",
                "Some angelfish act as cleaner fish for other species.",
                "Their flattened bodies allow them to navigate tight coral crevices.",
                "They are known for their vibrant and complex coloring."
            }
        },
        new SpeciesData
        {
            speciesName = "Blue Tang",
            scientificName = "Paracanthurus hepatus",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Blue tangs are important for keeping coral clean by eating algae.",
                "They have a sharp spine on their tail for defense.",
                "When frightened, they may wedge themselves into coral branches.",
                "They are actually yellow when they are born!",
                "They can make themselves semi-transparent to hide."
            }
        },
        new SpeciesData
        {
            speciesName = "Yellow Tang",
            scientificName = "Zebrasoma flavescens",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Yellow tangs are bright yellow to blend in with coral and sunlight.",
                "They are active swimmers and graze on algae all day.",
                "They often travel in loose schools.",
                "At night, their bright yellow color fades to a duller shade.",
                "They help keep sea turtles clean by eating algae off their shells."
            }
        },
        new SpeciesData
        {
            speciesName = "Parrotfish",
            scientificName = "Scaridae",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Their teeth are fused together to form a beak like a parrot.",
                "They crunch on coral to get to the algae inside.",
                "Much of the sand on tropical beaches is actually parrotfish poop!",
                "They sleep in a bubble of mucus to hide their scent from predators.",
                "Their colors change dramatically as they grow up."
            }
        },
        new SpeciesData
        {
            speciesName = "Orca",
            scientificName = "Orcinus orca",
            habitat = "All Oceans",
            facts = new string[]
            {
                "Orcas are actually the largest member of the dolphin family.",
                "They hunt in coordinated pods (groups) like wolf packs.",
                "Each pod has its own unique dialect of calls.",
                "Orcas are apex predators - they even eat great white sharks!",
                "They are highly intelligent and social animals."
            }
        },
        new SpeciesData
        {
            speciesName = "Sea Otter",
            scientificName = "Enhydra lutris",
            habitat = "Exposed Coastal Environments",
            facts = new string[]
            {
                "Sea otters hold hands while sleeping so they don't drift apart.",
                "They use rocks as tools to crack open clams and mussels.",
                "They have the thickest fur of any animal on Earth.",
                "A pocket of loose skin under their arm is used to store food.",
                "They spend most of their lives in the water."
            }
        },
        new SpeciesData
        {
            speciesName = "Lionfish",
            scientificName = "Pterois",
            habitat = "Coral Reefs",
            facts = new string[]
            {
                "Their venomous spines are used for defense, not hunting.",
                "They are an invasive species in the Atlantic Ocean.",
                "A female lionfish can lay up to 2 million eggs per year.",
                "They swallow their prey whole in one quick motion.",
                "They have no known predators in their invasive ranges."
            }
        },
        new SpeciesData
        {
            speciesName = "Mandarin Fish",
            scientificName = "Synchiropus splendidus",
            habitat = "Sheltered Lagoons",
            facts = new string[]
            {
                "One of the only fish that produces its own blue pigment.",
                "They are extremely shy and only come out at dusk to mate.",
                "They have no scales, protecting themselves with smelly slime.",
                "Their bright colors warn predators that they taste bad.",
                "They 'hop' along the coral rather than swimming smoothly."
            }
        },
        new SpeciesData
        {
            speciesName = "Pufferfish",
            scientificName = "Tetraodontidae",
            habitat = "Tropical Seas",
            facts = new string[]
            {
                "They can inflate into a ball shape to evade predators.",
                "Most pufferfish contain a toxic substance called tetrodotoxin.",
                "They are the only bony fish that can close their eyes.",
                "Their teeth never stop growing, so they chew on coral to wear them down.",
                "Some species build intricate geometric circles in the sand."
            }
        },
        new SpeciesData
        {
            speciesName = "Barracuda",
            scientificName = "Sphyraena",
            habitat = "Open Water near Reefs",
            facts = new string[]
            {
                "They can swim at speeds up to 36 mph (58 km/h).",
                "They are attracted to shiny objects that look like fish scales.",
                "Their lower jaw extends past their upper jaw, showing sharp teeth.",
                "They have been around for 50 million years.",
                "Juveniles live in mangroves for protection before moving to reefs."
            }
        },
        new SpeciesData
        {
            speciesName = "Seadragon",
            scientificName = "Phycodurus eques",
            habitat = "Southern Australian Waters",
            facts = new string[]
            {
                "They look like floating seaweed to hide from predators.",
                "Males carry the eggs on their tail until they hatch.",
                "They have no teeth and suck up tiny shrimp like a vacuum.",
                "Their leaf-like appendages are only for camouflage, not movement.",
                "They can change color to blend in with their surroundings."
            }
        }
    };
}
