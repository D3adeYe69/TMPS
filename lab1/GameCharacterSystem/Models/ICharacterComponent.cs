namespace GameCharacterSystem.Models
{
    /// <summary>
    /// Component interface for Decorator and Composite patterns.
    /// </summary>
    public interface ICharacterComponent
    {
        string GetDescription();
        int GetTotalHealth();
        int GetTotalStrength();
        Dictionary<string, string> GetEquipment();
    }
}

