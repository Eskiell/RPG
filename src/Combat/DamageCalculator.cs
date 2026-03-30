namespace RPG.Combat;

/// <summary>
/// Calcula o dano final de um ataque aplicando modificadores de ataque, defesa e variância aleatória.
/// </summary>
public class DamageCalculator
{
    private const int DamageVariance = 10;
    private readonly float _baseDamage;
    private readonly float _damageModifier;
    private readonly Random _random;
    private readonly float _targetDefense;
    private readonly float _targetModifier;

    /// <summary>
    /// Inicializa a calculadora de dano.
    /// </summary>
    /// <param name="baseDamage">Dano base do atacante.</param>
    /// <param name="damageModifier">Modificador de dano do atacante (ex.: bônus de atributo).</param>
    /// <param name="targetDefense">Defesa do alvo.</param>
    /// <param name="targetModifier">Modificador de defesa do alvo.</param>
    public DamageCalculator(float baseDamage, float damageModifier, float? targetDefense = 0,
        float? targetModifier = 0)
    {
        _baseDamage = baseDamage;
        _damageModifier = damageModifier;
        _targetModifier = targetModifier ?? 0;
        _targetDefense = targetDefense ?? 0;
        _random = new Random();
    }

    private double Variance(float damage)
    {
        var varianceMultiplier = 1f - DamageVariance / 100f;
        var minDamage = damage * varianceMultiplier;
        var maxDamage = damage / varianceMultiplier;
        return _random.NextDouble() * (maxDamage - minDamage) + minDamage;
    }

    /// <summary>
    /// Retorna a fórmula de dano utilizada no cálculo.
    /// </summary>
    /// <returns>String com a expressão da fórmula.</returns>
    public string Formula()
    {
        return "baseDamage * (1 + damageModifier) * (1 + targetModifier) - targetDefense";
    }

    /// <summary>
    /// Calcula o dano total do ataque com variância aleatória de ±10%.
    /// </summary>
    /// <returns>Dano final arredondado.</returns>
    public float CalculateTotalDamage()
    {
        var damage = _baseDamage * (1 + _damageModifier) * (1 + _targetModifier) - _targetDefense;
        Console.WriteLine($"Formula: {_baseDamage} * (1 + {_damageModifier}) * (1 + {_targetModifier}) - {_targetDefense} = {damage}");
        return (float)Math.Round(Math.Max(Variance(damage), Variance(10)));
    }
}
