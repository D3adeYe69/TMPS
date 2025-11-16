namespace GameCharacterSystem.Models
{
    /// <summary>
    /// Composite Pattern: Represents a group of characters (party) that can be
    /// treated as a single unit. A party can contain individual characters or other parties.
    /// </summary>
    public class CharacterParty : ICharacterComponent
    {
        private readonly List<ICharacterComponent> members;
        private readonly string partyName;

        public CharacterParty(string partyName)
        {
            this.partyName = partyName;
            members = new List<ICharacterComponent>();
        }

        /// <summary>
        /// Adds a character or another party to this party.
        /// </summary>
        public void AddMember(ICharacterComponent member)
        {
            members.Add(member);
        }

        /// <summary>
        /// Removes a member from the party.
        /// </summary>
        public void RemoveMember(ICharacterComponent member)
        {
            members.Remove(member);
        }

        /// <summary>
        /// Gets all members of the party.
        /// </summary>
        public List<ICharacterComponent> GetMembers()
        {
            return new List<ICharacterComponent>(members);
        }

        public string GetDescription()
        {
            var memberDescriptions = members.Select(m => m.GetDescription());
            return $"Party '{partyName}' ({members.Count} members): {string.Join(", ", memberDescriptions)}";
        }

        /// <summary>
        /// Total health is the sum of all party members' health.
        /// </summary>
        public int GetTotalHealth()
        {
            return members.Sum(m => m.GetTotalHealth());
        }

        /// <summary>
        /// Total strength is the sum of all party members' strength.
        /// </summary>
        public int GetTotalStrength()
        {
            return members.Sum(m => m.GetTotalStrength());
        }

        /// <summary>
        /// Combined equipment from all party members.
        /// </summary>
        public Dictionary<string, string> GetEquipment()
        {
            var allEquipment = new Dictionary<string, string>();
            foreach (var member in members)
            {
                var memberEquipment = member.GetEquipment();
                foreach (var item in memberEquipment)
                {
                    // If multiple members have the same equipment slot, combine them
                    if (allEquipment.ContainsKey(item.Key))
                    {
                        allEquipment[item.Key] += $", {item.Value}";
                    }
                    else
                    {
                        allEquipment[item.Key] = item.Value;
                    }
                }
            }
            return allEquipment;
        }
    }
}

