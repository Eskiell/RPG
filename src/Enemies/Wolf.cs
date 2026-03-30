namespace RPG.Enemies;

/// <summary>
/// Inimigo do tipo lobo com uivo como som característico.
/// </summary>
public class Wolf : Animal
{
    /// <summary>
    /// Inicializa um novo lobo com nome e vida.
    /// </summary>
    /// <param name="name">Nome do lobo.</param>
    /// <param name="health">Vida inicial.</param>
    public Wolf(string name, float health) : base(name, health)
    {
    }

    /// <inheritdoc/>
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} uiva.");
    }

    /// <summary>Faz o lobo uivar para a lua.</summary>
    public void HowlAtTheMoon()
    {
        Console.WriteLine($"{Name} uiva para a lua.");
    }
}
