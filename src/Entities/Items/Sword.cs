using RPG.Systems.Attributes;
using RPG.Calculators;
using RPG.Core;

namespace RPG.Entities.Items;

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
        {
            Console.WriteLine($"{Name} está quebrada e não pode ser usada!");
            attacker.Equipment.UnequipWeapon();
            return;
        }

        Durability--;
        var damage = new DamageCalculator(attacker.GetModifiedBaseAttack() + GetModifiedAttack(attacker),
            attacker.GetModifiedPAttack(),
            target.Defense());
        var power = damage.CalculateTotalDamage();
        target.TakeDamage(power);
        Console.WriteLine($"{attacker.Name} attacked {target.Name} with {Name} for {power} damage.");

        // Aplica os efeitos especiais
        // conta quantos efeitos foram aplicados
        Console.WriteLine(Effects.Count);
        foreach (var effect in Effects)
        {
            Console.WriteLine($"{attacker.Name} used {Name} and applied {effect.GetType().Name} to {target.Name}.");
            effect.ApplyEffect(attacker, target);

            Durability--;
        }
    }
}