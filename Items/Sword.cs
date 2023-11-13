using RPG.Attributes;
using RPG.Calculators;
using RPG.Core;

namespace RPG.Items;

public class Sword : Weapon
{
    private readonly ScalingCalculator _scalingCalculator;

    public Sword(string name, int damage, float attackSpeed, int durability, ScalingAttribute[] scaling)
        : base(name, damage, attackSpeed, durability)
    {
        Scaling = scaling;
        _scalingCalculator = new ScalingCalculator();
    }

    private ScalingAttribute[] Scaling { get; }

    private float GetModifiedAttack(Character attacker)
    {
        var modifiedPAttack = Damage;
        var attributes = attacker.CharacterPointsAttributes;

        modifiedPAttack += _scalingCalculator.CalculateScaling(Scaling, attributes);

        return modifiedPAttack;
    }

    public override void Attack(Character attacker, Character target)
    {
        if (Durability <= 0)
            // TODO: Implement logic for broken weapon
            return;

        Durability--;
        var damage = new DamageCalculator(attacker.GetModifiedBaseAttack() + GetModifiedAttack(attacker),
            attacker.GetModifiedPAttack(),
            target.Defense());
        var power = damage.CalculateTotalDamage();
        target.TakeDamage(power);
        Console.WriteLine($"{attacker.Name} attacked {target.Name} with {Name} for {power} damage.");
    }
}