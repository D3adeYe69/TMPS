namespace Lab2.Models
{
    /// <summary>
    /// Represents a game character with basic properties.
    /// </summary>
    public class GameCharacter
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public GameCharacter(string name, int level = 1)
        {
            Name = name;
            Level = level;
            Experience = 0;
            MaxHealth = 100 + (level * 10);
            Health = MaxHealth;
            PositionX = 0;
            PositionY = 0;
        }

        public void AddExperience(int exp)
        {
            Experience += exp;
            // Level up at 100 experience per level
            int newLevel = (Experience / 100) + 1;
            if (newLevel > Level)
            {
                Level = newLevel;
                MaxHealth = 100 + (Level * 10);
                Health = MaxHealth;
            }
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
        }

        public override string ToString()
        {
            return $"{Name} (Level {Level}, HP: {Health}/{MaxHealth}, Exp: {Experience}, Position: ({PositionX}, {PositionY}))";
        }
    }
}

