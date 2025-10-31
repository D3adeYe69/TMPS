using GameCharacterSystem.Models;

namespace GameCharacterSystem.Factory
{
    public abstract class CharacterFactory
    {
        public abstract Character CreateCharacter();
    }

    public class WarriorFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Warrior
            {
                Health = 100,
                Strength = 15,
                Level = 1
            };
        }
    }

    public class MageFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Mage
            {
                Health = 70,
                Strength = 8,
                Level = 1
            };
        }
    }

    public class ArcherFactory : CharacterFactory
    {
        public override Character CreateCharacter()
        {
            return new Archer
            {
                Health = 85,
                Strength = 12,
                Level = 1
            };
        }
    }
}