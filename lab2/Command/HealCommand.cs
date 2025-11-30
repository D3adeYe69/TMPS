using Lab2.Models;

namespace Lab2.Command
{
    /// <summary>
    /// Command to heal a character.
    /// Demonstrates Command pattern with undo capability.
    /// </summary>
    public class HealCommand : ICommand
    {
        private readonly GameCharacter _character;
        private readonly int _healAmount;
        private int _healthBefore;

        public HealCommand(GameCharacter character, int healAmount)
        {
            _character = character;
            _healAmount = healAmount;
        }

        public void Execute()
        {
            _healthBefore = _character.Health;
            _character.Heal(_healAmount);
            Console.WriteLine($"[Command] {_character.Name} healed for {_healAmount} HP. Health: {_character.Health}/{_character.MaxHealth}");
        }

        public void Undo()
        {
            _character.Health = _healthBefore;
            Console.WriteLine($"[Command] UNDO: {_character.Name} health restored to {_healthBefore}/{_character.MaxHealth}");
        }

        public string GetDescription()
        {
            return $"Heal {_character.Name} for {_healAmount} HP";
        }
    }
}

