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
        }
    };
}
