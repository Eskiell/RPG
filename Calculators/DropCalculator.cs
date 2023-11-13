using RPG.Drops;
using RPG.Items;

namespace RPG.Calculators;

public class DropCalculator
{
    private readonly List<ItemDrop> _dropTable;
    private readonly Random _random;

    public DropCalculator()
    {
        _dropTable = new List<ItemDrop>();
        _random = new Random();
    }

    public void AddItemDrop(Item item, float dropRate)
    {
        var itemDrop = new ItemDrop(item, dropRate);
        _dropTable.Add(itemDrop);
    }

    public List<Item> GetRandomItemDrop()
    {
        var randomItems = new List<Item>();

        foreach (var itemDrop in _dropTable)
            if (_random.NextDouble() < itemDrop.DropRate && itemDrop.Item != null)
                randomItems.Add(itemDrop.Item);

        return randomItems;
    }
}