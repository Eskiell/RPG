using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Classe base para todos os efeitos de status aplicáveis a um personagem.
/// </summary>
public abstract class StatusEffect
{
    /// <summary>
    /// Inicializa um efeito de status.
    /// </summary>
    /// <param name="name">Nome do efeito.</param>
    /// <param name="duration">Duração em turnos.</param>
    /// <param name="effectValue">Valor do efeito (dano, bônus, etc.).</param>
    protected StatusEffect(string name, int duration, float effectValue)
    {
        Name = name;
        Duration = duration;
        EffectValue = effectValue;
    }

    /// <summary>Nome identificador do efeito.</summary>
    public string Name { get; private set; }

    /// <summary>Turnos restantes de duração do efeito.</summary>
    public int Duration { get; set; }

    /// <summary>Valor numérico do efeito (dano por turno, bônus de atributo, etc.).</summary>
    public float EffectValue { get; private set; }

    /// <summary>
    /// Aplica o efeito ao personagem alvo.
    /// </summary>
    /// <param name="character">Personagem que receberá o efeito.</param>
    public abstract void ApplyEffect(Character character);

    /// <summary>
    /// Remove o efeito do personagem quando a duração expira.
    /// </summary>
    /// <param name="character">Personagem do qual o efeito será removido.</param>
    public virtual void RemoveEffect(Character character)
    {
        // Pode ser sobrescrito por efeitos específicos
    }
}
