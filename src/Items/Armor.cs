namespace RPG.Items;

public class Armor : Equipment
{
    public Armor(string name, int durability, float defenseValue)
    {
        Name = name;
        Durability = durability;
        DefenseValue = defenseValue;
    }

    public float DefenseValue { get; private set; }

    public void DecreaseDurability(int amount)
    {
        Durability -= amount;
        if (Durability < 0) Durability = 0;
    }

    public void RepairDurability(int amount)
    {
        Durability += amount;
        if (Durability > 100) Durability = 100;
    }
}