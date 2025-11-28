using RPG.Enums;

namespace RPG.Systems.Attributes;

public class ScalingAttribute
{
    public CharacterAttribute Attribute { get; private set; }
    public float Value { get; private set; }

    public ScalingAttribute(CharacterAttribute attribute, float value)
    {
        Attribute = attribute;
        Value = value;
    }
}