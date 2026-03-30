using RPG.Systems.Attributes;

namespace RPG.Entities.Items;

public class Ring
{
    public Ring(string name, string description, ScalingAttribute[] scaling)
    {
        Name = name;
        Description = description;
        Scaling = scaling;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Durability { get; private set; } = 100;
    public ScalingAttribute[] Scaling { get; private set; }

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