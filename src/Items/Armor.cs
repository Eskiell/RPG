namespace RPG.Items;

/// <summary>
/// Armadura equipável que reduz o dano físico recebido pelo personagem.
/// </summary>
public class Armor : Equipment
{
    /// <summary>
    /// Inicializa uma nova armadura.
    /// </summary>
    /// <param name="name">Nome da armadura.</param>
    /// <param name="durability">Durabilidade inicial.</param>
    /// <param name="defenseValue">Valor de defesa física fornecido.</param>
    public Armor(string name, int durability, float defenseValue)
    {
        Name = name;
        Durability = durability;
        DefenseValue = defenseValue;
    }

    /// <summary>Valor de defesa física da armadura.</summary>
    public float DefenseValue { get; private set; }

    /// <summary>
    /// Reduz a durabilidade da armadura pelo valor informado.
    /// </summary>
    /// <param name="amount">Quantidade a reduzir.</param>
    public void DecreaseDurability(int amount)
    {
        Durability -= amount;
        if (Durability < 0) Durability = 0;
    }

    /// <summary>
    /// Repara a armadura aumentando sua durabilidade (máximo de 100).
    /// </summary>
    /// <param name="amount">Quantidade a restaurar.</param>
    public void RepairDurability(int amount)
    {
        Durability += amount;
        if (Durability > 100) Durability = 100;
    }
}
