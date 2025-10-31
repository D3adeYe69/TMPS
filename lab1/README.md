# Laboratory Work Nr.1 - Creational Design Patterns

### Course: Software Design Techniques and Mechanisms
### Author: Chirtoaca Liviu
### Group: FAF-233

## Objectives
1. Study and understand the Creational Design Patterns
2. Choose a domain, define its main classes/models/entities and choose the appropriate instantiation mechanisms
3. Use some creational design patterns for object instantiation in a sample project

## Implementation

In this project, I'll implement a simple Game Character Creation System that demonstrates the use of several creational design patterns. The system will allow creating different types of game characters with various attributes and equipment.

### Design Patterns Used:

1. **Singleton Pattern**
   - Used for the GameManager class that maintains the global state of the game
   - Ensures only one instance of the game manager exists

2. **Factory Method Pattern**
   - Used for creating different types of characters (Warrior, Mage, Archer)
   - Each character type has its own factory implementation

3. **Builder Pattern**
   - Used for constructing complex character objects with many optional parameters
   - Allows step-by-step creation of characters with different equipment and attributes

### Project Structure

The project is organized into several modules:

- `domain/` - Contains the core game logic and interfaces
- `factory/` - Contains the factory implementations for character creation
- `models/` - Contains the concrete character classes and related models

### Code Examples

1. **Singleton Pattern Implementation**
```csharp
public sealed class GameManager
{
    private static GameManager? instance;
    private static readonly object padlock = new object();
    
    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            lock (padlock)
            {
                instance ??= new GameManager();
                return instance;
            }
        }
    }
}
```

2. **Factory Method Pattern Implementation**
```csharp
public abstract class CharacterFactory
{
    public abstract Character CreateCharacter();
}

public class WarriorFactory : CharacterFactory
{
    public override Character CreateCharacter()
    {
        return new Warrior
        {
            Health = 100,
            Strength = 15,
            Level = 1
        };
    }
}
```

3. **Builder Pattern Implementation**
```csharp
var warrior = new CharacterBuilder(warriorFactory.CreateCharacter())
    .WithName("Conan")
    .WithLevel(1)
    .WithEquipment("Weapon", "Sword")
    .WithEquipment("Armor", "Plate Mail")
    .Build();
```

## Conclusions

Through this laboratory work, I gained practical experience in implementing creational design patterns. Each pattern solved specific problems:

- Singleton ensured global state management
- Factory Method provided flexibility in character creation
- Builder simplified the construction of complex character objects

These patterns helped create more maintainable and flexible code by encapsulating object creation logic and providing clear interfaces for object instantiation.