using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Efeito de sangramento que aplica dano contínuo ao personagem ao longo do tempo usando um timer em background.
/// </summary>
public class BleedingEffect : StatusEffect
{
    private Timer? _timer;

    /// <summary>
    /// Inicializa o efeito de sangramento.
    /// </summary>
    /// <param name="duration">Duração total em segundos.</param>
    /// <param name="bleedDamage">Dano total causado pelo sangramento.</param>
    public BleedingEffect(int duration, float bleedDamage)
        : base("Bleeding", duration, 10)
    {
        BleedDamage = bleedDamage;
        InitialDuration = duration;
    }

    /// <summary>Duração inicial do efeito (em segundos).</summary>
    public int InitialDuration { get; set; }

    /// <summary>Dano total distribuído ao longo da duração.</summary>
    public float BleedDamage { get; set; }

    /// <summary>
    /// Inicia o timer de sangramento se ainda não estiver ativo.
    /// </summary>
    /// <param name="character">Personagem que sofrerá o sangramento.</param>
    public override void ApplyEffect(Character character)
    {
        if (_timer != null)
            return;

        _timer = new Timer(state => { Task.Run(() => OnTimerElapsed(character)); }, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
    }

    private void OnTimerElapsed(Character character)
    {
        Console.WriteLine("Duration" + InitialDuration);
        var damagePerTick = BleedDamage / InitialDuration;

        character.TakeDamage(damagePerTick);
        Console.WriteLine($"{character.Name} is bleeding! Took {damagePerTick} damage from bleeding.");

        Duration--;

        if (Duration <= 0)
        {
            _timer?.Dispose();
            _timer = null;
            Console.WriteLine($"{character.Name}'s bleeding effect has ended.");
        }
    }

    /// <summary>
    /// Verifica se o efeito de sangramento ainda está ativo.
    /// </summary>
    /// <returns><c>true</c> se o timer está rodando e ainda há duração restante.</returns>
    public bool IsActive()
    {
        return _timer != null && Duration > 0;
    }
}
