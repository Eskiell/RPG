using RPG.Attributes;
using RPG.Calculators;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Core;

public abstract class Character
{
    public Character(string name, float baseHealth, PointsAttributes pointsAttributes)
    {
        Name = name;
        BaseHealth = baseHealth;
        CharacterPointsAttributes = pointsAttributes;
        Equipment = new EquipmentManager(this);
        Health = GetMaxHealth();
    }

    public List<StatusEffect> ActiveEffects { get; set; } = new();

    // Atributos gerais
    public string Name { get; set; }
    public float Health { get; set; }
    public float BaseHealth { get; protected set; }
    public float BaseAttack { get; set; } = 10;
    public float BaseDefense { get; set; } = 10;
    public int Level { get; set; } = 1;

    public EquipmentManager Equipment { get; }
    public PointsAttributes CharacterPointsAttributes { get; protected set; }

    public float GetMaxHealth()
    {
        return StatCalculator.ComputeBonus(CharacterAttribute.Vitality, CharacterPointsAttributes.Vitality) +
               BaseHealth;
    }

    public void UpdateEffects()
    {
        var expiredEffects = new List<StatusEffect>();
        foreach (var effect in ActiveEffects)
        {
            effect.Duration--;

            if (effect.Duration <= 0)
                expiredEffects.Add(effect);
        }

        foreach (var effect in expiredEffects) effect.RemoveEffect(this);
    }

    public abstract void Attack(Character target);

    public virtual float Defense()
    {
        return GetModifiedBaseDefense() * GetModifiedPDefense();
    }

    public virtual bool IsAlive()
    {
        return Health > 0;
    }

    public virtual bool IsDead()
    {
        return Health <= 0;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public virtual float GetModifiedBaseAttack()
    {
        return CombatCalculator.CalculateModifiedAttack(this);
    }

    public virtual float GetModifiedBaseDefense()
    {
        return CombatCalculator.CalculateModifiedDefense(this);
    }

    public virtual float GetModifiedPDefense()
    {
        var vitality = CharacterPointsAttributes.Vitality;
        var endurance = CharacterPointsAttributes.Endurance;
        var strength = CharacterPointsAttributes.Strength;
        const float vitalityBonus = AttributeBonus.Vitality;
        const float enduranceBonus = AttributeBonus.Endurance;
        const float strengthBonus = AttributeBonus.Strength;
        return GetModifiedBaseDefense() * vitality * endurance * strength * vitalityBonus * enduranceBonus *
               strengthBonus;
    }

    public virtual float GetModifiedPAttack()
    {
        return CombatCalculator.CalculatePhysicalAttack(this);
    }

    public void IncreaseLevelUp(int amount)
    {
        Level += amount;
    }

    public void AdjustHealth(float amount)
    {
        Health += amount;
        if (Health < 0) Health = 0;
    }
}