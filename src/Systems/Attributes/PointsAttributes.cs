namespace RPG.Systems.Attributes;

public class PointsAttributes
{
    public int Vitality { get; set; } = 10;
    public int Vigor { get; set; } = 10;
    public int Strength { get; set; } = 10;
    public int Dexterity { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int Faith { get; set; } = 10;
    public int Endurance { get; set; } = 10;
    public int Arcane { get; set; } = 10;

    public void AddVitalityPoints(int points)
    {
        Vitality += points;
    }

    public void AddVigorPoints(int points)
    {
        Vigor += points;
    }

    public void AddStrengthPoints(int points)
    {
        Strength += points;
    }

    public void AddDexterityPoints(int points)
    {
        Dexterity += points;
    }

    public void AddIntelligencePoints(int points)
    {
        Intelligence += points;
    }

    public void AddEndurancePoints(int points)
    {
        Endurance += points;
    }

    public void AddFaithPoints(int points)
    {
        Faith += points;
    }

    public void AddArcanePoints(int points)
    {
        Arcane += points;
    }
}