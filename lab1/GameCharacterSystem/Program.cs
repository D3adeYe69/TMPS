using GameCharacterSystem.Domain;
using GameCharacterSystem.Factory;
using GameCharacterSystem.Models;

namespace GameCharacterSystem
{
    public class Program
    {
        public static void Main()
        {
            var gameManager = GameManager.Instance;
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nGame Character Creation System");
                Console.WriteLine("1. Create a new character");
                Console.WriteLine("2. View all characters");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateCharacter(gameManager);
                        break;
                    case "2":
                        DisplayCharacters(gameManager);
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void CreateCharacter(GameManager gameManager)
        {
            Console.WriteLine("\nCharacter Creation");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Archer");
            Console.Write("Choose character type: ");

            CharacterFactory? factory = null;
            string? typeChoice = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                    factory = new WarriorFactory();
                    break;
                case "2":
                    factory = new MageFactory();
                    break;
                case "3":
                    factory = new ArcherFactory();
                    break;
                default:
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

            var character = new CharacterBuilder(factory.CreateCharacter())
                .WithName(name)
                .WithLevel(level);

            // Equipment selection
            Console.WriteLine("\nEquipment Selection");
            
            Console.Write("Enter weapon name: ");
            string? weapon = Console.ReadLine();
            if (!string.IsNullOrEmpty(weapon))
            {
                character.WithEquipment("Weapon", weapon);
            }

            Console.Write("Enter armor name: ");
            string? armor = Console.ReadLine();
            if (!string.IsNullOrEmpty(armor))
            {
                character.WithEquipment("Armor", armor);
            }

            var builtCharacter = character.Build();
            gameManager.AddCharacter(builtCharacter);
            Console.WriteLine("\nCharacter created successfully!");
        }

        private static void DisplayCharacters(GameManager gameManager)
        {
            var characters = gameManager.GetAllCharacters();
            if (characters.Count == 0)
            {
                Console.WriteLine("\nNo characters created yet.");
                return;
            }

            Console.WriteLine("\nAll Characters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"\nName: {character.Name}");
                Console.WriteLine($"Type: {character.GetCharacterType()}");
                Console.WriteLine($"Level: {character.Level}");
                Console.WriteLine($"Health: {character.Health}");
                Console.WriteLine($"Strength: {character.Strength}");
                Console.WriteLine("Equipment:");
                foreach (var equipment in character.Equipment)
                {
                    Console.WriteLine($"  {equipment.Key}: {equipment.Value}");
                }
            }
        }
    }
}