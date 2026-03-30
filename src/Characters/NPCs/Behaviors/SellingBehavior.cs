using RPG.Characters;
using RPG.Characters.NPCs.Behaviors;
using RPG.Items;

namespace RPG.Characters.NPCs.Behaviors;

/// <summary>
/// Comportamento de venda: lista os itens disponíveis do NPC comerciante.
/// </summary>
public class SellingBehavior : IBehavior
{
    private readonly List<Item> _itemsForSale;

    /// <summary>
    /// Inicializa o comportamento com a lista de itens à venda.
    /// </summary>
    /// <param name="itemsForSale">Itens que o NPC oferece ao jogador.</param>
    public SellingBehavior(List<Item> itemsForSale)
    {
        _itemsForSale = itemsForSale;
    }

    /// <summary>
    /// Exibe no console os itens disponíveis para compra.
    /// </summary>
    /// <param name="npc">NPC que realiza a venda.</param>
    public void Execute(Npc npc)
    {
        Console.WriteLine($"{npc.Name} tem os seguintes itens à venda:");
        foreach (var item in _itemsForSale) Console.WriteLine(item.Name);
    }

    /// <summary>
    /// Adiciona um item à lista de venda.
    /// </summary>
    /// <param name="item">Item a ser adicionado.</param>
    public void AddItemForSale(Item item)
    {
        _itemsForSale.Add(item);
    }

    /// <summary>
    /// Remove um item da lista de venda.
    /// </summary>
    /// <param name="item">Item a ser removido.</param>
    public void RemoveItemForSale(Item item)
    {
        _itemsForSale.Remove(item);
    }
}
