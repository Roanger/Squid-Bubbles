# Inventory System Implementation Guide

## Overview
The inventory system now consists of three main components that work together to track and display discovered marine species:

### 1. **InventoryManager** (Model)
- Tracks all discovered species and learned facts
- Stores completion progress per species
- Provides query methods for inventory state
- Located: `Assets/Scripts/InventoryManager.cs`
- **Key Features:**
  - Singleton pattern (only one instance at a time)
  - Events system for UI updates (OnSpeciesDiscovered, OnFactLearned, OnInventoryChanged)
  - Automatic database integration
  - Persistent discovery tracking

### 2. **InventoryUIManager** (View)
- Displays the animated phone/tablet inventory interface
- Shows species list and selected species details
- Displays learned vs unlearned facts
- Located: `Assets/Scripts/InventoryUIManager.cs`
- **Key Features:**
  - Open/Close animations with customizable curves
  - Species selection and detail view
  - Progress tracking display
  - Fact state visualization (learned facts shown, unlearned as "?????")

### 3. **Integration Points**
- **MarineLife.cs**: Records discoveries when player encounters species
- **UIManager.cs**: Subscribes to inventory events for real-time updates
- **MarineSpeciesDatabase.cs**: Source of species data

---

## Unity Setup Instructions

### Step 1: Add InventoryManager to Scene
1. Create an empty GameObject in your scene: `InventoryManager`
2. Attach the `InventoryManager` script to it
3. In the Inspector, assign your `MarineSpeciesDatabase` asset to the "Species Database" field
4. This should typically be in `Assets/Resources/MarineSpeciesDatabase.asset`

### Step 2: Create Inventory UI
1. Create a new Canvas in your scene if you don't have one
2. Create a Panel child under the Canvas called `InventoryPanel`
3. Attach `InventoryUIManager` script to the InventoryPanel
4. Add a CanvasGroup component to the InventoryPanel (for fade animations)

### Step 3: Configure InventoryUIManager
In the Inspector, set up the following:

**UI References:**
- **Inventory Canvas Group**: The CanvasGroup on the same panel
- **Species List Container**: A Vertical Layout Group child panel (left side) 
- **Species Entry Prefab**: A Button prefab with TextMeshPro component for each species
- **Species Detail Panel**: A panel to display species info (right side)
- **Species Name Text**: TextMeshPro field for species name
- **Scientific Name Text**: TextMeshPro field for scientific name
- **Habitat Text**: TextMeshPro field for habitat info
- **Facts Progress Text**: TextMeshPro for "X/Y facts learned"
- **Facts List Container**: A layout group for fact entries
- **Fact Entry Prefab**: A prefab displaying individual facts

**Animation Settings:**
- Customize open/close durations and animation curves

### Step 4: Create UI Prefabs

#### Species Entry Prefab:
```
Button (LayoutElement: Preferred Height 40)
├── TextMeshProUGUI (species name and progress)
```

#### Fact Entry Prefab:
```
Image (with color/background)
├── TextMeshProUGUI (fact text)
```

### Step 5: Add Input Handler
Create a simple script to toggle the inventory:

```csharp
public class InventoryToggle : MonoBehaviour
{
    private InventoryUIManager inventoryUI;
    
    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUIManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.ToggleInventory();
        }
    }
}
```

---

## Data Flow

1. **Discovery Trigger**: Player collides with marine species
2. **MarineLife.cs**: Calls `inventoryManager.RecordDiscovery(speciesName, factIndex)`
3. **InventoryManager**: 
   - Creates DiscoveredSpeciesEntry if new
   - Adds fact to learnedFactIndices
   - Fires OnSpeciesDiscovered / OnFactLearned events
4. **InventoryUIManager**: Listens to events and refreshes display
5. **UIManager**: Also listens to events for counter updates

---

## API Reference

### InventoryManager Methods

```csharp
// Record a discovery
RecordDiscovery(string speciesName, int factIndex = -1)

// Check discovery status
IsSpeciesDiscovered(string speciesName)
TryGetDiscoveredSpecies(string speciesName, out DiscoveredSpeciesEntry entry)

// Get statistics
GetDiscoveryStats(out int discovered, out int total, out float completion%)
GetFullyExploredCount()
GetPartiallyExploredSpecies()
GetAllDiscoveries()
```

### InventoryUIManager Methods

```csharp
// Control inventory display
OpenInventory()
CloseInventory()
ToggleInventory()
```

### Events (Subscribe in scripts)

```csharp
InventoryManager.OnSpeciesDiscovered += (entry) => { };
InventoryManager.OnFactLearned += (speciesName, factIndex) => { };
InventoryManager.OnInventoryChanged += () => { };
```

---

## Future Enhancements

- [ ] Save/Load inventory to disk
- [ ] Add species images/sprites to display
- [ ] Biome-based filtering in inventory
- [ ] Achievements for collecting all facts
- [ ] Species rarity/completion badges
- [ ] Sound effects for discovery and learning
- [ ] Pagination for large inventories
- [ ] Search/filter functionality
