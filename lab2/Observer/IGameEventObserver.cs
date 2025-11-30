namespace Lab2.Observer
{
    /// <summary>
    /// Observer interface for game events.
    /// Part of the Observer pattern.
    /// </summary>
    public interface IGameEventObserver
    {
        void OnLevelUp(string characterName, int newLevel);
        void OnQuestCompleted(string questName, int reward);
        void OnCharacterDied(string characterName);
        void OnHealthChanged(string characterName, int currentHealth, int maxHealth);
    }
}

