using RPG.Characters;

namespace RPG.Effects;

public class WeaponPoisonEffect : IWeaponEffect
{
    private readonly float _damagePerTurn;
    private readonly int _duration;

    public WeaponPoisonEffect(float damagePerTurn, int duration)
    {
        if (damagePerTurn <= 0) throw new ArgumentException("Damage per turn must be greater than zero.");
        if (duration <= 0) throw new ArgumentException("Duration must be greater than zero.");

        _damagePerTurn = damagePerTurn;
        _duration = duration;
    }

    public void ApplyEffect(Character attacker, Character target)
    {
        var poison = new PoisonEffect(_damagePerTurn, _duration);
        poison.ApplyEffect(target);
        Console.WriteLine(
            $"{attacker.Name} poisoned {target.Name}! {_damagePerTurn} damage per turn for {_duration} turns.");
    }
}