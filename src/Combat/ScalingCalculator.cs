using RPG.Attributes;
using RPG.Enums;

namespace RPG.Combat;

/// <summary>
/// Calcula o bônus de dano de uma arma com base nos atributos de escalonamento e nos pontos do personagem.
/// </summary>
public class ScalingCalculator
{
    private readonly Dictionary<CharacterAttribute, Func<PointsAttributes, float>> _attributeCalculators;

    /// <summary>
    /// Inicializa o calculador e mapeia cada atributo ao seu getter correspondente.
    /// </summary>
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

    /// <summary>
    /// Calcula o bônus total de escalonamento de uma arma para os atributos do personagem.
    /// </summary>
    /// <param name="scaling">Array de escalonamentos da arma.</param>
    /// <param name="attributes">Atributos do personagem portador.</param>
    /// <returns>Bônus total de dano escalonado.</returns>
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
