using RPG.Interface;
using RPG.Items;

namespace RPG.Core;

public class SellingBehavior : IBehavior
{
    private readonly List<Item> _itemsForSale;

    public SellingBehavior(List<Item> itemsForSale)
    {
        _itemsForSale = itemsForSale;
    }

    public void Execute(Npc npc)
    {
        Console.WriteLine($"{npc.Name} tem os seguintes itens à venda:");
        foreach (var item in _itemsForSale) Console.WriteLine(item.Name);
    }

    public void AddItemForSale(Item item)
    {
        _itemsForSale.Add(item);
    }

    public void RemoveItemForSale(Item item)
    {
        _itemsForSale.Remove(item);
    }
}