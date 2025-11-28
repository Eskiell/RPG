using Newtonsoft.Json;
using RPG.Entities.Items;

namespace RPG.Utils;

public class LoadItems
{
    public LoadItems()
    {
        var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        var itemsPath = Path.Combine(projectPath, "Data", "Database", "items.json");
        Items = FromJson(itemsPath);
    }

    public List<Item> Items { get; set; }

    private List<Item> FromJson(string path)
    {
        var jsonData = File.ReadAllText(path);
        var items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
        return items ?? new List<Item>();
    }
}