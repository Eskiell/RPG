using Newtonsoft.Json;
using RPG.Items;

namespace RPG.Utils;

/// <summary>
/// Carrega a lista de itens do arquivo JSON de banco de dados do jogo.
/// </summary>
public class LoadItems
{
    /// <summary>
    /// Inicializa o loader e carrega os itens do arquivo <c>Data/Database/items.json</c>.
    /// </summary>
    public LoadItems()
    {
        var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        var itemsPath = Path.Combine(projectPath, "Data", "Database", "items.json");
        Items = FromJson(itemsPath);
    }

    /// <summary>Lista de itens carregados do arquivo JSON.</summary>
    public List<Item> Items { get; set; }

    private List<Item> FromJson(string path)
    {
        var jsonData = File.ReadAllText(path);
        var items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
        return items ?? new List<Item>();
    }
}
