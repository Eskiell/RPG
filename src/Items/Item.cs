using RPG.Enums;

namespace RPG.Items;

public class Item
{
    public Item(
        string name,
        int value,
        Rarity itemRarity,
        string description,
        double price,
        YesNoOption consumable)
    {
        Name = name;
        Value = value;
        Description = description;
        ItemRarity = itemRarity;
        Price = price;
        Consumable = consumable;
    }

    public string Name { get; set; }
    public int Value { get; set; }
    public Rarity ItemRarity { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public YesNoOption Consumable { get; set; }
}