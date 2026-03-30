using RPG.Characters;

namespace RPG.Enemies;

public abstract class Animal
{
    public Animal(string name, float health)
    {
        Name = name;
        Health = health;
    }

    public string Name { get; set; }
    public float Health { get; set; }
    public int Level { get; set; } = 1;

    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} faz um som.");
    }

    public virtual void Attack(Character target)
    {
        Console.WriteLine($"{Name} ataca {target.Name}!");
        // Aqui você pode adicionar a lógica de dano, defesa, etc.
        target.TakeDamage(Level * 10); // Exemplo de dano baseado no nível
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }

    protected virtual void Die()
    {
        Console.WriteLine($"{Name} morreu.");
    }
}