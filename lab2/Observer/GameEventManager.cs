using System.Collections.Generic;

namespace Lab2.Observer
{
    /// <summary>
    /// Game Event Manager - Subject in the Observer pattern.
    /// Manages observers and notifies them of game events.
    /// </summary>
    public class GameEventManager : IGameEventSubject
    {
        private readonly List<IGameEventObserver> _observers = new();

        public void Subscribe(IGameEventObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"[Observer] {observer.GetType().Name} subscribed to game events.");
            }
        }

        public void Unsubscribe(IGameEventObserver observer)
        {
            if (_observers.Remove(observer))
            {
                Console.WriteLine($"[Observer] {observer.GetType().Name} unsubscribed from game events.");
            }
        }

        public void NotifyLevelUp(string characterName, int newLevel)
        {
            Console.WriteLine($"[Event] Level Up: {characterName} reached level {newLevel}!");
            foreach (var observer in _observers)
            {
                observer.OnLevelUp(characterName, newLevel);
            }
        }

        public void NotifyQuestCompleted(string questName, int reward)
        {
            Console.WriteLine($"[Event] Quest Completed: {questName} (Reward: {reward} exp)!");
            foreach (var observer in _observers)
            {
                observer.OnQuestCompleted(questName, reward);
            }
        }

        public void NotifyCharacterDied(string characterName)
        {
            Console.WriteLine($"[Event] Character Died: {characterName}!");
            foreach (var observer in _observers)
            {
                observer.OnCharacterDied(characterName);
            }
        }

        public void NotifyHealthChanged(string characterName, int currentHealth, int maxHealth)
        {
            foreach (var observer in _observers)
            {
                observer.OnHealthChanged(characterName, currentHealth, maxHealth);
            }
        }
    }
}

