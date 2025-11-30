using System.Collections.Generic;

namespace Lab2.Command
{
    /// <summary>
    /// Manages command history for undo/redo functionality.
    /// Part of the Command pattern implementation.
    /// </summary>
    public class CommandHistory
    {
        private readonly Stack<ICommand> _history = new();
        private readonly Stack<ICommand> _redoStack = new();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _history.Push(command);
            _redoStack.Clear(); // Clear redo stack when new command is executed
        }

        public bool Undo()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("[Command History] No commands to undo.");
                return false;
            }

            var command = _history.Pop();
            command.Undo();
            _redoStack.Push(command);
            return true;
        }

        public bool Redo()
        {
            if (_redoStack.Count == 0)
            {
                Console.WriteLine("[Command History] No commands to redo.");
                return false;
            }

            var command = _redoStack.Pop();
            command.Execute();
            _history.Push(command);
            return true;
        }

        public void Clear()
        {
            _history.Clear();
            _redoStack.Clear();
            Console.WriteLine("[Command History] History cleared.");
        }

        public int GetHistoryCount()
        {
            return _history.Count;
        }
    }
}

