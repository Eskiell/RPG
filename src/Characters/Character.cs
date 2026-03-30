using RPG.Systems.Attributes;
using RPG.Calculators;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Core;

public abstract class Character
{
    /// <summary>
    /// Inicializa uma nova instância da classe Character.
    /// </summary>
    /// <param name="name">Nome do personagem.</param>
    /// <param name="baseHealth">Vida base do personagem.</param>
    /// <param name="pointsAttributes">Atributos do personagem.</param>
    /// <exception cref="ArgumentNullException">Lançada quando name ou pointsAttributes são null.</exception>
    /// <exception cref="ArgumentException">Lançada quando name está vazio ou baseHealth é negativo.</exception>
    public Character(string name, float baseHealth, PointsAttributes pointsAttributes)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo.", nameof(name));
        if (baseHealth < 0)
            throw new ArgumentException("Vida base não pode ser negativa.", nameof(baseHealth));
        if (pointsAttributes == null)
            throw new ArgumentNullException(nameof(pointsAttributes), "Atributos não podem ser nulos.");

        Name = name;
        BaseHealth = baseHealth;
        CharacterPointsAttributes = pointsAttributes;
        Equipment = new EquipmentManager(this);
        Health = GetMaxHealth();
    }

    public List<StatusEffect> ActiveEffects { get; set; } = new();

    // Atributos gerais
    public string Name { get; set; }
    public float Health { get; set; }
    public float BaseHealth { get; protected set; }
    public float BaseAttack { get; set; } = 10;
    public float BaseDefense { get; set; } = 10;
    public int Level { get; set; } = 1;

    public EquipmentManager Equipment { get; }
    public PointsAttributes CharacterPointsAttributes { get; protected set; }

    public float MaxStamina =>
        StatCalculator.ComputeBonus(CharacterAttribute.Endurance, CharacterPointsAttributes.Endurance) + 100f;

    public float MaxMana =>
        StatCalculator.ComputeBonus(CharacterAttribute.Intelligence, CharacterPointsAttributes.Intelligence)
        + StatCalculator.ComputeBonus(CharacterAttribute.Faith, CharacterPointsAttributes.Faith)
        + 50f;

    public float GetMaxHealth()
    {
        return StatCalculator.ComputeBonus(CharacterAttribute.Vitality, CharacterPointsAttributes.Vitality) +
               BaseHealth;
    }

    public float GetAttributeBonus(CharacterAttribute attribute)
    {
        var value = attribute switch
        {
            CharacterAttribute.Vitality => CharacterPointsAttributes.Vitality,
            CharacterAttribute.Vigor => CharacterPointsAttributes.Vigor,
            CharacterAttribute.Strength => CharacterPointsAttributes.Strength,
            CharacterAttribute.Dexterity => CharacterPointsAttributes.Dexterity,
            CharacterAttribute.Intelligence => CharacterPointsAttributes.Intelligence,
            CharacterAttribute.Faith => CharacterPointsAttributes.Faith,
            CharacterAttribute.Endurance => CharacterPointsAttributes.Endurance,
            CharacterAttribute.Arcane => CharacterPointsAttributes.Arcane,
            _ => 0
        };
        return StatCalculator.ComputeBonus(attribute, value);
    }

    public void UpdateEffects()
    {
        var expiredEffects = new List<StatusEffect>();
        foreach (var effect in ActiveEffects)
        {
            effect.Duration--;

            if (effect.Duration <= 0)
                expiredEffects.Add(effect);
        }

        foreach (var effect in expiredEffects) effect.RemoveEffect(this);
    }

    public abstract void Attack(Character target);

    public virtual float Defense()
    {
        return GetModifiedBaseDefense() * GetModifiedPDefense();
    }

    public virtual bool IsAlive()
    {
        return Health > 0;
    }

    public virtual bool IsDead()
    {
        return Health <= 0;
    }

    /// <summary>
    /// Aplica dano ao personagem.
    /// </summary>
    /// <param name="damage">Quantidade de dano a ser aplicada.</param>
    public virtual void TakeDamage(float damage)
    {
        if (damage < 0) damage = 0;
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public virtual float GetModifiedBaseAttack()
    {
        return CombatCalculator.CalculateModifiedAttack(this);
    }

    public virtual float GetModifiedBaseDefense()
    {
        return CombatCalculator.CalculateModifiedDefense(this);
    }

    public virtual float GetModifiedPDefense()
    {
        var vitalityBonus = GetAttributeBonus(CharacterAttribute.Vitality);
        var enduranceBonus = GetAttributeBonus(CharacterAttribute.Endurance);
        var strengthBonus = GetAttributeBonus(CharacterAttribute.Strength);

        return BaseDefense + vitalityBonus * enduranceBonus * strengthBonus;
    }

    public virtual float GetModifiedMDefense()
    {
        var intelligenceBonus = GetAttributeBonus(CharacterAttribute.Intelligence);
        var faithBonus = GetAttributeBonus(CharacterAttribute.Faith);
        var arcaneBonus = GetAttributeBonus(CharacterAttribute.Arcane);

        return BaseDefense + intelligenceBonus * faithBonus * arcaneBonus;
    }

    public virtual float GetModifiedPAttack()
    {
        return CombatCalculator.CalculatePhysicalAttack(this);
    }

    public void IncreaseLevelUp(int amount)
    {
        Level += amount;
    }

    /// <summary>
    /// Ajusta a vida do personagem (pode ser positivo ou negativo).
    /// </summary>
    /// <param name="amount">Quantidade a ajustar (positivo para curar, negativo para dano).</param>
    public void AdjustHealth(float amount)
    {
        Health += amount;
        var maxHealth = GetMaxHealth();
        if (Health < 0) Health = 0;
        if (Health > maxHealth) Health = maxHealth;
    }
}