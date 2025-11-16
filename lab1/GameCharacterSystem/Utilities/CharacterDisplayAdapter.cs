using GameCharacterSystem.Models;

namespace GameCharacterSystem.Utilities
{
    /// <summary>
    /// Adapter Pattern: Adapts character data to different display formats.
    /// This allows the system to work with different output formats without
    /// modifying the core character classes.
    /// </summary>
    public interface ICharacterDisplayAdapter
    {
        string Format(ICharacterComponent character);
    }

    /// <summary>
    /// Adapter for plain text display format.
    /// </summary>
    public class TextDisplayAdapter : ICharacterDisplayAdapter
    {
        public string Format(ICharacterComponent character)
        {
            return $"{character.GetDescription()}\n" +
                   $"Health: {character.GetTotalHealth()}\n" +
                   $"Strength: {character.GetTotalStrength()}\n" +
                   $"Equipment: {string.Join(", ", character.GetEquipment().Select(e => $"{e.Key}: {e.Value}"))}";
        }
    }

    /// <summary>
    /// Adapter for JSON-like display format.
    /// </summary>
    public class JsonDisplayAdapter : ICharacterDisplayAdapter
    {
        public string Format(ICharacterComponent character)
        {
            var equipmentJson = string.Join(", ", 
                character.GetEquipment().Select(e => $"\"{e.Key}\": \"{e.Value}\""));
            
            return $"{{\n" +
                   $"  \"description\": \"{character.GetDescription()}\",\n" +
                   $"  \"health\": {character.GetTotalHealth()},\n" +
                   $"  \"strength\": {character.GetTotalStrength()},\n" +
                   $"  \"equipment\": {{ {equipmentJson} }}\n" +
                   $"}}";
        }
    }

    /// <summary>
    /// Adapter for XML-like display format.
    /// </summary>
    public class XmlDisplayAdapter : ICharacterDisplayAdapter
    {
        public string Format(ICharacterComponent character)
        {
            var equipmentXml = string.Join("\n", 
                character.GetEquipment().Select(e => $"    <{e.Key}>{e.Value}</{e.Key}>"));
            
            return $"<Character>\n" +
                   $"  <Description>{character.GetDescription()}</Description>\n" +
                   $"  <Health>{character.GetTotalHealth()}</Health>\n" +
                   $"  <Strength>{character.GetTotalStrength()}</Strength>\n" +
                   $"  <Equipment>\n{equipmentXml}\n  </Equipment>\n" +
                   $"</Character>";
        }
    }
}

