namespace RPG.Attributes;

public class AttributeScaler
{
    private float Softcap { get; } = 40f;
    private float Hardcap { get; } = 60f;
    public float GrowthLinear { get; set; } = 1f; // ganho por ponto antes do softcap
    public float GrowthSoft { get; set; } = 0.5f; // ganho entre softcap e hardcap
    public float GrowthHard { get; set; } = 0.1f; // ganho após o hardcap

    public float CalculateBonus(float attributeValue)
    {
        var bonus = 0f;

        if (attributeValue <= Softcap)
            bonus = attributeValue * GrowthLinear;
        else if (attributeValue <= Hardcap)
            bonus = Softcap * GrowthLinear +
                    (attributeValue - Softcap) * GrowthSoft;
        else
            bonus = Softcap * GrowthLinear +
                    (Hardcap - Softcap) * GrowthSoft +
                    (attributeValue - Hardcap) * GrowthHard;

        return bonus;
    }
}