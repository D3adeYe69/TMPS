namespace Lab2.Command
{
    /// <summary>
    /// Command interface for the Command pattern.
    /// Allows actions to be encapsulated as objects, enabling undo/redo functionality.
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
        string GetDescription();
    }
}

