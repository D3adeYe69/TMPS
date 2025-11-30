namespace Lab2.Strategy
{
    /// <summary>
    /// Sneak attack strategy - low base damage but chance for critical hit.
    /// Demonstrates Strategy pattern.
    /// </summary>
    public class SneakAttackStrategy : IAttackStrategy
    {
        private readonly Random _random = new();

        public string Name => "Sneak Attack";

        public int ExecuteAttack(int baseDamage, int characterLevel)
        {
            // 70% base damage, but 30% chance for 3x critical
            int damage = (int)(baseDamage * 0.7) + characterLevel;
            
            if (_random.NextDouble() < 0.3)
            {
                damage *= 3;
                Console.WriteLine($"  [Sneak Attack] CRITICAL HIT! Deals {damage} damage!");
            }
            else
            {
                Console.WriteLine($"  [Sneak Attack] Deals {damage} damage");
            }
            
            return damage;
        }

        public string GetDescription()
        {
            return "Low base damage (70% + level), but 30% chance for 3x critical hit";
        }
    }
}

