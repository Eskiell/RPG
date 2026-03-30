using RPG.Attributes;
using RPG.Combat;
using RPG.Characters;

namespace RPG.Items;

/// <summary>
/// Espada que escala o dano com atributos do portador e aplica efeitos especiais ao acertar.
/// </summary>
public class Sword : Weapon
{
    private readonly ScalingCalculator _scalingCalculator;

    /// <summary>
    /// Inicializa uma espada com seus atributos e escalonamento.
    /// </summary>
    /// <param name="name">Nome da espada.</param>
    /// <param name="damage">Dano base.</param>
    /// <param name="attackSpeed">Velocidade de ataque.</param>
    /// <param name="durability">Durabilidade inicial.</param>
    /// <param name="scaling">Atributos que escalonam o dano da espada.</param>
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

    /// <summary>
    /// Executa o ataque com a espada, aplicando dano e efeitos especiais ao alvo.
    /// </summary>
    /// <param name="attacker">Personagem que realiza o ataque.</param>
    /// <param name="target">Personagem alvo.</param>
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

        Console.WriteLine(Effects.Count);
        foreach (var effect in Effects)
        {
            Console.WriteLine($"{attacker.Name} used {Name} and applied {effect.GetType().Name} to {target.Name}.");
            effect.ApplyEffect(attacker, target);
            Durability--;
        }
    }
}
