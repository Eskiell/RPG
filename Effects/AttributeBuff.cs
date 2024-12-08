using RPG.Core;
using RPG.Enums;

namespace RPG.Effects;

public class AttributeBuff(string name, int duration, CharacterAttribute targetAttribute, float bonusValue)
    : StatusEffect(name, duration, bonusValue)
{
    public CharacterAttribute TargetAttribute { get; } = targetAttribute;
    public float BonusValue { get; } = bonusValue;

    public override void ApplyEffect(Character character)
    {
        // Buff aplicado
        character.ActiveEffects.Add(this);
        Console.WriteLine($"{character.Name} received a buff: {Name}, +{BonusValue} to {TargetAttribute}.");
    }

    public override void RemoveEffect(Character character)
    {
        // Remover o buff ao fim da duração
        character.ActiveEffects.Remove(this);
        Console.WriteLine($"{Name} buff on {character.Name} has expired.");
    }
}