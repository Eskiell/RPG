using System.Text;
using NCalc;

namespace RPG.Core;

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
        var formulaBuilder = new StringBuilder();
        formulaBuilder.Append($"{_baseDamage} * (1 + {_damageModifier}) * (1 + {_targetModifier}) - {_targetDefense}");
        return formulaBuilder.ToString();
    }

    public float CalculateTotalDamage()
    {
        var formula = Formula();
        var expression = new Expression(formula)
        {
            Parameters =
            {
                ["baseDamage"] = _baseDamage,
                ["damageModifier"] = _damageModifier,
                ["targetModifier"] = _targetModifier,
                ["targetDefense"] = _targetDefense
            }
        };

        var damage = Convert.ToSingle(expression.Evaluate());
        Console.WriteLine("Fórmula: " + formula + " = " + damage);
        return (float)Math.Round(Math.Max(Variance(damage), Variance(10)));
    }
}