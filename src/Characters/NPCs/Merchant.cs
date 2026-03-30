using RPG.Systems.Attributes;
using RPG.Core;
using RPG.Entities.Items;
using RPG.Behaviors;

namespace RPG.NPCs;

public class Merchant : Npc
{
    // Outros métodos específicos de comerciantes...
    public Merchant(string name, float health, PointsAttributes pointsAttributes, List<Item> itemsForSale) : base(name,
        health, pointsAttributes)
    {
        Behavior = new SellingBehavior(itemsForSale);
    }
}