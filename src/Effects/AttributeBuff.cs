using RPG.Characters;
using RPG.Enums;

namespace RPG.Effects;

public class AttributeBuff : StatusEffect
{
    public CharacterAttribute TargetAttribute { get; }
    public float BonusValue { get; }

    public AttributeBuff(string name, int duration, CharacterAttribute targetAttribute, float bonusValue)
        : base(name, duration, bonusValue)
    {
        TargetAttribute = targetAttribute;
        BonusValue = bonusValue;
    }

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