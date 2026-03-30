using RPG.Attributes;
using RPG.Characters;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Combat;

/// <summary>
/// Fornece métodos estáticos para calcular ataque, defesa e modificadores de combate de um personagem.
/// </summary>
public static class CombatCalculator
{
    /// <summary>
    /// Calcula o ataque modificado do personagem, incluindo bônus de anéis e atributos.
    /// </summary>
    /// <param name="character">Personagem cujo ataque será calculado.</param>
    /// <returns>Valor total de ataque modificado.</returns>
    public static float CalculateModifiedAttack(Character character)
    {
        var modifiedAttack = character.BaseAttack;

        foreach (var ring in character.Equipment.EquippedRings)
        foreach (var scaling in ring.Scaling)
            if (scaling.Attribute == CharacterAttribute.Strength)
                modifiedAttack += character.CharacterPointsAttributes.Strength * scaling.Value;
            else if (scaling.Attribute == CharacterAttribute.Dexterity)
                modifiedAttack += character.CharacterPointsAttributes.Dexterity * scaling.Value;

        var bonus = StatCalculator.ComputeBonus(CharacterAttribute.Strength, character.CharacterPointsAttributes.Strength);
        modifiedAttack += bonus;

        return modifiedAttack;
    }

    /// <summary>
    /// Calcula a defesa modificada do personagem com base em Vitalidade, Resistência e Força.
    /// </summary>
    /// <param name="character">Personagem cuja defesa será calculada.</param>
    /// <returns>Valor total de defesa modificada.</returns>
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

    /// <summary>
    /// Calcula o ataque físico do personagem combinando o ataque modificado com bônus de Força e Destreza.
    /// </summary>
    /// <param name="character">Personagem cujo ataque físico será calculado.</param>
    /// <returns>Valor total de ataque físico.</returns>
    public static float CalculatePhysicalAttack(Character character)
    {
        var strength = character.CharacterPointsAttributes.Strength;
        var dexterity = character.CharacterPointsAttributes.Dexterity;
        var strengthBonus = StatCalculator.ComputeBonus(CharacterAttribute.Strength, strength);
        var dexterityBonus = StatCalculator.ComputeBonus(CharacterAttribute.Dexterity, dexterity);
        return CalculateModifiedAttack(character) + (strengthBonus * dexterityBonus);
    }

    /// <summary>
    /// Aplica os efeitos de <see cref="AttributeBuff"/> ativos ao valor base de um atributo.
    /// </summary>
    /// <param name="character">Personagem com efeitos ativos.</param>
    /// <param name="baseValue">Valor base do atributo.</param>
    /// <param name="attribute">Atributo alvo dos buffs.</param>
    /// <returns>Valor do atributo após aplicação dos buffs.</returns>
    public static float ApplyEffects(Character character, float baseValue, CharacterAttribute attribute)
    {
        foreach (var effect in character.ActiveEffects)
            if (effect is AttributeBuff buff && buff.TargetAttribute == attribute)
                baseValue += buff.BonusValue;

        return baseValue;
    }
}
