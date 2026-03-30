namespace RPG.Items;

/// <summary>
/// Classe base abstrata para equipamentos (armas, armaduras, anéis).
/// </summary>
public abstract class Equipment
{
    /// <summary>Nome do equipamento.</summary>
    public string Name { get; set; }

    /// <summary>Durabilidade atual do equipamento.</summary>
    public int Durability { get; set; }
}
