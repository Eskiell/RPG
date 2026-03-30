using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Efeito de envenenamento que adiciona dano por turno ao personagem afetado.
/// </summary>
public class PoisonEffect : StatusEffect
{
    /// <summary>
    /// Inicializa o efeito de veneno.
    /// </summary>
    /// <param name="damagePerTurn">Dano aplicado a cada turno.</param>
    /// <param name="duration">Número de turnos de duração.</param>
    public PoisonEffect(float damagePerTurn, int duration)
        : base("Poison", duration, damagePerTurn)
    {
    }

    /// <summary>
    /// Aplica o veneno ao personagem, adicionando-o aos efeitos ativos.
    /// </summary>
    /// <param name="character">Personagem que sofrerá o envenenamento.</param>
    public override void ApplyEffect(Character character)
    {
        character.ActiveEffects.Add(this);
        Console.WriteLine(
            $"{character.Name} has been poisoned! {EffectValue} damage per turn for {Duration} turns.");
    }
}
