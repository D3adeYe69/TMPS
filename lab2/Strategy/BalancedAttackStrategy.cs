namespace Lab2.Strategy
{
    /// <summary>
    /// Balanced attack strategy - standard damage.
    /// Demonstrates Strategy pattern.
    /// </summary>
    public class BalancedAttackStrategy : IAttackStrategy
    {
        public string Name => "Balanced";

        public int ExecuteAttack(int baseDamage, int characterLevel)
        {
            // 100% damage + level bonus
            int damage = baseDamage + characterLevel;
            Console.WriteLine($"  [Balanced Attack] Deals {damage} damage (standard attack)");
            return damage;
        }

        public string GetDescription()
        {
            return "Standard damage (100% + level bonus), balanced approach";
        }
    }
}

