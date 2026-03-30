using RPG.Attributes;
using RPG.Characters;
using RPG.Items;
using RPG.Characters.NPCs.Behaviors;

namespace RPG.Characters.NPCs;

public class Merchant : Npc
{
    // Outros métodos específicos de comerciantes...
    public Merchant(string name, float health, PointsAttributes pointsAttributes, List<Item> itemsForSale) : base(name,
        health, pointsAttributes)
    {
        Behavior = new SellingBehavior(itemsForSale);
    }
}