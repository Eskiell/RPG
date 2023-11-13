using RPG.Attributes;

namespace RPG.Calculators;

public class ScalingCalculator
{
    private readonly Dictionary<string, Func<PointsAttributes, float>> attributeCalculators;

    public ScalingCalculator()
    {
        attributeCalculators = new Dictionary<string, Func<PointsAttributes, float>>
        {
            { "vitality", attributes => attributes.Vitality },
            { "vigor", attributes => attributes.Vigor },
            { "strength", attributes => attributes.Strength },
            { "dexterity", attributes => attributes.Dexterity },
            { "intelligence", attributes => attributes.Intelligence },
            { "faith", attributes => attributes.Faith },
            { "endurance", attributes => attributes.Endurance },
            { "arcane", attributes => attributes.Arcane }
        };
    }

    public float CalculateScaling(ScalingAttribute[] scaling, PointsAttributes attributes)
    {
        float modifiedValue = 0;

        foreach (var scalingAttribute in scaling)
            if (attributeCalculators.TryGetValue(scalingAttribute.Attribute, out var attributeCalculator))
            {
                var attributeValue = attributeCalculator(attributes);
                modifiedValue += attributeValue * scalingAttribute.Value;
            }

        return modifiedValue;
    }
}