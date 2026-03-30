namespace RPG.Progression;

/// <summary>
/// Calcula a experiência necessária para subir de nível e o nível correspondente a uma quantidade de XP.
/// </summary>
public static class ExperienceCalculator
{
    /// <summary>
    /// Calcula a experiência total necessária para atingir o nível informado.
    /// </summary>
    /// <param name="level">Nível alvo.</param>
    /// <param name="basis">Base da curva de XP.</param>
    /// <param name="extra">XP extra por nível.</param>
    /// <param name="accA">Acelerador A da curva.</param>
    /// <param name="accB">Acelerador B da curva.</param>
    /// <returns>XP necessário para atingir o nível.</returns>
    public static double ExpForLevel(int level, int basis = 10, int extra = 20, int accA = 30, int accB = 30)
    {
        return Math.Round(basis * Math.Pow(level - 1, 0.9 + accA / 250.0) * level * (level + 1) /
            (6 + Math.Pow(level, 2) / 50.0 / accB) + (level - 1) * extra);
    }

    /// <summary>
    /// Determina o nível atual de um personagem com base na experiência acumulada.
    /// </summary>
    /// <param name="exp">Experiência acumulada.</param>
    /// <param name="basis">Base da curva de XP.</param>
    /// <param name="extra">XP extra por nível.</param>
    /// <param name="accA">Acelerador A da curva.</param>
    /// <param name="accB">Acelerador B da curva.</param>
    /// <returns>Nível correspondente à experiência informada.</returns>
    public static int LevelForExp(double exp, int basis = 10, int extra = 20, int accA = 30, int accB = 30)
    {
        var level = 1;
        while (ExpForLevel(level, basis, extra, accA, accB) <= exp) level++;
        return level - 1;
    }
}
