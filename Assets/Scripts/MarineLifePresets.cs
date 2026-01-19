using UnityEngine;

[CreateAssetMenu(fileName = "MarineLifePresets", menuName = "Squid Bubbles/Marine Life Presets")]
public class MarineLifePresets : ScriptableObject
{
    [System.Serializable]
    public class SpeciesPreset
    {
        public string speciesName;
        public Color speciesColor = Color.white;
        public Vector3 scale = Vector3.one;
        public MarineLife.MovementPattern movementPattern;
        public float moveSpeed = 2f;
        public float moveRadius = 3f;
        public MarineLife.InteractionBehavior interactionBehavior;
        public float detectionRadius = 5f;
        public float interactionSpeed = 3f;
    }

    public SpeciesPreset[] presets = new SpeciesPreset[]
    {
        new SpeciesPreset
        {
            speciesName = "Blue Whale",
            speciesColor = new Color(0.2f, 0.3f, 0.8f), // Deep Blue
            scale = new Vector3(3.5f, 3.5f, 1f),
            movementPattern = MarineLife.MovementPattern.Drift,
            moveSpeed = 1.5f,
            moveRadius = 6f,
            interactionBehavior = MarineLife.InteractionBehavior.None,
            detectionRadius = 8f,
            interactionSpeed = 3f
        },
        new SpeciesPreset
        {
            speciesName = "Clownfish",
            speciesColor = new Color(1f, 0.5f, 0f), // Orange
            scale = new Vector3(0.5f, 0.5f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 4f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 4f,
            interactionSpeed = 5f
        },
        new SpeciesPreset
        {
            speciesName = "Giant Pacific Octopus",
            speciesColor = new Color(0.8f, 0.2f, 0.2f), // Red
            scale = new Vector3(2f, 2f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 3f,
            moveRadius = 3f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 6f,
            interactionSpeed = 4f
        },
        new SpeciesPreset
        {
            speciesName = "Manta Ray",
            speciesColor = new Color(0.3f, 0.3f, 0.3f), // Dark Gray
            scale = new Vector3(2.5f, 2.5f, 1f),
            movementPattern = MarineLife.MovementPattern.Glide,
            moveSpeed = 3f,
            moveRadius = 4f,
            interactionBehavior = MarineLife.InteractionBehavior.None,
            detectionRadius = 7f,
            interactionSpeed = 4f
        },
        new SpeciesPreset
        {
            speciesName = "Seahorse",
            speciesColor = new Color(1f, 1f, 0f), // Yellow
            scale = new Vector3(0.4f, 0.4f, 1f),
            movementPattern = MarineLife.MovementPattern.Hover,
            moveSpeed = 1f,
            moveRadius = 1f,
            interactionBehavior = MarineLife.InteractionBehavior.Flee,
            detectionRadius = 3f,
            interactionSpeed = 3f
        },
        new SpeciesPreset
        {
            speciesName = "Hammerhead Shark",
            speciesColor = new Color(0.5f, 0.5f, 0.5f), // Gray
            scale = new Vector3(2f, 2f, 1f),
            movementPattern = MarineLife.MovementPattern.Patrol,
            moveSpeed = 4.5f,
            moveRadius = 4f,
            interactionBehavior = MarineLife.InteractionBehavior.Follow,
            detectionRadius = 8f,
            interactionSpeed = 5f
        },
        new SpeciesPreset
        {
            speciesName = "Sea Turtle",
            speciesColor = new Color(0f, 0.8f, 0f), // Green
            scale = new Vector3(1.5f, 1.5f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 2.5f,
            moveRadius = 3f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 5f,
            interactionSpeed = 3.5f
        },
        new SpeciesPreset
        {
            speciesName = "Jellyfish",
            speciesColor = new Color(1f, 0.8f, 1f), // Pink
            scale = new Vector3(0.8f, 0.8f, 1f),
            movementPattern = MarineLife.MovementPattern.Pulse,
            moveSpeed = 1f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Flee,
            detectionRadius = 4f,
            interactionSpeed = 2.5f
        },
        new SpeciesPreset
        {
            speciesName = "Dolphin",
            speciesColor = new Color(0.7f, 0.7f, 0.8f), // Light Gray
            scale = new Vector3(2f, 2f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 7f,
            moveRadius = 5f,
            interactionBehavior = MarineLife.InteractionBehavior.Follow,
            detectionRadius = 8f,
            interactionSpeed = 8.5f
        },
        new SpeciesPreset
        {
            speciesName = "Anglerfish",
            speciesColor = new Color(0.1f, 0.1f, 0.1f), // Very Dark Gray
            scale = new Vector3(0.6f, 0.6f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 2f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 5f,
            interactionSpeed = 2f
        },
        new SpeciesPreset
        {
            speciesName = "Angelfish",
            speciesColor = new Color(0.2f, 0.8f, 0.9f), // Cyan
            scale = new Vector3(0.7f, 0.7f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 2.5f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 4f,
            interactionSpeed = 3f
        },
        new SpeciesPreset
        {
            speciesName = "Blue Tang",
            speciesColor = new Color(0.1f, 0.2f, 0.9f), // Deep Blue
            scale = new Vector3(0.6f, 0.6f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 4f,
            moveRadius = 3f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 5f,
            interactionSpeed = 5f
        },
        new SpeciesPreset
        {
            speciesName = "Yellow Tang",
            speciesColor = new Color(1f, 0.9f, 0.1f), // Bright Yellow
            scale = new Vector3(0.6f, 0.6f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 3.5f,
            moveRadius = 3f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious, // School behavior not implemented, using Curious
            detectionRadius = 4f,
            interactionSpeed = 4f
        },
        new SpeciesPreset
        {
            speciesName = "Parrotfish",
            speciesColor = new Color(0.2f, 0.8f, 0.4f), // Greenish
            scale = new Vector3(1f, 1f, 1f),
            movementPattern = MarineLife.MovementPattern.Patrol,
            moveSpeed = 2.5f,
            moveRadius = 4f,
            interactionBehavior = MarineLife.InteractionBehavior.None,
            detectionRadius = 3f,
            interactionSpeed = 3f
        },
        new SpeciesPreset
        {
            speciesName = "Orca",
            speciesColor = new Color(0.1f, 0.1f, 0.1f), // Black
            scale = new Vector3(3f, 3f, 1f),
            movementPattern = MarineLife.MovementPattern.Patrol,
            moveSpeed = 6f,
            moveRadius = 8f,
            interactionBehavior = MarineLife.InteractionBehavior.Follow,
            detectionRadius = 10f,
            interactionSpeed = 7f
        },
        new SpeciesPreset
        {
            speciesName = "Sea Otter",
            speciesColor = new Color(0.6f, 0.4f, 0.2f), // Brown
            scale = new Vector3(1.2f, 1.2f, 1f),
            movementPattern = MarineLife.MovementPattern.Hover,
            moveSpeed = 1.5f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 6f,
            interactionSpeed = 2f
        },
        new SpeciesPreset
        {
            speciesName = "Lionfish",
            speciesColor = new Color(0.8f, 0.2f, 0.2f), // Reddish
            scale = new Vector3(1.2f, 1.2f, 1f),
            movementPattern = MarineLife.MovementPattern.Hover,
            moveSpeed = 1.5f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.None,
            detectionRadius = 4f,
            interactionSpeed = 2f
        },
        new SpeciesPreset
        {
            speciesName = "Mandarin Fish",
            speciesColor = new Color(0f, 0.5f, 1f), // Vibrant Blue
            scale = new Vector3(0.4f, 0.4f, 1f),
            movementPattern = MarineLife.MovementPattern.Hover,
            moveSpeed = 1f,
            moveRadius = 1f,
            interactionBehavior = MarineLife.InteractionBehavior.Flee,
            detectionRadius = 3f,
            interactionSpeed = 4f
        },
        new SpeciesPreset
        {
            speciesName = "Pufferfish",
            speciesColor = new Color(0.9f, 0.8f, 0.4f), // Sandy Yellow
            scale = new Vector3(0.9f, 0.9f, 1f),
            movementPattern = MarineLife.MovementPattern.Circular,
            moveSpeed = 1.5f,
            moveRadius = 2f,
            interactionBehavior = MarineLife.InteractionBehavior.Curious,
            detectionRadius = 4f,
            interactionSpeed = 1f
        },
        new SpeciesPreset
        {
            speciesName = "Barracuda",
            speciesColor = new Color(0.6f, 0.7f, 0.8f), // Silvery Blue
            scale = new Vector3(2.5f, 1f, 1f), // Long and thin
            movementPattern = MarineLife.MovementPattern.Patrol,
            moveSpeed = 5.5f,
            moveRadius = 6f,
            interactionBehavior = MarineLife.InteractionBehavior.Follow,
            detectionRadius = 8f,
            interactionSpeed = 6f
        },
        new SpeciesPreset
        {
            speciesName = "Seadragon",
            speciesColor = new Color(0.8f, 0.6f, 0.2f), // Golden/Brown
            scale = new Vector3(1.1f, 1.1f, 1f),
            movementPattern = MarineLife.MovementPattern.Drift,
            moveSpeed = 0.8f,
            moveRadius = 3f,
            interactionBehavior = MarineLife.InteractionBehavior.None,
            detectionRadius = 3f,
            interactionSpeed = 1f
        }
    };
}
