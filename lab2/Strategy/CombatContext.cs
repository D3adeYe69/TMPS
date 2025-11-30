namespace Lab2.Strategy
{
    /// <summary>
    /// Context class that uses different attack strategies.
    /// Demonstrates Strategy pattern - can switch strategies at runtime.
    /// </summary>
    public class CombatContext
    {
        private IAttackStrategy _strategy;

        public CombatContext(IAttackStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IAttackStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine($"[Strategy] Changed attack strategy to: {strategy.Name}");
        }

        public int PerformAttack(int baseDamage, int characterLevel)
        {
            return _strategy.ExecuteAttack(baseDamage, characterLevel);
        }

        public string GetCurrentStrategyDescription()
        {
            return _strategy.GetDescription();
        }
    }
}

