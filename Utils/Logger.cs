namespace RPG.Utils;

public static class Logger
{
    private static readonly string LogFilePath = "logs.txt";

    public static void Log(string message, string category = "Info")
    {
        var formattedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{category}] {message}";
        Console.WriteLine(formattedMessage);
        // TODO: The file was been not created yet, so we need to create it
        File.AppendAllText(LogFilePath, formattedMessage + Environment.NewLine);
    }

    public static void ClearLog()
    {
        if (File.Exists(LogFilePath)) File.Delete(LogFilePath);
    }
}