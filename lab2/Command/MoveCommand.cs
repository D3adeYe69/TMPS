using Lab2.Models;

namespace Lab2.Command
{
    /// <summary>
    /// Command to move a character to a new position.
    /// Demonstrates Command pattern with undo capability.
    /// </summary>
    public class MoveCommand : ICommand
    {
        private readonly GameCharacter _character;
        private readonly int _newX;
        private readonly int _newY;
        private int _oldX;
        private int _oldY;

        public MoveCommand(GameCharacter character, int newX, int newY)
        {
            _character = character;
            _newX = newX;
            _newY = newY;
        }

        public void Execute()
        {
            _oldX = _character.PositionX;
            _oldY = _character.PositionY;
            _character.PositionX = _newX;
            _character.PositionY = _newY;
            Console.WriteLine($"[Command] {_character.Name} moved from ({_oldX}, {_oldY}) to ({_newX}, {_newY})");
        }

        public void Undo()
        {
            _character.PositionX = _oldX;
            _character.PositionY = _oldY;
            Console.WriteLine($"[Command] UNDO: {_character.Name} moved back to ({_oldX}, {_oldY})");
        }

        public string GetDescription()
        {
            return $"Move {_character.Name} to ({_newX}, {_newY})";
        }
    }
}

