# Laboratory Work Nr. 3 — Behavioral Design Patterns

### Course: Theory of Modeling and Programming Systems (TMPS)
### Topic: Behavioral Design Patterns

## 📘 Topic of the Laboratory Work

**Behavioral Design Patterns** — Implementing three behavioral design patterns in a game character system.

This laboratory work demonstrates the implementation and usage of three behavioral design patterns:

- **Observer** — Event notification system
- **Strategy** — Interchangeable attack behaviors
- **Command** — Undoable game actions

## 🏗️ Project Architecture

```
Lab2/
├── Models/
│   └── GameCharacter.cs          # Basic character model
├── Observer/                      # Observer Pattern
│   ├── IGameEventObserver.cs     # Observer interface
│   ├── IGameEventSubject.cs      # Subject interface
│   ├── GameEventManager.cs       # Subject implementation
│   ├── ConsoleLoggerObserver.cs  # Concrete observer (logging)
│   └── AchievementObserver.cs    # Concrete observer (achievements)
├── Strategy/                      # Strategy Pattern
│   ├── IAttackStrategy.cs        # Strategy interface
│   ├── BalancedAttackStrategy.cs
│   ├── AggressiveAttackStrategy.cs
│   ├── DefensiveAttackStrategy.cs
│   ├── SneakAttackStrategy.cs
│   └── CombatContext.cs          # Context using strategies
├── Command/                       # Command Pattern
│   ├── ICommand.cs               # Command interface
│   ├── MoveCommand.cs
│   ├── AttackCommand.cs
│   ├── HealCommand.cs
│   └── CommandHistory.cs         # Command history manager
├── GameManager.cs                 # Central game coordinator
└── Program.cs                     # Main application
```

## 🔷 Pattern 1 — Observer

**Files**: `Observer/` directory

### Purpose:

Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

### Why It's Useful

- **Loose Coupling**: Subject doesn't need to know concrete observer classes
- **Dynamic Relationships**: Observers can be added/removed at runtime
- **Event-Driven Architecture**: Enables reactive programming
- **Separation of Concerns**: Different observers handle different aspects

### Implementation Details

#### Subject (GameEventManager)
- Maintains a list of observers
- Provides methods to subscribe/unsubscribe
- Notifies all observers when events occur

#### Observers
- **ConsoleLoggerObserver**: Logs all game events to console
- **AchievementObserver**: Tracks and unlocks achievements based on events

### Example Usage:

```csharp
// Subscribe observers
eventManager.Subscribe(new ConsoleLoggerObserver());
eventManager.Subscribe(new AchievementObserver());

// Notify observers of events
eventManager.NotifyLevelUp("Hero", 5);
eventManager.NotifyQuestCompleted("Slay Dragon", 100);
```

### Events Tracked:
- Level Up
- Quest Completed
- Character Died
- Health Changed (with critical health warnings)

---

## 🔷 Pattern 2 — Strategy

**Files**: `Strategy/` directory

### Purpose:

Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

### Why It's Useful

- **Runtime Algorithm Selection**: Change behavior at runtime
- **Eliminates Conditional Logic**: No need for if/else chains
- **Open/Closed Principle**: Add new strategies without modifying existing code
- **Single Responsibility**: Each strategy handles one approach

### Implementation Details

#### Strategy Interface (IAttackStrategy)
```csharp
public interface IAttackStrategy
{
    int ExecuteAttack(int baseDamage, int characterLevel);
    string GetDescription();
}
```

#### Concrete Strategies
1. **BalancedAttackStrategy**: Standard damage (100% + level bonus)
2. **AggressiveAttackStrategy**: High damage (150% + level bonus), high risk
3. **DefensiveAttackStrategy**: Moderate damage (80% + level), reduces incoming damage
4. **SneakAttackStrategy**: Low base damage (70% + level), 30% chance for 3x critical hit

#### Context (CombatContext)
- Holds a reference to the current strategy
- Delegates attack execution to the strategy
- Allows strategy switching at runtime

### Example Usage:

```csharp
// Create context with default strategy
var combat = new CombatContext(new BalancedAttackStrategy());

// Change strategy at runtime
combat.SetStrategy(new AggressiveAttackStrategy());

// Execute attack using current strategy
int damage = combat.PerformAttack(50, 5);
```

### Benefits Demonstrated:
- Easy to add new attack types
- Strategies are interchangeable
- No modification needed to existing code when adding strategies

---

## 🔷 Pattern 3 — Command

**Files**: `Command/` directory

### Purpose:

Encapsulate a request as an object, thereby allowing you to parameterize clients with different requests, queue requests, and support undoable operations.

### Why It's Useful

- **Undo/Redo Functionality**: Commands can be reversed
- **Command Queuing**: Commands can be stored and executed later
- **Logging/Auditing**: All commands can be logged
- **Macro Commands**: Combine multiple commands
- **Decoupling**: Invoker doesn't need to know command details

### Implementation Details

#### Command Interface (ICommand)
```csharp
public interface ICommand
{
    void Execute();
    void Undo();
    string GetDescription();
}
```

#### Concrete Commands
1. **MoveCommand**: Moves character to new position (stores old position for undo)
2. **AttackCommand**: Performs attack using current strategy (stores target health for undo)
3. **HealCommand**: Heals character (stores health before healing for undo)

#### Command History
- Maintains a stack of executed commands
- Supports undo (pop from history, execute undo)
- Supports redo (maintains separate redo stack)

### Example Usage:

```csharp
// Create command
var moveCommand = new MoveCommand(character, 10, 20);

// Execute and store in history
commandHistory.ExecuteCommand(moveCommand);

// Undo last command
commandHistory.Undo();

// Redo command
commandHistory.Redo();
```

### Benefits Demonstrated:
- All game actions are undoable
- Commands can be logged for debugging
- Commands can be queued for batch execution
- Easy to add new command types

---

## 🔗 How All Patterns Work Together

1. **Observer** → Notifies about game events (level ups, quests, health changes)
2. **Strategy** → Determines attack behavior (used within AttackCommand)
3. **Command** → Encapsulates actions (attacks, moves, heals) with undo capability

### Integration Example:

```csharp
// 1. Create attack command (Command pattern)
var attackCommand = new AttackCommand(attacker, target, combatContext, baseDamage);

// 2. Execute command (uses Strategy pattern internally)
commandHistory.ExecuteCommand(attackCommand);

// 3. Update health and notify observers (Observer pattern)
gameManager.UpdateCharacterHealth(target);
```

---

## 📚 Other Behavioral Patterns (Not Implemented)

### 4. Chain of Responsibility

**Purpose**: Pass requests along a chain of handlers. Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.

**Use Cases**:
- Request processing pipelines
- Event handling systems
- Middleware in web frameworks
- Exception handling chains

**Example Scenario**: A request validation chain where each handler checks a different aspect (authentication, authorization, validation, rate limiting).

---

### 5. Interpreter

**Purpose**: Define a grammar for a language and provide an interpreter to evaluate sentences in that language.

**Use Cases**:
- Language parsers (SQL, regular expressions)
- Expression evaluators
- Rule engines
- Configuration file parsers

**Example Scenario**: A game scripting language where players can write simple commands like "if health < 50 then use potion".

---

### 6. Iterator

**Purpose**: Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.

**Use Cases**:
- Traversing collections
- Hiding collection implementation details
- Providing uniform interface for different collections
- Lazy evaluation

**Example Scenario**: Iterating over game entities (characters, items, enemies) without knowing the internal storage structure.

---

### 7. Mediator

**Purpose**: Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly.

**Use Cases**:
- Chat systems (mediator coordinates messages)
- UI components (dialog boxes, forms)
- Air traffic control systems
- Complex object interactions

**Example Scenario**: A game lobby where a mediator coordinates communication between players, handles matchmaking, and manages game sessions.

---

### 8. Memento

**Purpose**: Without violating encapsulation, capture and externalize an object's internal state so that the object can be restored to this state later.

**Use Cases**:
- Save/load game state
- Undo/redo functionality (alternative to Command)
- Checkpoint systems
- State snapshots

**Example Scenario**: Saving game progress where the entire game state is captured and can be restored later.

---

### 9. State

**Purpose**: Allow an object to alter its behavior when its internal state changes. The object will appear to have changed its class.

**Use Cases**:
- State machines
- Game character states (idle, walking, attacking, dead)
- Workflow management
- UI component states

**Example Scenario**: A character that behaves differently based on state (idle, combat, fleeing, dead) with state transitions.

---

### 10. Template Method

**Purpose**: Define the skeleton of an algorithm in an operation, deferring some steps to subclasses. Template Method lets subclasses redefine certain steps of an algorithm without changing the algorithm's structure.

**Use Cases**:
- Framework design
- Algorithm variations
- Code reuse
- Defining workflow steps

**Example Scenario**: A character creation process with fixed steps (validate input, create character, initialize stats, add to game) where subclasses can customize specific steps.

---

### 11. Visitor

**Purpose**: Represent an operation to be performed on elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates.

**Use Cases**:
- Compiler design (AST traversal)
- Report generation
- Type checking
- Code analysis tools

**Example Scenario**: A game entity visitor that calculates total damage, counts items, or generates statistics without modifying entity classes.

---

## 🎮 Running the Application

1. Build the project:
   ```bash
   dotnet build
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Follow the interactive menu to:
   - Create characters
   - Perform attacks (demonstrates Strategy + Command)
   - Change attack strategies (demonstrates Strategy)
   - Move characters (demonstrates Command)
   - Undo/Redo actions (demonstrates Command)
   - Complete quests (demonstrates Observer)
   - View achievements (demonstrates Observer)

## 📝 Key Takeaways

### Observer Pattern
- ✅ Loose coupling between subject and observers
- ✅ Dynamic subscription/unsubscription
- ✅ Multiple observers can react to same event differently
- ✅ Event-driven architecture

### Strategy Pattern
- ✅ Runtime algorithm selection
- ✅ Easy to add new strategies
- ✅ Eliminates conditional logic
- ✅ Each strategy is independently testable

### Command Pattern
- ✅ Undo/redo functionality
- ✅ Command queuing and logging
- ✅ Decouples invoker from receiver
- ✅ Macro commands support

## 🎯 Conclusion

This laboratory work successfully demonstrates three behavioral design patterns working together in a cohesive game system:

- **Observer** enables event-driven notifications for game events
- **Strategy** provides flexible, interchangeable attack behaviors
- **Command** encapsulates actions with undo/redo capability

Together, these patterns create a flexible, maintainable, and extensible system that follows SOLID principles and demonstrates best practices in object-oriented design.

