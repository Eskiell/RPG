using RPG.Progression;
using RPG.Items;

namespace RPG.Progression;

/// <summary>
/// Gerencia a tabela de drops e realiza sorteios aleatórios para determinar os itens obtidos.
/// </summary>
public class DropCalculator
{
    private readonly List<ItemDrop> _dropTable;
    private readonly Random _random;

    /// <summary>Inicializa o calculador com uma tabela de drops vazia.</summary>
    public DropCalculator()
    {
        _dropTable = new List<ItemDrop>();
        _random = new Random();
    }

    /// <summary>
    /// Adiciona um item à tabela de drops com sua respectiva taxa de queda.
    /// </summary>
    /// <param name="item">Item a ser adicionado.</param>
    /// <param name="dropRate">Taxa de drop entre 0.0 e 1.0.</param>
    public void AddItemDrop(Item item, float dropRate)
    {
        var itemDrop = new ItemDrop(item, dropRate);
        _dropTable.Add(itemDrop);
    }

    /// <summary>
    /// Sorteia aleatoriamente os itens dropados com base nas taxas configuradas.
    /// </summary>
    /// <returns>Lista de itens obtidos no drop.</returns>
    public List<Item> GetRandomItemDrop()
    {
        var randomItems = new List<Item>();

        foreach (var itemDrop in _dropTable)
            if (_random.NextDouble() < itemDrop.DropRate && itemDrop.Item != null)
                randomItems.Add(itemDrop.Item);

        return randomItems;
    }
}
