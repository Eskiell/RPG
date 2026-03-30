using RPG.Attributes;
using RPG.Enums;

namespace RPG.Attributes;

/// <summary>
/// Calcula os bônus finais de atributos aplicando as curvas de crescimento corretas para cada stat.
/// </summary>
public class StatCalculator
{
    /// <summary>
    /// Calcula o bônus total de um atributo dado o número de pontos alocados.
    /// </summary>
    /// <param name="attr">Atributo a ser calculado.</param>
    /// <param name="points">Pontos alocados no atributo.</param>
    /// <returns>Bônus numérico resultante.</returns>
    public static float ComputeBonus(CharacterAttribute attr, int points)
    {
        var scaler = GetScalerFor(attr);
        return scaler.CalculateBonus(points);
    }

    private static AttributeScaler GetScalerFor(CharacterAttribute attr)
    {
        return attr switch
        {
            CharacterAttribute.Vitality => new AttributeScaler
                { GrowthLinear = 10f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Vigor => new AttributeScaler { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Strength => new AttributeScaler
                { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Dexterity => new AttributeScaler
                { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Intelligence => new AttributeScaler
                { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Faith => new AttributeScaler { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Endurance => new AttributeScaler
                { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            CharacterAttribute.Arcane => new AttributeScaler { GrowthLinear = 5f, GrowthSoft = 2f, GrowthHard = 0.5f },
            _ => new AttributeScaler()
        };
    }
}
