using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace RPG.Utils;

/// <summary>
/// Sistema de localização que carrega strings de tradução de um arquivo JSON e as fornece por chave.
/// </summary>
public static class Localization
{
    private static readonly Dictionary<string, Dictionary<string, string>> Messages = new();

    static Localization()
    {
        var projectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        var localizationPath = Path.Combine(projectPath ?? ".", "Data", "localization.json");
        LoadMessages(localizationPath);
    }

    private static string CurrentLanguage { get; set; } = "en";

    /// <summary>
    /// Define o idioma ativo para as mensagens retornadas.
    /// </summary>
    /// <param name="language">Código do idioma (ex.: "en", "pt").</param>
    /// <exception cref="ArgumentException">Lançada se o idioma não estiver disponível.</exception>
    public static void SetLanguage(string language)
    {
        if (!Messages.ContainsKey(language))
            throw new ArgumentException($"Language '{language}' not found.");
        CurrentLanguage = language;
    }

    /// <summary>
    /// Retorna uma mensagem traduzida, substituindo os placeholders pelos valores informados.
    /// </summary>
    /// <param name="key">Chave da mensagem no arquivo de localização.</param>
    /// <param name="placeholders">Dicionário de substitutos (chave → valor).</param>
    /// <returns>Mensagem traduzida com placeholders substituídos.</returns>
    public static string GetMessage(string key, Dictionary<string, string> placeholders)
    {
        if (Messages.TryGetValue(CurrentLanguage, out var languageMessages) &&
            languageMessages.TryGetValue(key, out var template))
        {
            foreach (var (placeholder, value) in placeholders)
                template = Regex.Replace(template, $"\{{{placeholder}\}}", value);
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
