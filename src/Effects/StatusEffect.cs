using RPG.Core;

namespace RPG.Effects;

public abstract class StatusEffect
{
    protected StatusEffect(string name, int duration, float effectValue)
    {
        Name = name;
        Duration = duration;
        EffectValue = effectValue;
    }

    public string Name { get; private set; }
    public int Duration { get; set; }
    public float EffectValue { get; private set; }

    public abstract void ApplyEffect(Character character);

    public virtual void RemoveEffect(Character character)
    {
        // Pode ser sobrescrito por efeitos específicos
    }
}