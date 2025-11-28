namespace RPG.Calculators;

public class DamageCalculator
{
    private const int DamageVariance = 10;
    private readonly float _baseDamage;
    private readonly float _damageModifier;
    private readonly Random _random;
    private readonly float _targetDefense;
    private readonly float _targetModifier;

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

    public string Formula()
    {
        // Use parameter placeholders for NCalc
        return "baseDamage * (1 + damageModifier) * (1 + targetModifier) - targetDefense";
    }

    public float CalculateTotalDamage()
    {
        // Directly calculate damage to avoid NCalc parsing issues
        var damage = _baseDamage * (1 + _damageModifier) * (1 + _targetModifier) - _targetDefense;
        Console.WriteLine($"Formula: {_baseDamage} * (1 + {_damageModifier}) * (1 + {_targetModifier}) - {_targetDefense} = {damage}");

        return (float)Math.Round(Math.Max(Variance(damage), Variance(10)));
    }
}