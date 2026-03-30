using RPG.Attributes;

namespace RPG.Items;

/// <summary>
/// Anel equipável que concede bônus de atributos via escalonamento.
/// </summary>
public class Ring
{
    /// <summary>
    /// Inicializa um novo anel.
    /// </summary>
    /// <param name="name">Nome do anel.</param>
    /// <param name="description">Descrição dos bônus concedidos.</param>
    /// <param name="scaling">Atributos escalonados pelo anel.</param>
    public Ring(string name, string description, ScalingAttribute[] scaling)
    {
        Name = name;
        Description = description;
        Scaling = scaling;
    }

    /// <summary>Nome do anel.</summary>
    public string Name { get; private set; }

    /// <summary>Descrição dos efeitos do anel.</summary>
    public string Description { get; private set; }

    /// <summary>Durabilidade atual do anel.</summary>
    public int Durability { get; private set; } = 100;

    /// <summary>Atributos e multiplicadores de escalonamento do anel.</summary>
    public ScalingAttribute[] Scaling { get; private set; }

    /// <summary>
    /// Reduz a durabilidade do anel pelo valor informado.
    /// </summary>
    /// <param name="amount">Quantidade a reduzir.</param>
    public void DecreaseDurability(int amount)
    {
        Durability -= amount;
        if (Durability < 0) Durability = 0;
    }

    /// <summary>
    /// Repara o anel aumentando sua durabilidade (máximo de 100).
    /// </summary>
    /// <param name="amount">Quantidade a restaurar.</param>
    public void RepairDurability(int amount)
    {
        Durability += amount;
        if (Durability > 100) Durability = 100;
    }
}
