using RPG.Attributes;
using RPG.Characters;
using RPG.Items;
using RPG.Characters.NPCs.Behaviors;

namespace RPG.Characters.NPCs;

/// <summary>
/// NPC do tipo comerciante, que oferece itens à venda usando o comportamento <see cref="SellingBehavior"/>.
/// </summary>
public class Merchant : Npc
{
    /// <summary>
    /// Inicializa um novo Merchant com os dados básicos e a lista de itens à venda.
    /// </summary>
    /// <param name="name">Nome do comerciante.</param>
    /// <param name="health">Vida base.</param>
    /// <param name="pointsAttributes">Atributos primários.</param>
    /// <param name="itemsForSale">Lista de itens disponíveis para venda.</param>
    public Merchant(string name, float health, PointsAttributes pointsAttributes, List<Item> itemsForSale)
        : base(name, health, pointsAttributes)
    {
        Behavior = new SellingBehavior(itemsForSale);
    }
}
