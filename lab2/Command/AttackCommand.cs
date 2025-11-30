using Lab2.Models;
using Lab2.Strategy;

namespace Lab2.Command
{
    /// <summary>
    /// Command to perform an attack using a strategy.
    /// Demonstrates Command pattern combined with Strategy pattern.
    /// </summary>
    public class AttackCommand : ICommand
    {
        private readonly GameCharacter _attacker;
        private readonly GameCharacter _target;
        private readonly CombatContext _combatContext;
        private readonly int _baseDamage;
        private int _damageDealt;
        private int _targetHealthBefore;

        public AttackCommand(GameCharacter attacker, GameCharacter target, CombatContext combatContext, int baseDamage)
        {
            _attacker = attacker;
            _target = target;
            _combatContext = combatContext;
            _baseDamage = baseDamage;
        }

        public void Execute()
        {
            _targetHealthBefore = _target.Health;
            _damageDealt = _combatContext.PerformAttack(_baseDamage, _attacker.Level);
            _target.TakeDamage(_damageDealt);
            Console.WriteLine($"[Command] {_attacker.Name} attacked {_target.Name} for {_damageDealt} damage. {_target.Name} health: {_target.Health}/{_target.MaxHealth}");
        }

        public void Undo()
        {
            _target.Health = _targetHealthBefore;
            Console.WriteLine($"[Command] UNDO: {_target.Name} health restored to {_targetHealthBefore}/{_target.MaxHealth}");
        }

        public string GetDescription()
        {
            return $"{_attacker.Name} attacks {_target.Name}";
        }
    }
}

