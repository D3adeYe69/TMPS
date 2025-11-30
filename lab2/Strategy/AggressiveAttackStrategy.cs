namespace Lab2.Strategy
{
    /// <summary>
    /// Aggressive attack strategy - high damage, no defense.
    /// Demonstrates Strategy pattern.
    /// </summary>
    public class AggressiveAttackStrategy : IAttackStrategy
    {
        public string Name => "Aggressive";

        public int ExecuteAttack(int baseDamage, int characterLevel)
        {
            // 150% damage, scales with level
            int damage = (int)(baseDamage * 1.5) + (characterLevel * 2);
            Console.WriteLine($"  [Aggressive Attack] Deals {damage} damage (high risk, high reward)");
            return damage;
        }

        public string GetDescription()
        {
            return "High damage output (150% + level bonus), but leaves character vulnerable";
        }
    }
}

