using RPG.Attributes;
using RPG.Enums;

namespace RPG.Damages;

public class Damage
{
    public Damage(DamageType type, int value, ScalingAttribute[] scaling)
    {
        Type = type;
        Value = value;
        Scaling = scaling;
    }

    private DamageType Type { get; }
    private int Value { get; }
    private ScalingAttribute[] Scaling { get; set; }

    public override string ToString()
    {
        return $"{Value} {Type} Damage";
    }
}