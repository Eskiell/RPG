using RPG.Characters;
using RPG.Effects;

namespace RPG.Items;

/// <summary>
/// Classe base abstrata para armas equipáveis pelo personagem.
/// </summary>
public abstract class Weapon : Equipment
{
    /// <summary>
    /// Inicializa uma arma com os atributos básicos.
    /// </summary>
    /// <param name="name">Nome da arma.</param>
    /// <param name="damage">Dano base da arma.</param>
    /// <param name="attackSpeed">Velocidade de ataque.</param>
    /// <param name="durability">Durabilidade inicial.</param>
    public Weapon(string name, float damage, float attackSpeed, int durability)
    {
        Name = name;
        Damage = damage;
        AttackSpeed = attackSpeed;
        Durability = durability;
    }

    /// <summary>Lista de efeitos especiais aplicados ao acertar um alvo.</summary>
    public List<IWeaponEffect> Effects { get; } = new();

    /// <summary>Nome da arma (sobrescreve Equipment.Name).</summary>
    public new string Name { get; set; }

    /// <summary>Dano base da arma.</summary>
    public float Damage { get; set; }

    /// <summary>Velocidade de ataque da arma.</summary>
    public float AttackSpeed { get; set; }

    /// <summary>Durabilidade atual da arma (sobrescreve Equipment.Durability).</summary>
    public new int Durability { get; set; }

    /// <summary>
    /// Repara a arma aumentando sua durabilidade.
    /// </summary>
    /// <param name="amount">Quantidade de durabilidade a restaurar.</param>
    public void Repair(int amount)
    {
        Durability += amount;
    }

    /// <summary>
    /// Executa o ataque da arma de um personagem em direção a um alvo.
    /// </summary>
    /// <param name="attacker">Personagem que realiza o ataque.</param>
    /// <param name="target">Personagem alvo.</param>
    public abstract void Attack(Character attacker, Character target);
}
