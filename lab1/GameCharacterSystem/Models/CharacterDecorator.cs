namespace GameCharacterSystem.Models
{
    /// <summary>
    /// Decorator Pattern: Base decorator class that allows dynamic addition
    /// of equipment and enhancements to characters at runtime.
    /// </summary>
    public abstract class CharacterDecorator : ICharacterComponent
    {
        protected ICharacterComponent character;

        protected CharacterDecorator(ICharacterComponent character)
        {
            this.character = character;
        }

        public virtual string GetDescription()
        {
            return character.GetDescription();
        }

        public virtual int GetTotalHealth()
        {
            return character.GetTotalHealth();
        }

        public virtual int GetTotalStrength()
        {
            return character.GetTotalStrength();
        }

        public virtual Dictionary<string, string> GetEquipment()
        {
            return character.GetEquipment();
        }
    }

    /// <summary>
    /// Concrete decorator that adds weapon enhancements.
    /// </summary>
    public class WeaponEnhancementDecorator : CharacterDecorator
    {
        private readonly string weaponName;
        private readonly int strengthBonus;

        public WeaponEnhancementDecorator(ICharacterComponent character, string weaponName, int strengthBonus) 
            : base(character)
        {
            this.weaponName = weaponName;
            this.strengthBonus = strengthBonus;
        }

        public override string GetDescription()
        {
            return $"{character.GetDescription()}, Enhanced with {weaponName}";
        }

        public override int GetTotalStrength()
        {
            return character.GetTotalStrength() + strengthBonus;
        }

        public override Dictionary<string, string> GetEquipment()
        {
            var equipment = character.GetEquipment();
            equipment["Weapon"] = weaponName;
            return equipment;
        }
    }

    /// <summary>
    /// Concrete decorator that adds armor enhancements.
    /// </summary>
    public class ArmorEnhancementDecorator : CharacterDecorator
    {
        private readonly string armorName;
        private readonly int healthBonus;

        public ArmorEnhancementDecorator(ICharacterComponent character, string armorName, int healthBonus) 
            : base(character)
        {
            this.armorName = armorName;
            this.healthBonus = healthBonus;
        }

        public override string GetDescription()
        {
            return $"{character.GetDescription()}, Protected by {armorName}";
        }

        public override int GetTotalHealth()
        {
            return character.GetTotalHealth() + healthBonus;
        }

        public override Dictionary<string, string> GetEquipment()
        {
            var equipment = character.GetEquipment();
            equipment["Armor"] = armorName;
            return equipment;
        }
    }

    /// <summary>
    /// Adapter: Adapts Character class to ICharacterComponent interface.
    /// </summary>
    public class CharacterAdapter : ICharacterComponent
    {
        private readonly Character character;

        public CharacterAdapter(Character character)
        {
            this.character = character;
        }

        public string GetDescription()
        {
            return $"{character.Name} ({character.GetCharacterType()}) - Level {character.Level}";
        }

        public int GetTotalHealth()
        {
            return character.Health;
        }

        public int GetTotalStrength()
        {
            return character.Strength;
        }

        public Dictionary<string, string> GetEquipment()
        {
            return new Dictionary<string, string>(character.Equipment);
        }

        public Character GetCharacter()
        {
            return character;
        }
    }
}

