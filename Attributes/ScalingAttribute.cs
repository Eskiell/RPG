using RPG.Enums;

namespace RPG.Attributes;

public class ScalingAttribute(CharacterAttribute attribute, float value)
{
    public CharacterAttribute Attribute { get; private set; } = attribute;
    public float Value { get; private set; } = value;
}