using GameCharacterSystem.Domain;
using GameCharacterSystem.Factory;

namespace GameCharacterSystem
{
    public class Program
    {
        public static void Main()
        {
            // Singleton usage
            var gameManager = GameManager.Instance;

            // Factory Method usage
            CharacterFactory warriorFactory = new WarriorFactory();
            CharacterFactory mageFactory = new MageFactory();
            CharacterFactory archerFactory = new ArcherFactory();

            // Builder usage
            var warrior = new CharacterBuilder(warriorFactory.CreateCharacter())
                .WithName("Conan")
                .WithLevel(1)
                .WithEquipment("Weapon", "Sword")
                .WithEquipment("Armor", "Plate Mail")
                .Build();

            var mage = new CharacterBuilder(mageFactory.CreateCharacter())
                .WithName("Gandalf")
                .WithLevel(1)
                .WithEquipment("Weapon", "Staff")
                .WithEquipment("Armor", "Robe")
                .Build();

            var archer = new CharacterBuilder(archerFactory.CreateCharacter())
                .WithName("Legolas")
                .WithLevel(1)
                .WithEquipment("Weapon", "Bow")
                .WithEquipment("Armor", "Leather Armor")
                .Build();

            // Add characters to game manager
            gameManager.AddCharacter(warrior);
            gameManager.AddCharacter(mage);
            gameManager.AddCharacter(archer);

            // Display all characters
            var characters = gameManager.GetAllCharacters();
            foreach (var character in characters)
            {
                Console.WriteLine($"Name: {character.Name}");
                Console.WriteLine($"Type: {character.GetCharacterType()}");
                Console.WriteLine($"Level: {character.Level}");
                Console.WriteLine($"Health: {character.Health}");
                Console.WriteLine($"Strength: {character.Strength}");
                Console.WriteLine("Equipment:");
                foreach (var equipment in character.Equipment)
                {
                    Console.WriteLine($"  {equipment.Key}: {equipment.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}