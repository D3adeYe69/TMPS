namespace GameCharacterSystem.Models
{
    public abstract class Character
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public Dictionary<string, string> Equipment { get; set; }
        
        // Store enhancement metadata to apply decorators dynamically
        public CharacterEnhancement Enhancement { get; set; }

        protected Character()
        {
            Equipment = new Dictionary<string, string>();
            Enhancement = new CharacterEnhancement();
        }

        public abstract string GetCharacterType();
    }

    public class Warrior : Character
    {
        public override string GetCharacterType() => "Warrior";
    }

    public class Mage : Character
    {
        public override string GetCharacterType() => "Mage";
    }

    public class Archer : Character
    {
        public override string GetCharacterType() => "Archer";
    }
}