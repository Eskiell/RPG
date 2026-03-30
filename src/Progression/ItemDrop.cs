using RPG.Items;

namespace RPG.Progression;

/// <summary>
/// Representa uma entrada na tabela de drops, associando um item a uma taxa de queda.
/// </summary>
public class ItemDrop
{
    /// <summary>
    /// Inicializa uma entrada de drop.
    /// </summary>
    /// <param name="item">Item que pode ser dropado (pode ser nulo).</param>
    /// <param name="dropRate">Taxa de drop entre 0.0 (nunca) e 1.0 (sempre).</param>
    public ItemDrop(Item? item, float dropRate)
    {
        Item = item;
        DropRate = dropRate;
    }

    /// <summary>Item associado a esta entrada de drop.</summary>
    public Item? Item { get; }

    /// <summary>Probabilidade de queda do item (0.0 a 1.0).</summary>
    public float DropRate { get; }
}
