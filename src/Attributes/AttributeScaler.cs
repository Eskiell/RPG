namespace RPG.Attributes;

/// <summary>
/// Calcula o bônus de um atributo com base em limites de crescimento (softcap e hardcap).
/// </summary>
public class AttributeScaler
{
    private float Softcap { get; } = 40f;
    private float Hardcap { get; } = 60f;

    /// <summary>Ganho por ponto antes do softcap.</summary>
    public float GrowthLinear { get; set; } = 1f;

    /// <summary>Ganho por ponto entre o softcap e o hardcap.</summary>
    public float GrowthSoft { get; set; } = 0.5f;

    /// <summary>Ganho por ponto após o hardcap.</summary>
    public float GrowthHard { get; set; } = 0.1f;

    /// <summary>
    /// Calcula o bônus total para um determinado valor de atributo.
    /// </summary>
    /// <param name="attributeValue">Valor atual do atributo.</param>
    /// <returns>Bônus calculado com base nas faixas de crescimento.</returns>
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
