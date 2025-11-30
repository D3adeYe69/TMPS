namespace Lab2.Observer
{
    /// <summary>
    /// Subject interface for the Observer pattern.
    /// Allows observers to subscribe/unsubscribe to game events.
    /// </summary>
    public interface IGameEventSubject
    {
        void Subscribe(IGameEventObserver observer);
        void Unsubscribe(IGameEventObserver observer);
        void NotifyLevelUp(string characterName, int newLevel);
        void NotifyQuestCompleted(string questName, int reward);
        void NotifyCharacterDied(string characterName);
        void NotifyHealthChanged(string characterName, int currentHealth, int maxHealth);
    }
}

