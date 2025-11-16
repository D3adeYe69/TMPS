using GameCharacterSystem.Domain;
using GameCharacterSystem.Models;
using GameCharacterSystem.Utilities;

namespace GameCharacterSystem.Client
{
    /// <summary>
    /// Single client for the entire system.
    /// All object creation is hidden behind the Facade pattern.
    /// </summary>
    public class Program
    {
        private static CharacterCreationFacade facade = null!;
        private static ICharacterDisplayAdapter displayAdapter = null!;

        public static void Main()
        {
            // Initialize facade - this hides all object creation complexity
            facade = new CharacterCreationFacade();
            
            // Initialize display adapter - default to text format
            displayAdapter = new TextDisplayAdapter();
            
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Game Character Management System ===");
                Console.WriteLine("1. Create a new character");
                Console.WriteLine("2. View all characters");
                Console.WriteLine("3. Enhance character with equipment");
                Console.WriteLine("4. Create character party");
                Console.WriteLine("5. Change display format");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCharacter();
                        break;
                    case "2":
                        DisplayCharacters();
                        break;
                    case "3":
                        EnhanceCharacter();
                        break;
                    case "4":
                        CreateParty();
                        break;
                    case "5":
                        ChangeDisplayFormat();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Character creation using Facade - no factories or builders exposed to client.
        /// </summary>
        private static void CreateCharacter()
        {
            Console.WriteLine("\n=== Character Creation ===");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Archer");
            Console.Write("Choose character type: ");

            string? typeChoice = Console.ReadLine();
            string characterType = typeChoice switch
            {
                "1" => "warrior",
                "2" => "mage",
                "3" => "archer",
                _ => ""
            };

            if (string.IsNullOrEmpty(characterType))
            {
                Console.WriteLine("Invalid character type.");
                return;
            }

            Console.Write("Enter character name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter character level (1-100): ");
            if (!int.TryParse(Console.ReadLine(), out int level) || level < 1 || level > 100)
            {
                Console.WriteLine("Invalid level. Must be between 1 and 100.");
                return;
            }

            // Equipment selection
            var equipment = new Dictionary<string, string>();
            Console.WriteLine("\nEquipment Selection (optional):");
            
            Console.Write("Enter weapon name (or press Enter to skip): ");
            string? weapon = Console.ReadLine();
            if (!string.IsNullOrEmpty(weapon))
            {
                equipment["Weapon"] = weapon;
            }

            Console.Write("Enter armor name (or press Enter to skip): ");
            string? armor = Console.ReadLine();
            if (!string.IsNullOrEmpty(armor))
            {
                equipment["Armor"] = armor;
            }

            try
            {
                // Facade handles all the complexity - factories, builders, game manager
                var character = facade.CreateCharacter(characterType, name, level, equipment);
                Console.WriteLine($"\nCharacter '{name}' created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating character: {ex.Message}");
            }
        }

        /// <summary>
        /// Display characters using the display adapter pattern.
        /// Decorators are applied here to show enhanced characters properly.
        /// </summary>
        private static void DisplayCharacters()
        {
            var characters = facade.GetAllCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters created yet.");
                return;
            }

            Console.WriteLine($"\n=== All Characters ({characters.Count}) ===");
            for (int i = 0; i < characters.Count; i++)
            {
                // Get character with decorators applied (proper decorator pattern usage)
                var enhancedCharacter = facade.GetEnhancedCharacter(characters[i]);
                Console.WriteLine($"\n--- Character #{i + 1} ---");
                Console.WriteLine(displayAdapter.Format(enhancedCharacter));
            }
        }

        /// <summary>
        /// Demonstrates Decorator pattern - dynamically adding enhancements.
        /// The decorator pattern is used to calculate enhancements, then they are applied to the character.
        /// </summary>
        private static void EnhanceCharacter()
        {
            var characters = facade.GetAllCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters available to enhance. Create a character first.");
                return;
            }

            Console.WriteLine("\n=== Enhance Character ===");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].GetCharacterType()})");
            }
            Console.Write("Select character number: ");

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > characters.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            var selectedCharacter = characters[index - 1];
            var adaptedCharacter = new CharacterAdapter(selectedCharacter);

            Console.WriteLine("\nEnhancement options:");
            Console.WriteLine("1. Add weapon enhancement");
            Console.WriteLine("2. Add armor enhancement");
            Console.WriteLine("3. Add both");
            Console.Write("Choose option: ");

            string? choice = Console.ReadLine();
            string? weaponName = null;
            int? strengthBonus = null;
            string? armorName = null;
            int? healthBonus = null;

            // Collect enhancement information
            switch (choice)
            {
                case "1":
                    Console.Write("Enter weapon name: ");
                    weaponName = Console.ReadLine();
                    Console.Write("Enter strength bonus: ");
                    if (int.TryParse(Console.ReadLine(), out int strBonus))
                    {
                        strengthBonus = strBonus;
                    }
                    break;
                case "2":
                    Console.Write("Enter armor name: ");
                    armorName = Console.ReadLine();
                    Console.Write("Enter health bonus: ");
                    if (int.TryParse(Console.ReadLine(), out int hb))
                    {
                        healthBonus = hb;
                    }
                    break;
                case "3":
                    Console.Write("Enter weapon name: ");
                    weaponName = Console.ReadLine();
                    Console.Write("Enter strength bonus: ");
                    if (int.TryParse(Console.ReadLine(), out int sb))
                    {
                        strengthBonus = sb;
                    }
                    Console.Write("Enter armor name: ");
                    armorName = Console.ReadLine();
                    Console.Write("Enter health bonus: ");
                    if (int.TryParse(Console.ReadLine(), out int hb2))
                    {
                        healthBonus = hb2;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            // Store enhancement metadata (not modifying base character directly)
            facade.ApplyEnhancement(selectedCharacter, weaponName, strengthBonus, armorName, healthBonus);

            // Get enhanced character with decorators applied (proper decorator pattern)
            var enhancedCharacter = facade.GetEnhancedCharacter(selectedCharacter);

            Console.WriteLine("\n=== Enhanced Character ===");
            Console.WriteLine(displayAdapter.Format(enhancedCharacter));
            Console.WriteLine("\nEnhancements have been applied and saved!");
            Console.WriteLine("(Base character unchanged - decorators applied dynamically)");
        }

        /// <summary>
        /// Demonstrates Composite pattern - creating character parties.
        /// </summary>
        private static void CreateParty()
        {
            var characters = facade.GetAllCharacters();
            if (characters.Count < 2)
            {
                Console.WriteLine("\nNeed at least 2 characters to create a party.");
                return;
            }

            Console.WriteLine("\n=== Create Character Party ===");
            Console.Write("Enter party name: ");
            string? partyName = Console.ReadLine();
            if (string.IsNullOrEmpty(partyName))
            {
                Console.WriteLine("Party name cannot be empty.");
                return;
            }

            var party = new CharacterParty(partyName);

            Console.WriteLine("\nAvailable characters:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].GetCharacterType()})");
            }
            Console.WriteLine("Enter character numbers to add to party (comma-separated, e.g., 1,2,3): ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No characters selected.");
                return;
            }

            var indices = input.Split(',')
                .Select(s => int.TryParse(s.Trim(), out int idx) ? idx : -1)
                .Where(idx => idx >= 1 && idx <= characters.Count)
                .Select(idx => idx - 1)
                .Distinct()
                .ToList();

            if (indices.Count == 0)
            {
                Console.WriteLine("No valid characters selected.");
                return;
            }

            foreach (var idx in indices)
            {
                // Use enhanced characters (with decorators applied) in the party
                party.AddMember(facade.GetEnhancedCharacter(characters[idx]));
            }

            Console.WriteLine("\n=== Party Information ===");
            Console.WriteLine(displayAdapter.Format(party));
        }

        /// <summary>
        /// Demonstrates Adapter pattern - switching between display formats.
        /// </summary>
        private static void ChangeDisplayFormat()
        {
            Console.WriteLine("\n=== Change Display Format ===");
            Console.WriteLine("1. Text format");
            Console.WriteLine("2. JSON format");
            Console.WriteLine("3. XML format");
            Console.Write("Choose format: ");

            string? choice = Console.ReadLine();
            displayAdapter = choice switch
            {
                "1" => new TextDisplayAdapter(),
                "2" => new JsonDisplayAdapter(),
                "3" => new XmlDisplayAdapter(),
                _ => displayAdapter
            };

            Console.WriteLine($"Display format changed to: {displayAdapter.GetType().Name.Replace("DisplayAdapter", "")}");
        }
    }
}

