using RPG.Systems.Attributes;
using RPG.Enums;

namespace RPG.Systems.Combat;

public class Damage
{
    public Damage(List<DamageType> types, int value, ScalingAttribute[] scaling)
    {
        Types = types;
        Value = value;
        Scaling = scaling;
    }

    private List<DamageType> Types { get; }
    private int Value { get; }
    private ScalingAttribute[] Scaling { get; set; }

    public override string ToString()
    {
        var typesString = string.Join(" + ", Types);
        return $"{Value} [{typesString}] Damage";
    }
}