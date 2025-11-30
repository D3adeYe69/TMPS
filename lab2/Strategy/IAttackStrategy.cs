namespace Lab2.Strategy
{
    /// <summary>
    /// Strategy interface for different attack behaviors.
    /// Part of the Strategy pattern.
    /// </summary>
    public interface IAttackStrategy
    {
        string Name { get; }
        int ExecuteAttack(int baseDamage, int characterLevel);
        string GetDescription();
    }
}

