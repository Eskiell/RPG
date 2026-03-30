using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Contrato para efeitos especiais aplicados por armas ao acertar um alvo.
/// </summary>
public interface IWeaponEffect
{
    /// <summary>
    /// Aplica o efeito da arma ao alvo.
    /// </summary>
    /// <param name="attacker">Personagem que realizou o ataque.</param>
    /// <param name="target">Personagem que receberá o efeito.</param>
    void ApplyEffect(Character attacker, Character target);
}
