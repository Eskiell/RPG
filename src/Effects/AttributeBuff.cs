using RPG.Characters;
using RPG.Enums;

namespace RPG.Effects;

/// <summary>
/// Efeito de status que concede um bônus temporário a um atributo específico do personagem.
/// </summary>
public class AttributeBuff : StatusEffect
{
    /// <summary>Atributo que recebe o bônus.</summary>
    public CharacterAttribute TargetAttribute { get; }

    /// <summary>Valor do bônus adicionado ao atributo.</summary>
    public float BonusValue { get; }

    /// <summary>
    /// Inicializa um buff de atributo.
    /// </summary>
    /// <param name="name">Nome do buff.</param>
    /// <param name="duration">Duração em turnos.</param>
    /// <param name="targetAttribute">Atributo que será aumentado.</param>
    /// <param name="bonusValue">Quantidade de bônus.</param>
    public AttributeBuff(string name, int duration, CharacterAttribute targetAttribute, float bonusValue)
        : base(name, duration, bonusValue)
    {
        TargetAttribute = targetAttribute;
        BonusValue = bonusValue;
    }

    /// <summary>
    /// Adiciona o buff à lista de efeitos ativos do personagem.
    /// </summary>
    /// <param name="character">Personagem que receberá o buff.</param>
    public override void ApplyEffect(Character character)
    {
        character.ActiveEffects.Add(this);
        Console.WriteLine($"{character.Name} received a buff: {Name}, +{BonusValue} to {TargetAttribute}.");
    }

    /// <summary>
    /// Remove o buff da lista de efeitos ativos ao expirar.
    /// </summary>
    /// <param name="character">Personagem do qual o buff será removido.</param>
    public override void RemoveEffect(Character character)
    {
        character.ActiveEffects.Remove(this);
        Console.WriteLine($"{Name} buff on {character.Name} has expired.");
    }
}
