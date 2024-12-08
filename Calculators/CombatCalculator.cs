using RPG.Attributes;
using RPG.Core;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Calculators;

public static class CombatCalculator
{
    public static float CalculateModifiedAttack(Character character)
    {
        var modifiedAttack = character.BaseAttack;

        // Aumentos de ataque baseados em anéis
        foreach (var ring in character.Equipment.EquippedRings)
        foreach (var scaling in ring.Scaling)
            if (scaling.Attribute == CharacterAttribute.Strength)
                modifiedAttack += character.CharacterPointsAttributes.Strength * scaling.Value;
            else if (scaling.Attribute == CharacterAttribute.Dexterity)
                modifiedAttack += character.CharacterPointsAttributes.Dexterity * scaling.Value;

        // Bônus de atributos principais
        modifiedAttack *= character.CharacterPointsAttributes.Strength * AttributeBonus.Strength;

        return modifiedAttack;
    }

    public static float CalculateModifiedDefense(Character character)
    {
        var vitality = character.CharacterPointsAttributes.Vitality;
        var endurance = character.CharacterPointsAttributes.Endurance;
        var strength = character.CharacterPointsAttributes.Strength;

        return character.BaseDefense * vitality * endurance * strength *
               AttributeBonus.Vitality * AttributeBonus.Endurance * AttributeBonus.Strength;
    }

    public static float CalculatePhysicalAttack(Character character)
    {
        var strength = character.CharacterPointsAttributes.Strength;
        var dexterity = character.CharacterPointsAttributes.Dexterity;

        return CalculateModifiedAttack(character) * strength * dexterity *
               AttributeBonus.Strength * AttributeBonus.Dexterity;
    }

    public static float ApplyEffects(Character character, float baseValue, CharacterAttribute attribute)
    {
        foreach (var effect in character.ActiveEffects)
            if (effect is AttributeBuff buff && buff.TargetAttribute == attribute)
                baseValue += buff.BonusValue;

        return baseValue;
    }
}