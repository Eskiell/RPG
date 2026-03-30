using RPG.Attributes;
using RPG.Characters;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Combat;

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
        var bonus = StatCalculator.ComputeBonus(CharacterAttribute.Strength, character.CharacterPointsAttributes.Strength);
        modifiedAttack += bonus;

        return modifiedAttack;
    }

    public static float CalculateModifiedDefense(Character character)
    {
        var vitality = character.CharacterPointsAttributes.Vitality;
        var endurance = character.CharacterPointsAttributes.Endurance;
        var strength = character.CharacterPointsAttributes.Strength;
        var vitalityBonus = StatCalculator.ComputeBonus(CharacterAttribute.Vitality, vitality);
        var enduranceBonus = StatCalculator.ComputeBonus(CharacterAttribute.Endurance, endurance);
        var strengthBonus = StatCalculator.ComputeBonus(CharacterAttribute.Strength, strength);
        return character.BaseDefense + (vitalityBonus * enduranceBonus * strengthBonus);

    }

    public static float CalculatePhysicalAttack(Character character)
    {
        var strength = character.CharacterPointsAttributes.Strength;
        var dexterity = character.CharacterPointsAttributes.Dexterity;
        var strengthBonus = StatCalculator.ComputeBonus(CharacterAttribute.Strength, strength);
        var dexterityBonus = StatCalculator.ComputeBonus(CharacterAttribute.Dexterity, dexterity);
        return CalculateModifiedAttack(character) + (strengthBonus * dexterityBonus);

    }

    public static float ApplyEffects(Character character, float baseValue, CharacterAttribute attribute)
    {
        foreach (var effect in character.ActiveEffects)
            if (effect is AttributeBuff buff && buff.TargetAttribute == attribute)
                baseValue += buff.BonusValue;

        return baseValue;
    }
}