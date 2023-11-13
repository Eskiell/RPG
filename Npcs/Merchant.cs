using RPG.Attributes;
using RPG.Core;
using RPG.Items;

namespace RPG.Npcs;

public class Merchant : Npc
{
    // Outros métodos específicos de comerciantes...
    public Merchant(string name, float health, PointsAttributes pointsAttributes, List<Item> itemsForSale) : base(name,
        health, pointsAttributes)
    {
        SellingBehavior = new SellingBehavior(itemsForSale);
    }
}