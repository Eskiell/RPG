using RPG.Attributes;
using RPG.Enums;

namespace RPG.Damages;

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
        string typesString = string.Join(" + ", Types);
        return $"{Value} [{typesString}] Damage";
    }
}