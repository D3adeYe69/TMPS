namespace Lab2.Observer
{
    /// <summary>
    /// Concrete observer that logs game events to console.
    /// Demonstrates Observer pattern.
    /// </summary>
    public class ConsoleLoggerObserver : IGameEventObserver
    {
        public void OnLevelUp(string characterName, int newLevel)
        {
            Console.WriteLine($"  [Logger] {characterName} leveled up to {newLevel}! Stats increased.");
        }

        public void OnQuestCompleted(string questName, int reward)
        {
            Console.WriteLine($"  [Logger] Quest '{questName}' completed. Reward: {reward} experience points.");
        }

        public void OnCharacterDied(string characterName)
        {
            Console.WriteLine($"  [Logger] {characterName} has died. Game over!");
        }

        public void OnHealthChanged(string characterName, int currentHealth, int maxHealth)
        {
            double percentage = (double)currentHealth / maxHealth * 100;
            if (percentage < 25)
            {
                Console.WriteLine($"  [Logger] WARNING: {characterName} health critical: {currentHealth}/{maxHealth} ({percentage:F1}%)");
            }
        }
    }
}

