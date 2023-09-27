namespace RPG.Core;

public static class ExperienceCalculator
{
    public static double ExpForLevel(int level, int basis = 10, int extra = 20, int accA = 30, int accB = 30)
    {
        return Math.Round(basis * Math.Pow(level - 1, 0.9 + accA / 250.0) * level * (level + 1) /
            (6 + Math.Pow(level, 2) / 50.0 / accB) + (level - 1) * extra);
    }

    public static int LevelForExp(double exp, int basis = 10, int extra = 20, int accA = 30, int accB = 30)
    {
        var level = 1;
        while (ExpForLevel(level, basis, extra, accA, accB) <= exp) level++;
        return level - 1;
    }
}