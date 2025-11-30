using Lab2.Models;
using Lab2.Observer;
using Lab2.Strategy;
using Lab2.Command;

namespace Lab2
{
    /// <summary>
    /// Main program demonstrating three behavioral design patterns:
    /// 1. Observer - Game event notifications
    /// 2. Strategy - Different attack behaviors
    /// 3. Command - Undoable game actions
    /// </summary>
    class Program
    {
        private static GameManager? _gameManager;
        private static CommandHistory? _commandHistory;
        private static CombatContext? _combatContext;
        private static ConsoleLoggerObserver? _loggerObserver;
        private static AchievementObserver? _achievementObserver;

        static void Main(string[] args)
        {
            InitializeGame();
            
            // Dictionary mapping menu choices to actions
            var menuActions = new Dictionary<string, Func<bool>>
            {
                { "1", () => { CreateCharacter(); return true; } },
                { "2", () => { DisplayCharacters(); return true; } },
                { "3", () => { PerformAttack(); return true; } },
                { "4", () => { ChangeAttackStrategy(); return true; } },
                { "5", () => { MoveCharacter(); return true; } },
                { "6", () => { HealCharacter(); return true; } },
                { "7", () => { UndoLastCommand(); return true; } },
                { "8", () => { RedoCommand(); return true; } },
                { "9", () => { CompleteQuest(); return true; } },
                { "10", () => { DisplayAchievements(); return true; } },
                { "11", () => false } // Exit
            };
            
            bool running = true;
            while (running)
            {
                DisplayMainMenu();
                string? choice = Console.ReadLine();

                if (choice != null && menuActions.TryGetValue(choice, out var action))
                {
                    running = action();
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }

        static void InitializeGame()
        {
            Console.WriteLine("=== Initializing Game System ===\n");
            
            // Initialize game manager
            _gameManager = new GameManager();
            
            // Initialize command history (Command pattern)
            _commandHistory = new CommandHistory();
            
            // Initialize combat context with default strategy (Strategy pattern)
            _combatContext = new CombatContext(new BalancedAttackStrategy());
            
            // Initialize observers (Observer pattern)
            _loggerObserver = new ConsoleLoggerObserver();
            _achievementObserver = new AchievementObserver();
            
            // Subscribe observers to game events
            _gameManager.EventManager.Subscribe(_loggerObserver);
            _gameManager.EventManager.Subscribe(_achievementObserver);
            
            Console.WriteLine("Game system initialized with:");
            Console.WriteLine("- Observer pattern: Event notifications active");
            Console.WriteLine("- Strategy pattern: Combat system ready");
            Console.WriteLine("- Command pattern: Action history enabled\n");
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n=== Game Character System (Behavioral Patterns Demo) ===");
            Console.WriteLine("1. Create a new character");
            Console.WriteLine("2. Display all characters");
            Console.WriteLine("3. Perform attack (Command + Strategy)");
            Console.WriteLine("4. Change attack strategy (Strategy pattern)");
            Console.WriteLine("5. Move character (Command pattern)");
            Console.WriteLine("6. Heal character (Command pattern)");
            Console.WriteLine("7. Undo last command (Command pattern)");
            Console.WriteLine("8. Redo command (Command pattern)");
            Console.WriteLine("9. Complete quest (Observer pattern)");
            Console.WriteLine("10. Display achievements (Observer pattern)");
            Console.WriteLine("11. Exit");
            Console.Write("Choose an option: ");
        }

        static void CreateCharacter()
        {
            Console.WriteLine("\n=== Create Character ===");
            Console.Write("Enter character name: ");
            string? name = Console.ReadLine();
            
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter starting level (1-10): ");
            if (!int.TryParse(Console.ReadLine(), out int level) || level < 1 || level > 10)
            {
                level = 1;
            }

            var character = new GameCharacter(name, level);
            _gameManager!.AddCharacter(character);
            Console.WriteLine($"Character '{name}' created successfully!");
        }

        static void DisplayCharacters()
        {
            var characters = _gameManager!.GetCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters created yet.");
                return;
            }

            Console.WriteLine("\n=== All Characters ===");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i]}");
            }
        }

        static void PerformAttack()
        {
            var characters = _gameManager!.GetCharacters();
            if (characters.Count < 2)
            {
                Console.WriteLine("\nNeed at least 2 characters to perform an attack.");
                return;
            }

            Console.WriteLine("\n=== Perform Attack ===");
            Console.WriteLine("Select attacker:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name}");
            }
            Console.Write("Attacker number: ");
            
            if (!int.TryParse(Console.ReadLine(), out int attackerIdx) || attackerIdx < 1 || attackerIdx > characters.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.WriteLine("\nSelect target:");
            for (int i = 0; i < characters.Count; i++)
            {
                if (i != attackerIdx - 1)
                {
                    Console.WriteLine($"{i + 1}. {characters[i].Name}");
                }
            }
            Console.Write("Target number: ");
            
            if (!int.TryParse(Console.ReadLine(), out int targetIdx) || targetIdx < 1 || targetIdx > characters.Count || targetIdx == attackerIdx)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var attacker = characters[attackerIdx - 1];
            var target = characters[targetIdx - 1];
            int baseDamage = 20 + (attacker.Level * 5);

            // Create and execute attack command (Command pattern)
            var attackCommand = new AttackCommand(attacker, target, _combatContext!, baseDamage);
            _commandHistory!.ExecuteCommand(attackCommand);

            // Update health and notify observers
            _gameManager.UpdateCharacterHealth(target);
        }

        static void ChangeAttackStrategy()
        {
            Console.WriteLine("\n=== Change Attack Strategy ===");
            Console.WriteLine("1. Balanced (100% damage + level)");
            Console.WriteLine("2. Aggressive (150% damage + level, high risk)");
            Console.WriteLine("3. Defensive (80% damage + level, reduces incoming damage)");
            Console.WriteLine("4. Sneak Attack (70% damage + level, 30% crit chance)");
            Console.Write("Choose strategy: ");

            string? choice = Console.ReadLine();
            IAttackStrategy? strategy = choice switch
            {
                "1" => new BalancedAttackStrategy(),
                "2" => new AggressiveAttackStrategy(),
                "3" => new DefensiveAttackStrategy(),
                "4" => new SneakAttackStrategy(),
                _ => null
            };

            if (strategy != null)
            {
                _combatContext!.SetStrategy(strategy);
                Console.WriteLine($"Current strategy: {strategy.GetDescription()}");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        static void MoveCharacter()
        {
            var characters = _gameManager!.GetCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters available.");
                return;
            }

            Console.WriteLine("\n=== Move Character ===");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} (Current: {characters[i].PositionX}, {characters[i].PositionY})");
            }
            Console.Write("Select character: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > characters.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.Write("Enter new X position: ");
            if (!int.TryParse(Console.ReadLine(), out int x))
            {
                Console.WriteLine("Invalid X position.");
                return;
            }

            Console.Write("Enter new Y position: ");
            if (!int.TryParse(Console.ReadLine(), out int y))
            {
                Console.WriteLine("Invalid Y position.");
                return;
            }

            var character = characters[idx - 1];
            var moveCommand = new MoveCommand(character, x, y);
            _commandHistory!.ExecuteCommand(moveCommand);
        }

        static void HealCharacter()
        {
            var characters = _gameManager!.GetCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters available.");
                return;
            }

            Console.WriteLine("\n=== Heal Character ===");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} (HP: {characters[i].Health}/{characters[i].MaxHealth})");
            }
            Console.Write("Select character: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > characters.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.Write("Enter heal amount: ");
            if (!int.TryParse(Console.ReadLine(), out int healAmount) || healAmount < 1)
            {
                Console.WriteLine("Invalid heal amount.");
                return;
            }

            var character = characters[idx - 1];
            var healCommand = new HealCommand(character, healAmount);
            _commandHistory!.ExecuteCommand(healCommand);
        }

        static void UndoLastCommand()
        {
            Console.WriteLine("\n=== Undo Last Command ===");
            if (_commandHistory!.GetHistoryCount() == 0)
            {
                Console.WriteLine("No commands to undo.");
                return;
            }
            _commandHistory.Undo();
        }

        static void RedoCommand()
        {
            Console.WriteLine("\n=== Redo Command ===");
            _commandHistory!.Redo();
        }

        static void CompleteQuest()
        {
            var characters = _gameManager!.GetCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters available.");
                return;
            }

            Console.WriteLine("\n=== Complete Quest ===");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name}");
            }
            Console.Write("Select character: ");

            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > characters.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            Console.Write("Enter quest name: ");
            string? questName = Console.ReadLine();
            if (string.IsNullOrEmpty(questName))
            {
                questName = "Default Quest";
            }

            Console.Write("Enter experience reward: ");
            if (!int.TryParse(Console.ReadLine(), out int reward) || reward < 1)
            {
                reward = 50;
            }

            var character = characters[idx - 1];
            _gameManager!.CompleteQuest(questName, reward, character.Name);
        }

        static void DisplayAchievements()
        {
            _achievementObserver!.DisplayAchievements();
        }

      
        
    }
}
