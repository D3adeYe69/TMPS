using GameCharacterSystem.Models;

namespace GameCharacterSystem.Domain
{
    public class CharacterBuilder
    {
        private readonly Character character;

        public CharacterBuilder(Character character)
        {
            this.character = character;
        }

        public CharacterBuilder WithName(string name)
        {
            character.Name = name;
            return this;
        }

        public CharacterBuilder WithLevel(int level)
        {
            character.Level = level;
            return this;
        }

        public CharacterBuilder WithHealth(int health)
        {
            character.Health = health;
            return this;
        }

        public CharacterBuilder WithStrength(int strength)
        {
            character.Strength = strength;
            return this;
        }

        public CharacterBuilder WithEquipment(string slot, string item)
        {
            character.Equipment[slot] = item;
            return this;
        }

        public Character Build()
        {
            return character;
        }
    }
}