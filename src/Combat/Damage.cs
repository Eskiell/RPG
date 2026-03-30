using RPG.Attributes;
using RPG.Enums;

namespace RPG.Combat;

/// <summary>
/// Representa um pacote de dano com tipos, valor base e atributos de escalonamento.
/// </summary>
public class Damage
{
    /// <summary>
    /// Inicializa uma instância de dano.
    /// </summary>
    /// <param name="types">Tipos de dano (ex.: físico, mágico).</param>
    /// <param name="value">Valor base do dano.</param>
    /// <param name="scaling">Atributos que escalonam o dano.</param>
    public Damage(List<DamageType> types, int value, ScalingAttribute[] scaling)
    {
        Types = types;
        Value = value;
        Scaling = scaling;
    }

    private List<DamageType> Types { get; }
    private int Value { get; }
    private ScalingAttribute[] Scaling { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var typesString = string.Join(" + ", Types);
        return $"{Value} [{typesString}] Damage";
    }
}
