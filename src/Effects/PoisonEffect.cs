using RPG.Characters;

namespace RPG.Effects;

public class PoisonEffect : StatusEffect
{
    public PoisonEffect(float damagePerTurn, int duration)
        : base("Poison", duration, damagePerTurn)
    {
    }

    public override void ApplyEffect(Character character)
    {
        character.ActiveEffects.Add(this);
        Console.WriteLine(
            $"{character.Name} has been poisoned! {EffectValue} damage per turn for {Duration} turns.");
    }
}