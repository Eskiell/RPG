using RPG.Characters;

namespace RPG.Effects;

public class HemorrhageEffect : StatusEffect
{
    private const float BloodBarMax = 100f;
    private readonly float _bleedDamage;
    private float _bloodBar;
    private Timer? _timer;

    public HemorrhageEffect(int duration, float bleedDamage)
        : base("Hemorrhage", duration, bleedDamage)
    {
        _bleedDamage = bleedDamage;
        _bloodBar = 0f;
    }

    public void IncreaseBloodBar(float amount)
    {
        _bloodBar += amount;
    }

    public void DecreaseBloodBar(float amount)
    {
        _bloodBar = Math.Max(0f, _bloodBar - amount);
    }

    public void ResetBloodBar()
    {
        _bloodBar = 0f;
    }

    public override void ApplyEffect(Character character)
    {
        if (_timer != null)
        {
            Console.WriteLine($"{character.Name} add blood to bar");
            IncreaseBloodBar(10f);
            return;
        }

        // Iniciar o timer para acionar o efeito de hemorragia periodicamente
        _timer = new Timer(state => { Task.Run(() => OnTimerElapsed(character)); }, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
    }

    private void HemorrhageEffectEnd(Character character)
    {
        // O efeito de hemorragia terminou, parar o timer
        _timer?.Dispose();
        _timer = null; // Definir o timer como nulo para permitir reaplicar o efeito no futuro
        ResetBloodBar();
        Console.WriteLine($"{character.Name}'s hemorrhage effect has ended.");
    }

    private void OnTimerElapsed(Character character)
    {
        if (_bloodBar >= BloodBarMax)
        {
            character.TakeDamage(_bleedDamage); // Aplicar o dano total ao personagem
            Console.WriteLine($"{character.Name} is hemorrhaging! Took {_bleedDamage} damage from hemorrhage.");
            HemorrhageEffectEnd(character);
        }

        Duration--; // Diminuir a duração restante
        DecreaseBloodBar(5);
        if (Duration <= 0) HemorrhageEffectEnd(character);
    }

    public bool IsActive()
    {
        return _timer != null && Duration > 0; // Verificar se o timer está ativo e a duração é maior que 0
    }
}