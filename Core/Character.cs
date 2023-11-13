using RPG.Attributes;
using RPG.Items;

namespace RPG.Core;

public abstract class Character
{
    public Character(string name, float health, PointsAttributes pointsAttributes)
    {
        Name = name;
        Health = health;
        CharacterPointsAttributes = pointsAttributes;
    }

    // Atributos gerais
    public string Name { get; set; }
    public float Health { get; set; }
    public float BaseAttack { get; set; } = 10;
    public float BaseDefense { get; set; } = 10;
    public int Level { get; set; } = 1;
    public Weapon? EquippedWeapon { get; set; }
    public PointsAttributes CharacterPointsAttributes { get; protected set; }

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
        return BaseAttack;
    }

    public virtual float GetModifiedBaseDefense()
    {
        return BaseDefense;
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
        var strength = CharacterPointsAttributes.Strength;
        var dexterity = CharacterPointsAttributes.Dexterity;
        const float strengthBonus = AttributeBonus.Strength;
        const float dexterityBonus = AttributeBonus.Dexterity;
        return GetModifiedBaseAttack() * strength * dexterity * strengthBonus * dexterityBonus;
    }

    public void IncreaseLevelUp(int amount)
    {
        Level += amount;
    }

    public void IncreaseHealth(float amount)
    {
        Health += amount;
    }

    public void DecreaseHealth(float amount)
    {
        Health -= amount;
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        EquippedWeapon = weapon;
        Console.WriteLine($"{Name} equipped {weapon.Name}.");
    }

    public virtual void UnequipWeapon()
    {
        EquippedWeapon = null;
        Console.WriteLine($"{Name} unequipped the weapon.");
    }
}