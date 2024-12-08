using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace RPG.Utils;

public static class Localization
{
    private static readonly Dictionary<string, Dictionary<string, string>> Messages = new();

    static Localization()
    {
        // TODO: Arquivo de localização deve ser carregado de acordo com a configuração do jogo
        LoadMessages("localization.json");
    }

    private static string CurrentLanguage { get; set; } = "en";

    public static void SetLanguage(string language)
    {
        if (!Messages.ContainsKey(language))
            throw new ArgumentException($"Language '{language}' not found.");
        CurrentLanguage = language;
    }

    public static string GetMessage(string key, Dictionary<string, string> placeholders)
    {
        if (Messages.TryGetValue(CurrentLanguage, out var languageMessages) &&
            languageMessages.TryGetValue(key, out var template))
        {
            foreach (var (placeholder, value) in placeholders)
                template = Regex.Replace(template, $"\\{{{placeholder}\\}}", value);
            return template;
        }

        return $"Message key '{key}' not found in language '{CurrentLanguage}'.";
    }

    private static void LoadMessages(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Warning: Localization file '{path}' not found. Default messages will be used.");
            return;
        }

        var jsonData = File.ReadAllText(path);
        var localizationData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonData);

        if (localizationData != null)
            foreach (var (language, translations) in localizationData)
                Messages[language] = translations;
    }
}