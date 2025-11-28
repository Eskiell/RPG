using RPG.Core;

namespace RPG.Effects;

public interface IWeaponEffect
{
    void ApplyEffect(Character attacker, Character target);
}