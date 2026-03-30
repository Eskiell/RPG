using RPG.Characters;

namespace RPG.Effects;

/// <summary>
/// Efeito de hemorragia que acumula uma barra de sangue e causa dano massivo ao atingir o máximo.
/// </summary>
public class HemorrhageEffect : StatusEffect
{
    private const float BloodBarMax = 100f;
    private readonly float _bleedDamage;
    private float _bloodBar;
    private Timer? _timer;

    /// <summary>
    /// Inicializa o efeito de hemorragia.
    /// </summary>
    /// <param name="duration">Duração máxima do efeito em segundos.</param>
    /// <param name="bleedDamage">Dano aplicado ao atingir a barra máxima.</param>
    public HemorrhageEffect(int duration, float bleedDamage)
        : base("Hemorrhage", duration, bleedDamage)
    {
        _bleedDamage = bleedDamage;
        _bloodBar = 0f;
    }

    /// <summary>
    /// Incrementa a barra de sangue pelo valor informado.
    /// </summary>
    /// <param name="amount">Quantidade a aumentar.</param>
    public void IncreaseBloodBar(float amount)
    {
        _bloodBar += amount;
    }

    /// <summary>
    /// Reduz a barra de sangue pelo valor informado (mínimo de 0).
    /// </summary>
    /// <param name="amount">Quantidade a reduzir.</param>
    public void DecreaseBloodBar(float amount)
    {
        _bloodBar = Math.Max(0f, _bloodBar - amount);
    }

    /// <summary>Zera a barra de sangue.</summary>
    public void ResetBloodBar()
    {
        _bloodBar = 0f;
    }

    /// <summary>
    /// Inicia o efeito de hemorragia ou incrementa a barra se já estiver ativo.
    /// </summary>
    /// <param name="character">Personagem afetado.</param>
    public override void ApplyEffect(Character character)
    {
        if (_timer != null)
        {
            Console.WriteLine($"{character.Name} add blood to bar");
            IncreaseBloodBar(10f);
            return;
        }

        _timer = new Timer(state => { Task.Run(() => OnTimerElapsed(character)); }, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
    }

    private void HemorrhageEffectEnd(Character character)
    {
        _timer?.Dispose();
        _timer = null;
        ResetBloodBar();
        Console.WriteLine($"{character.Name}'s hemorrhage effect has ended.");
    }

    private void OnTimerElapsed(Character character)
    {
        if (_bloodBar >= BloodBarMax)
        {
            character.TakeDamage(_bleedDamage);
            Console.WriteLine($"{character.Name} is hemorrhaging! Took {_bleedDamage} damage from hemorrhage.");
            HemorrhageEffectEnd(character);
        }

        Duration--;
        DecreaseBloodBar(5);
        if (Duration <= 0) HemorrhageEffectEnd(character);
    }

    /// <summary>
    /// Verifica se o efeito de hemorragia ainda está ativo.
    /// </summary>
    /// <returns><c>true</c> se o timer está rodando e ainda há duração restante.</returns>
    public bool IsActive()
    {
        return _timer != null && Duration > 0;
    }
}
