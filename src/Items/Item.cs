using RPG.Enums;

namespace RPG.Items;

/// <summary>
/// Representa um item base do jogo com nome, valor, raridade, descrição, preço e flag de consumível.
/// </summary>
public class Item
{
    /// <summary>
    /// Inicializa um novo item com todos os seus atributos.
    /// </summary>
    /// <param name="name">Nome do item.</param>
    /// <param name="value">Valor numérico do item.</param>
    /// <param name="itemRarity">Raridade do item.</param>
    /// <param name="description">Descrição do item.</param>
    /// <param name="price">Preço de venda/compra.</param>
    /// <param name="consumable">Indica se o item é consumível.</param>
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

    /// <summary>Nome do item.</summary>
    public string Name { get; set; }

    /// <summary>Valor numérico (poder, quantidade, etc.).</summary>
    public int Value { get; set; }

    /// <summary>Raridade do item (Common, Rare, etc.).</summary>
    public Rarity ItemRarity { get; set; }

    /// <summary>Descrição exibida ao jogador.</summary>
    public string Description { get; set; }

    /// <summary>Preço de compra/venda no mercado.</summary>
    public double Price { get; set; }

    /// <summary>Indica se o item pode ser consumido pelo jogador.</summary>
    public YesNoOption Consumable { get; set; }
}
