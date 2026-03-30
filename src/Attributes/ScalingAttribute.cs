using RPG.Enums;

namespace RPG.Attributes;

/// <summary>
/// Representa o escalonamento de uma arma ou item em relação a um atributo específico do personagem.
/// </summary>
public class ScalingAttribute
{
    /// <summary>O atributo que influencia o escalonamento.</summary>
    public CharacterAttribute Attribute { get; private set; }

    /// <summary>Multiplicador de escalonamento aplicado ao atributo.</summary>
    public float Value { get; private set; }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ScalingAttribute"/>.
    /// </summary>
    /// <param name="attribute">Atributo de escalonamento.</param>
    /// <param name="value">Multiplicador do escalonamento.</param>
    public ScalingAttribute(CharacterAttribute attribute, float value)
    {
        Attribute = attribute;
        Value = value;
    }
}
