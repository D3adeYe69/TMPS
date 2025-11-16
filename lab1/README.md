# Laboratory Work Nr. 2 — Structural Design Patterns

### Course: Software Design Techniques and Mechanisms
### Author: Chirtoaca Liviu
### Group: FAF-233

## 📘 Topic of the Laboratory Work

**Structural Design Patterns — Extension of Laboratory Work 1**

 Game Character Creation System by integrating four structural design patterns:

- **Facade**
- **Decorator**
- **Adapter**
- **Composite**



### Structural Patterns Help With:

Structural design patterns define how classes and objects are combined to form larger, flexible, and efficient structures.


  

This laboratory extends the system from Lab 1 (Factory Method + Builder + Singleton) by introducing structural patterns that make the system cleaner, more modular, and easier to extend.

## 🏗️ Project Architecture

```
GameCharacterSystem/
├── Client/                  
│   └── Program.cs
├── Domain/                  
│   ├── CharacterBuilder.cs
│   ├── CharacterCreationFacade.cs
│   └── GameManager.cs
├── Factory/                  
│   └── CharacterFactory.cs
├── Models/                 
│   ├── Character.cs
│   ├── ICharacterComponent.cs
│   ├── CharacterDecorator.cs
│   └── CharacterParty.cs
├── Utilities/               
│   └── CharacterDisplayAdapter.cs
└── Data/                    
```

## 🔷 Pattern 1 — Facade

**File**: `Domain/CharacterCreationFacade.cs`

### Purpose:

Provide a simple, unified interface for creating characters, hiding all subsystem complexity:

- factory selection
- builder configuration
- game manager updates

### Why It's Useful

- Client code becomes extremely clean
- You can change internal creation logic without touching the client
- Centralizes and standardizes complex workflows

### Usage Before vs After

 **Before :**
```csharp
CharacterFactory factory = new WarriorFactory();
var character = new CharacterBuilder(factory.CreateCharacter())
    .WithName(name)
    .WithLevel(level)
    .Build();
gameManager.AddCharacter(character);
```

 **After :**
```csharp
var character = facade.CreateCharacter("warrior", name, level, equipment);
```

## 🔷 Pattern 2 — Decorator

**File**: `Models/CharacterDecorator.cs`

### Purpose:

Add dynamic enhancements (weapons, armor, buffs) without modifying existing classes.

### Why It's Useful

- Follows the Open/Closed Principle
- Avoids subclass explosion
- Enhancements can be stacked at runtime
- Characters remain immutable underneath

### Example (stacking decorators):

```csharp
var adapted = new CharacterAdapter(warrior);
var enhanced = new WeaponEnhancementDecorator(adapted, "Excalibur", 15);
var fullyEnhanced = new ArmorEnhancementDecorator(enhanced, "Dragon Scale", 20);

Console.WriteLine(fullyEnhanced.GetTotalStrength());
Console.WriteLine(fullyEnhanced.GetTotalHealth());
```

## 🔷 Pattern 3 — Adapter

**Files:**
- `Utilities/CharacterDisplayAdapter.cs`
- `Models/CharacterAdapter.cs`

### Purpose:

Make incompatible interfaces work together.

### System Uses Two Types of Adapters

#### 1️⃣ Display Format Adapter

Convert character data to:
- **TEXT**
- **JSON**
- **XML**

Switching output format requires ZERO changes to the core logic.

```csharp
ICharacterDisplayAdapter adapter = new JsonDisplayAdapter();
Console.WriteLine(adapter.Format(character));
```

#### 2️⃣ Character → ICharacterComponent Adapter

Allows existing Character objects to work with:
- Decorator
- Composite
- Display adapters

## 🔷 Pattern 4 — Composite

**File**: `Models/CharacterParty.cs`

### Purpose:

Represent part-whole hierarchies such as:
- Parties
- Guilds
- Squads
- Sub-teams

### What It Enables

- Both character and party share the same interface
- You can treat a party as a single unit
- Party can contain sub-parties (recursive structure)

### Example:

```csharp
var party = new CharacterParty("Adventurers");
party.AddMember(new CharacterAdapter(warrior));
party.AddMember(new CharacterAdapter(mage));

Console.WriteLine(party.GetTotalHealth());
Console.WriteLine(party.GetTotalStrength());

var elite = new CharacterParty("Elite Team");
elite.AddMember(new CharacterAdapter(archer));
party.AddMember(elite);  // nested composite!
```

## 🔗 How All Patterns Work Together

1. **Facade** → creates character
2. **Adapter** → adapts character to component interface
3. **Decorator** → adds dynamic equipment stats
4. **Composite** → groups enhanced characters into parties
5. **Display Adapter** → outputs party in any format



## Conclusion:

In this character-creation system, the Facade pattern greatly simplifies the client interface, allowing character creation without exposing internal complexities like factories or builders. The Adapter pattern ensures compatibility between different interfaces, enabling characters to work seamlessly with decorators, composites, and various display formats. The Decorator pattern provides flexible, runtime enhancements such as equipment or buffs without modifying existing classes. Finally, the Composite pattern allows characters to be grouped into parties, enabling hierarchical management while treating individual characters and groups uniformly. Together, these patterns improve maintainability, scalability, and flexibility, making the system easy to extend and manage.
