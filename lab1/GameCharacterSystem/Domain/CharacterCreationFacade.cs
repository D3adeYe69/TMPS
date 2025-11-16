using GameCharacterSystem.Factory;
using GameCharacterSystem.Models;

namespace GameCharacterSystem.Domain
{
    /// <summary>
    /// Facade Pattern: Simplifies the complex character creation process
    /// by hiding factories, builders, and game manager from the client.
    /// </summary>
    public class CharacterCreationFacade
    {
        private readonly GameManager gameManager;

        public CharacterCreationFacade()
        {
            gameManager = GameManager.Instance;
        }

        /// <summary>
        /// Creates a character with all necessary steps hidden from the client.
        /// The client only needs to provide basic information.
        /// </summary>
        public Character CreateCharacter(string characterType, string name, int level, Dictionary<string, string>? equipment = null)
        {
            // Hide factory selection from client
            CharacterFactory factory = GetFactoryForType(characterType);
            if (factory == null)
            {
                throw new ArgumentException($"Invalid character type: {characterType}");
            }

            // Hide builder usage from client
            var builder = new CharacterBuilder(factory.CreateCharacter())
                .WithName(name)
                .WithLevel(level);

            // Add equipment if provided
            if (equipment != null)
            {
                foreach (var item in equipment)
                {
                    builder.WithEquipment(item.Key, item.Value);
                }
            }

            var character = builder.Build();
            
            // Hide game manager interaction from client
            gameManager.AddCharacter(character);
            
            return character;
        }

        /// <summary>
        /// Gets all characters managed by the system.
        /// </summary>
        public List<Character> GetAllCharacters()
        {
            return gameManager.GetAllCharacters();
        }

        /// <summary>
        /// Stores enhancement metadata for a character.
        /// The decorator pattern will be applied when displaying/accessing the character,
        /// keeping the base character unchanged (proper decorator pattern usage).
        /// </summary>
        public void ApplyEnhancement(Character character, string? weaponName, int? strengthBonus, string? armorName, int? healthBonus)
        {
            // Store enhancement metadata (not applying directly to character)
            if (!string.IsNullOrEmpty(weaponName) && strengthBonus.HasValue)
            {
                character.Enhancement.WeaponName = weaponName;
                character.Enhancement.StrengthBonus = strengthBonus.Value;
                character.Equipment["Weapon"] = weaponName;
            }

            if (!string.IsNullOrEmpty(armorName) && healthBonus.HasValue)
            {
                character.Enhancement.ArmorName = armorName;
                character.Enhancement.HealthBonus = healthBonus.Value;
                character.Equipment["Armor"] = armorName;
            }
        }

        /// <summary>
        /// Gets a character with decorators applied based on stored enhancements.
        /// This properly demonstrates the decorator pattern - decorators wrap the character
        /// and add behavior without modifying the base character.
        /// </summary>
        public ICharacterComponent GetEnhancedCharacter(Character character)
        {
            var adapted = new CharacterAdapter(character);
            ICharacterComponent enhanced = adapted;

            // Apply decorators based on stored enhancement metadata
            if (character.Enhancement.HasWeapon)
            {
                enhanced = new WeaponEnhancementDecorator(
                    enhanced, 
                    character.Enhancement.WeaponName!, 
                    character.Enhancement.StrengthBonus!.Value);
            }

            if (character.Enhancement.HasArmor)
            {
                enhanced = new ArmorEnhancementDecorator(
                    enhanced, 
                    character.Enhancement.ArmorName!, 
                    character.Enhancement.HealthBonus!.Value);
            }

            return enhanced;
        }

        /// <summary>
        /// Internal method to select appropriate factory - hidden from client.
        /// </summary>
        private CharacterFactory GetFactoryForType(string type)
        {
            return type.ToLower() switch
            {
                "warrior" or "1" => new WarriorFactory(),
                "mage" or "2" => new MageFactory(),
                "archer" or "3" => new ArcherFactory(),
                _ => null!
            };
        }
    }
}

