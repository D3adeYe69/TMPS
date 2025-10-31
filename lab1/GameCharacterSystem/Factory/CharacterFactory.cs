using GameCharacterSystem.Models;

namespace GameCharacterSystem.Factory
{
    public abstract class CharacterFactory
    {
        protected static readonly Random random = new Random();
        public abstract Character CreateCharacter();
    }

    public class WarriorFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Warrior
            {
                Health = random.Next(90, 120),    // Warriors have high health
                Strength = random.Next(14, 20)    // Warriors have high strength
            };
        }
    }

    public class MageFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Mage
            {
                Health = random.Next(60, 80),     // Mages have lower health
                Strength = random.Next(6, 10)     // Mages have lower strength
            };
        }
    }

    public class ArcherFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Archer
            {
                Health = random.Next(75, 95),     // Archers have medium health
                Strength = random.Next(10, 15)    // Archers have medium strength
            };
        }
    }
}