using RPG.Attributes;
using RPG.Enums;

namespace RPG.Combat;

public class ScalingCalculator
{
    private readonly Dictionary<CharacterAttribute, Func<PointsAttributes, float>> _attributeCalculators;

    public ScalingCalculator()
    {
        _attributeCalculators = new Dictionary<CharacterAttribute, Func<PointsAttributes, float>>
        {
            { CharacterAttribute.Vitality, attributes => attributes.Vitality },
            { CharacterAttribute.Vigor, attributes => attributes.Vigor },
            { CharacterAttribute.Strength, attributes => attributes.Strength },
            { CharacterAttribute.Dexterity, attributes => attributes.Dexterity },
            { CharacterAttribute.Intelligence, attributes => attributes.Intelligence },
            { CharacterAttribute.Faith, attributes => attributes.Faith },
            { CharacterAttribute.Endurance, attributes => attributes.Endurance },
            { CharacterAttribute.Arcane, attributes => attributes.Arcane }
        };
    }

    public float CalculateScaling(ScalingAttribute[] scaling, PointsAttributes attributes)
    {
        float modifiedValue = 0;

        foreach (var scalingAttribute in scaling)
            if (_attributeCalculators.TryGetValue(scalingAttribute.Attribute, out var attributeCalculator))
            {
                var attributeValue = attributeCalculator(attributes);
                modifiedValue += attributeValue * scalingAttribute.Value;
            }

        return modifiedValue;
    }
}