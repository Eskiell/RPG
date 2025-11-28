namespace RPG.Utils;

/// <summary>
/// Classe utilitária para logging de mensagens no console e arquivo.
/// </summary>
public static class Logger
{
    private static readonly string LogFilePath = Path.Combine("bin", "Debug", "net10.0", "logs.txt");

    /// <summary>
    /// Registra uma mensagem no console e no arquivo de log.
    /// </summary>
    /// <param name="message">Mensagem a ser registrada.</param>
    /// <param name="category">Categoria da mensagem (padrão: "Info").</param>
    public static void Log(string message, string category = "Info")
    {
        var formattedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{category}] {message}";
        Console.WriteLine(formattedMessage);
        
        try
        {
            var logDirectory = Path.GetDirectoryName(LogFilePath);
            if (!string.IsNullOrEmpty(logDirectory) && !Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            
            File.AppendAllText(LogFilePath, formattedMessage + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
        }
    }

    /// <summary>
    /// Limpa o arquivo de log, removendo-o se existir.
    /// </summary>
    public static void ClearLog()
    {
        if (File.Exists(LogFilePath)) File.Delete(LogFilePath);
    }
}

    public static void ClearLog()
    {
        if (File.Exists(LogFilePath)) File.Delete(LogFilePath);
    }
}