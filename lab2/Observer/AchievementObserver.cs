using System.Collections.Generic;

namespace Lab2.Observer
{
    /// <summary>
    /// Concrete observer that tracks achievements.
    /// Demonstrates Observer pattern with different behavior than ConsoleLoggerObserver.
    /// </summary>
    public class AchievementObserver : IGameEventObserver
    {
        private readonly HashSet<string> _unlockedAchievements = new();

        public void OnLevelUp(string characterName, int newLevel)
        {
            if (newLevel >= 10 && !_unlockedAchievements.Contains("Level10"))
            {
                _unlockedAchievements.Add("Level10");
                Console.WriteLine($"  [Achievement] Unlocked: 'Reached Level 10'!");
            }
            if (newLevel >= 20 && !_unlockedAchievements.Contains("Level20"))
            {
                _unlockedAchievements.Add("Level20");
                Console.WriteLine($"  [Achievement] Unlocked: 'Reached Level 20'!");
            }
        }

        public void OnQuestCompleted(string questName, int reward)
        {
            if (!_unlockedAchievements.Contains("FirstQuest"))
            {
                _unlockedAchievements.Add("FirstQuest");
                Console.WriteLine($"  [Achievement] Unlocked: 'First Quest Completed'!");
            }
        }

        public void OnCharacterDied(string characterName)
        {
            // Achievement for dying? Maybe not, but we track it
        }

        public void OnHealthChanged(string characterName, int currentHealth, int maxHealth)
        {
            // Track health changes if needed
        }

        public void DisplayAchievements()
        {
            Console.WriteLine("\n=== Unlocked Achievements ===");
            if (_unlockedAchievements.Count == 0)
            {
                Console.WriteLine("No achievements unlocked yet.");
            }
            else
            {
                foreach (var achievement in _unlockedAchievements)
                {
                    Console.WriteLine($"- {achievement}");
                }
            }
        }
    }
}

