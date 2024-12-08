using RPG.Core;

namespace RPG.Effects;

public class PoisonEffect(float damagePerTurn, int duration) : StatusEffect("Poison", duration, damagePerTurn)
{
    public override void ApplyEffect(Character character)
    {
        character.ActiveEffects.Add(this);
        Console.WriteLine(
            $"{character.Name} has been poisoned! {EffectValue} damage per turn for {Duration} turns.");
    }
} 