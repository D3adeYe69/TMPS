namespace Lab2.Strategy
{
    /// <summary>
    /// Defensive attack strategy - moderate damage, reduces incoming damage.
    /// Demonstrates Strategy pattern.
    /// </summary>
    public class DefensiveAttackStrategy : IAttackStrategy
    {
        public string Name => "Defensive";

        public int ExecuteAttack(int baseDamage, int characterLevel)
        {
            // 80% damage, but provides defense bonus
            int damage = (int)(baseDamage * 0.8) + characterLevel;
            Console.WriteLine($"  [Defensive Attack] Deals {damage} damage (reduces incoming damage by 30%)");
            return damage;
        }

        public string GetDescription()
        {
            return "Moderate damage (80% + level), but reduces incoming damage by 30%";
        }
    }
}

