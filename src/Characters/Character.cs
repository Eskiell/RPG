using RPG.Attributes;
using RPG.Combat;
using RPG.Effects;
using RPG.Enums;

namespace RPG.Characters;

/// <summary>
/// Classe base abstrata para todos os personagens do jogo (jogadores e NPCs).
/// Define atributos, equipamentos, efeitos de status e comportamentos de combate.
/// </summary>
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

    /// <summary>Lista de efeitos de status ativos no personagem.</summary>
    public List<StatusEffect> ActiveEffects { get; set; } = new();

    /// <summary>Nome do personagem.</summary>
    public string Name { get; set; }

    /// <summary>Vida atual do personagem.</summary>
    public float Health { get; set; }

    /// <summary>Vida base usada no cálculo do HP máximo.</summary>
    public float BaseHealth { get; protected set; }

    /// <summary>Ataque base antes de modificadores.</summary>
    public float BaseAttack { get; set; } = 10;

    /// <summary>Defesa base antes de modificadores.</summary>
    public float BaseDefense { get; set; } = 10;

    /// <summary>Nível atual do personagem.</summary>
    public int Level { get; set; } = 1;

    /// <summary>Gerenciador de equipamentos do personagem.</summary>
    public EquipmentManager Equipment { get; }

    /// <summary>Pontos brutos dos atributos primários do personagem.</summary>
    public PointsAttributes CharacterPointsAttributes { get; protected set; }

    /// <summary>Stamina máxima calculada com base em Resistência.</summary>
    public float MaxStamina =>
        StatCalculator.ComputeBonus(CharacterAttribute.Endurance, CharacterPointsAttributes.Endurance) + 100f;

    /// <summary>MP máximo calculado com base em Inteligência e Fé.</summary>
    public float MaxMana =>
        StatCalculator.ComputeBonus(CharacterAttribute.Intelligence, CharacterPointsAttributes.Intelligence)
        + StatCalculator.ComputeBonus(CharacterAttribute.Faith, CharacterPointsAttributes.Faith)
        + 50f;

    /// <summary>Calcula o HP máximo com base em Vitalidade e vida base.</summary>
    /// <returns>HP máximo do personagem.</returns>
    public float GetMaxHealth()
    {
        return StatCalculator.ComputeBonus(CharacterAttribute.Vitality, CharacterPointsAttributes.Vitality) +
               BaseHealth;
    }

    /// <summary>
    /// Retorna o bônus calculado de um atributo específico.
    /// </summary>
    /// <param name="attribute">Atributo a ser calculado.</param>
    /// <returns>Bônus numérico do atributo.</returns>
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

    /// <summary>Decrementa a duração dos efeitos ativos e remove os que expiraram.</summary>
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

    /// <summary>Ataca o personagem alvo. Implementado por cada subclasse.</summary>
    /// <param name="target">Alvo do ataque.</param>
    public abstract void Attack(Character target);

    /// <summary>Calcula a defesa total do personagem.</summary>
    /// <returns>Valor de defesa combinada.</returns>
    public virtual float Defense()
    {
        return GetModifiedBaseDefense() * GetModifiedPDefense();
    }

    /// <summary>Verifica se o personagem ainda está vivo.</summary>
    /// <returns><c>true</c> se a vida for maior que zero.</returns>
    public virtual bool IsAlive()
    {
        return Health > 0;
    }

    /// <summary>Verifica se o personagem está morto.</summary>
    /// <returns><c>true</c> se a vida for zero ou menos.</returns>
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

    /// <summary>Retorna o ataque base do personagem após modificadores de anéis e atributos.</summary>
    /// <returns>Valor de ataque modificado.</returns>
    public virtual float GetModifiedBaseAttack()
    {
        return CombatCalculator.CalculateModifiedAttack(this);
    }

    /// <summary>Retorna a defesa base do personagem após modificadores.</summary>
    /// <returns>Valor de defesa modificada.</returns>
    public virtual float GetModifiedBaseDefense()
    {
        return CombatCalculator.CalculateModifiedDefense(this);
    }

    /// <summary>Calcula a defesa física do personagem com bônus de Vitalidade, Resistência e Força.</summary>
    /// <returns>Valor de defesa física.</returns>
    public virtual float GetModifiedPDefense()
    {
        var vitalityBonus = GetAttributeBonus(CharacterAttribute.Vitality);
        var enduranceBonus = GetAttributeBonus(CharacterAttribute.Endurance);
        var strengthBonus = GetAttributeBonus(CharacterAttribute.Strength);

        return BaseDefense + vitalityBonus * enduranceBonus * strengthBonus;
    }

    /// <summary>Calcula a defesa mágica do personagem com bônus de Inteligência, Fé e Arcano.</summary>
    /// <returns>Valor de defesa mágica.</returns>
    public virtual float GetModifiedMDefense()
    {
        var intelligenceBonus = GetAttributeBonus(CharacterAttribute.Intelligence);
        var faithBonus = GetAttributeBonus(CharacterAttribute.Faith);
        var arcaneBonus = GetAttributeBonus(CharacterAttribute.Arcane);

        return BaseDefense + intelligenceBonus * faithBonus * arcaneBonus;
    }

    /// <summary>Calcula o ataque físico do personagem com bônus de Força e Destreza.</summary>
    /// <returns>Valor de ataque físico.</returns>
    public virtual float GetModifiedPAttack()
    {
        return CombatCalculator.CalculatePhysicalAttack(this);
    }

    /// <summary>
    /// Aumenta o nível do personagem pela quantidade informada.
    /// </summary>
    /// <param name="amount">Quantidade de níveis a adicionar.</param>
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