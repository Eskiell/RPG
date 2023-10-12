using System.Text;
using NCalc;
using RPG.Core.Enum;

namespace RPG.Core;
public class Damage
{
    private DamageType Type { get; }
    private int Value { get; }
    private ScalingAttribute[] Scaling { get; set; }
    
    public Damage(DamageType type, int value, ScalingAttribute[] scaling)
    {
        Type = type;
        Value = value;
        Scaling = scaling;

    }

    public override string ToString()
    {
        return $"{Value} {Type} Damage";
    }
}