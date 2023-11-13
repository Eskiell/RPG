using RPG.Core;

namespace RPG.Effects;

public abstract class StatusEffect
{
    public StatusEffect(string name, int duration, float damagePerTick)
    {
        Name = name;
        Duration = duration;
        DamagePerTick = damagePerTick;
    }

    public string Name { get; set; }
    public int Duration { get; set; }
    public float DamagePerTick { get; set; }

    public abstract void ApplyEffect(Character character);
}