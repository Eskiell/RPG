using RPG.Core;

namespace RPG.Effects;

public class BleedingEffect : StatusEffect
{
    private Timer? _timer;

    public BleedingEffect(int duration, float bleedDamage)
        : base("Bleeding", duration, 10)
    {
        BleedDamage = bleedDamage;
        InitialDuration = duration;
    }

    public int InitialDuration { get; set; }
    public float BleedDamage { get; set; }

    public override void ApplyEffect(Character character)
    {
        if (_timer != null)
            // O efeito de sangramento já está aplicado, não é necessário iniciar outro timer
            return;

        // Iniciar o timer para acionar o efeito de sangramento periodicamente em segundo plano
        _timer = new Timer(state => { Task.Run(() => OnTimerElapsed(character)); }, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
    }

    private void OnTimerElapsed(Character character)
    {
        Console.WriteLine("Duration" + InitialDuration);
        var damagePerTick = BleedDamage / InitialDuration; // Calculate the damage per tick

        character.TakeDamage(damagePerTick); // Apply the damage to the character
        Console.WriteLine($"{character.Name} is bleeding! Took {damagePerTick} damage from bleeding.");

        Duration--; // Decrease the remaining duration

        if (Duration <= 0)
        {
            // The bleeding effect has ended, stop the timer
            _timer?.Dispose();
            _timer = null; // Set the timer to null to allow reapplying the effect in the future
            Console.WriteLine($"{character.Name}'s bleeding effect has ended.");
        }
    }

    public bool IsActive()
    {
        return _timer != null && Duration > 0; // Verificar se o timer está ativo e a duração é maior que 0
    }
}