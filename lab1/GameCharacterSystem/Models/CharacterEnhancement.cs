namespace GameCharacterSystem.Models
{
    /// <summary>
    /// Stores enhancement metadata for a character.
    /// This allows us to apply decorators dynamically without modifying the base character.
    /// </summary>
    public class CharacterEnhancement
    {
        public string? WeaponName { get; set; }
        public int? StrengthBonus { get; set; }
        public string? ArmorName { get; set; }
        public int? HealthBonus { get; set; }

        public bool HasWeapon => !string.IsNullOrEmpty(WeaponName) && StrengthBonus.HasValue;
        public bool HasArmor => !string.IsNullOrEmpty(ArmorName) && HealthBonus.HasValue;
        public bool HasEnhancements => HasWeapon || HasArmor;
    }
}

