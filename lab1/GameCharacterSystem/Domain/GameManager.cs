using GameCharacterSystem.Models;

namespace GameCharacterSystem.Domain
{
    public sealed class GameManager
    {
        private static GameManager? instance;
        private static readonly object padlock = new object();
        private List<Character> characters;

        private GameManager()
        {
            characters = new List<Character>();
        }

        public static GameManager Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new GameManager();
                    return instance;
                }
            }
        }

        public void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        public List<Character> GetAllCharacters()
        {
            return characters.ToList();
        }
    }
}