using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Efeito de arma que aplica veneno no alvo ao acertar um ataque.
/// </summary>
public class WeaponPoisonEffect : IWeaponEffect
{
    private readonly float _damagePerTurn;
    private readonly int _duration;

    /// <summary>
    /// Inicializa o efeito de veneno da arma.
    /// </summary>
    /// <param name="damagePerTurn">Dano aplicado por turno no alvo envenenado.</param>
    /// <param name="duration">Duração do veneno em turnos.</param>
    /// <exception cref="ArgumentException">Lançada se damagePerTurn ou duration forem zero ou negativos.</exception>
    public WeaponPoisonEffect(float damagePerTurn, int duration)
    {
        if (damagePerTurn <= 0) throw new ArgumentException("Damage per turn must be greater than zero.");
        if (duration <= 0) throw new ArgumentException("Duration must be greater than zero.");

        _damagePerTurn = damagePerTurn;
        _duration = duration;
    }

    /// <summary>
    /// Aplica o veneno ao alvo quando a arma acerta.
    /// </summary>
    /// <param name="attacker">Personagem que realizou o ataque.</param>
    /// <param name="target">Personagem que receberá o veneno.</param>
    public void ApplyEffect(Character attacker, Character target)
    {
        var poison = new PoisonEffect(_damagePerTurn, _duration);
        poison.ApplyEffect(target);
        Console.WriteLine(
            $"{attacker.Name} poisoned {target.Name}! {_damagePerTurn} damage per turn for {_duration} turns.");
    }
}
