using RPG.Characters;
using RPG.Effects;

namespace RPG.Items;

public abstract class Weapon : Equipment
{
    public Weapon(string name, float damage, float attackSpeed, int durability)
    {
        Name = name;
        Damage = damage;
        AttackSpeed = attackSpeed;
        Durability = durability;
    }

    public List<IWeaponEffect> Effects { get; } = new();
    public new string Name { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public new int Durability { get; set; }

    public void Repair(int amount)
    {
        Durability += amount;
    }

    public abstract void Attack(Character attacker, Character target);
}