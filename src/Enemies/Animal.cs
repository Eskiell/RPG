using RPG.Characters;

namespace RPG.Enemies;

/// <summary>
/// Classe base abstrata para inimigos do tipo animal no mundo do jogo.
/// </summary>
public abstract class Animal
{
    /// <summary>
    /// Inicializa um novo animal com nome e vida.
    /// </summary>
    /// <param name="name">Nome do animal.</param>
    /// <param name="health">Vida inicial.</param>
    public Animal(string name, float health)
    {
        Name = name;
        Health = health;
    }

    /// <summary>Nome do animal.</summary>
    public string Name { get; set; }

    /// <summary>Vida atual do animal.</summary>
    public float Health { get; set; }

    /// <summary>Nível do animal, que influencia o dano do ataque.</summary>
    public int Level { get; set; } = 1;

    /// <summary>Emite o som característico do animal.</summary>
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} faz um som.");
    }

    /// <summary>
    /// Ataca um personagem causando dano baseado no nível.
    /// </summary>
    /// <param name="target">Personagem alvo do ataque.</param>
    public virtual void Attack(Character target)
    {
        Console.WriteLine($"{Name} ataca {target.Name}!");
        target.TakeDamage(Level * 10);
    }

    /// <summary>
    /// Verifica se o animal ainda está vivo.
    /// </summary>
    /// <returns><c>true</c> se a vida for maior que zero.</returns>
    public bool IsAlive()
    {
        return Health > 0;
    }

    /// <summary>
    /// Aplica dano ao animal, ativando a morte se a vida chegar a zero.
    /// </summary>
    /// <param name="damage">Quantidade de dano a ser aplicada.</param>
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }

    /// <summary>Chamado quando a vida do animal chega a zero.</summary>
    protected virtual void Die()
    {
        Console.WriteLine($"{Name} morreu.");
    }
}
