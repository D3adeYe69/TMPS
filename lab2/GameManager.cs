using Lab2.Models;
using Lab2.Observer;

namespace Lab2
{
    /// <summary>
    /// Central game manager that coordinates game state and events.
    /// Integrates Observer pattern for event notifications.
    /// </summary>
    public class GameManager
    {
        private readonly List<GameCharacter> _characters = new();
        private readonly GameEventManager _eventManager = new();

        public GameEventManager EventManager => _eventManager;

        public void AddCharacter(GameCharacter character)
        {
            _characters.Add(character);
        }

        public List<GameCharacter> GetCharacters()
        {
            return new List<GameCharacter>(_characters);
        }

        public GameCharacter? GetCharacter(string name)
        {
            return _characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void CompleteQuest(string questName, int experienceReward, string characterName)
        {
            var character = GetCharacter(characterName);
            if (character != null)
            {
                int oldLevel = character.Level;
                character.AddExperience(experienceReward);
                
                _eventManager.NotifyQuestCompleted(questName, experienceReward);
                
                if (character.Level > oldLevel)
                {
                    _eventManager.NotifyLevelUp(characterName, character.Level);
                }
            }
        }

        public void UpdateCharacterHealth(GameCharacter character)
        {
            _eventManager.NotifyHealthChanged(character.Name, character.Health, character.MaxHealth);
            
            if (character.Health <= 0)
            {
                _eventManager.NotifyCharacterDied(character.Name);
            }
        }
    }
}

