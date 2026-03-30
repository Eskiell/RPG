using RPG.Characters;

namespace RPG.Effects;

public interface IWeaponEffect
{
    void ApplyEffect(Character attacker, Character target);
}