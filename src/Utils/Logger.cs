namespace RPG.Utils;

/// <summary>
/// Classe utilitária para logging de mensagens no console com suporte a múltiplas categorias e cores.
/// </summary>
public static class Logger
{
    /// <summary>Mensagem informativa geral (branco).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Info(string message) => Write("INFO", message, ConsoleColor.White);

    /// <summary>Mensagem de depuração, útil durante o desenvolvimento (cinza).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Debug(string message) => Write("DEBUG", message, ConsoleColor.Gray);

    /// <summary>Aviso sobre uma situação inesperada mas não crítica (amarelo).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Warning(string message) => Write("WARNING", message, ConsoleColor.Yellow);

    /// <summary>Erro que impede uma operação de ser concluída (vermelho).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Error(string message) => Write("ERROR", message, ConsoleColor.Red);

    /// <summary>Evento de combate como ataques e dano (magenta).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Combat(string message) => Write("COMBAT", message, ConsoleColor.Magenta);

    /// <summary>Evento de progressão como level up e XP ganho (ciano).</summary>
    /// <param name="message">Mensagem a ser exibida.</param>
    public static void Progression(string message) => Write("PROGRESSION", message, ConsoleColor.Cyan);

    /// <summary>
    /// Método genérico que imprime uma mensagem com categoria e cor personalizadas.
    /// </summary>
    /// <param name="category">Categoria exibida entre colchetes.</param>
    /// <param name="message">Mensagem a ser exibida.</param>
    /// <param name="color">Cor do texto no console.</param>
    public static void Log(string message, string category = "INFO", ConsoleColor color = ConsoleColor.White)
        => Write(category, message, color);

    private static void Write(string category, string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{category}] {message}");
        Console.ResetColor();
    }
}