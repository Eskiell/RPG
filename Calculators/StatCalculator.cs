using RPG.Attributes;
using RPG.Enums;

namespace RPG.Calculators;

public class StatCalculator
{
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