using RPG.Attributes;
using RPG.Combat;
using RPG.Progression;
using RPG.Enums;
using RPG.Items;

namespace RPG.Characters;

/// <summary>
/// Representa o personagem controlado pelo jogador, com sistema de experiência, nivelamento e drops.
/// </summary>
public class Player : Character
{
    /// <summary>
    /// Inicializa um novo jogador.
    /// </summary>
    /// <param name="name">Nome do jogador.</param>
    /// <param name="health">Vida base.</param>
    /// <param name="pointsAttributes">Atributos primários.</param>
    /// <param name="level">Nível inicial (padrão: 1).</param>
    /// <param name="experience">Experiência inicial (padrão: 0).</param>
    public Player(
        string name,
        float health,
        PointsAttributes pointsAttributes,
        int level = 1,
        double experience = 0)
        : base(name, health, pointsAttributes)
    {
        Level = level;
        Experience = experience;
    }

    /// <summary>Nível atual do jogador (sobrescreve o nível base de Character).</summary>
    public new int Level { get; private set; }

    /// <summary>Experiência acumulada pelo jogador.</summary>
    public double Experience { get; private set; }

    /// <summary>
    /// Ataca um inimigo usando a arma equipada ou um soco caso não haja arma.
    /// </summary>
    /// <param name="enemy">Personagem alvo.</param>
    public override void Attack(Character enemy)
    {
        if (Equipment.EquippedWeapon != null)
        {
            Equipment.EquippedWeapon.Attack(this, enemy);
            return;
        }

        var damage = new DamageCalculator(GetModifiedBaseAttack(), GetModifiedPAttack(), enemy.Defense());
        var power = damage.CalculateTotalDamage();
        enemy.TakeDamage(power);
        Console.WriteLine($"{Name} attacked {enemy.Name} with a punch for {power} damage.");
    }


    /// <summary>
    /// Adiciona experiência ao jogador e verifica se deve subir de nível.
    /// </summary>
    /// <param name="amount">Quantidade de experiência a ser adicionada.</param>
    /// <exception cref="ArgumentException">Lançada quando amount é negativo.</exception>
    public void GainExperience(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Experiência não pode ser negativa.", nameof(amount));
        
        Experience += amount;
        while (Experience >= ExperienceCalculator.ExpForLevel(Level + 1)) LevelUp();
    }

    private void LevelUp()
    {
        Level++;
        Console.WriteLine($" {Name} Leveled up! New level is {Level}.");
    }

    /// <summary>
    /// Simula os drops do personagem ao ser derrotado, usando o sistema de tabela de drops.
    /// </summary>
    /// <returns>Lista de itens dropados.</returns>
    public List<Item> Drops()
    {
        // Criando os itens
        var sword = new Item("Sword", 100, Rarity.Common, "A basic sword", 10.0, YesNoOption.No);
        var potion = new Item("Health Potion", 50, Rarity.Common, "Restores health", 5.0, YesNoOption.Yes);
        var armor = new Item("Armor", 200, Rarity.Rare, "Protective armor", 50.0, YesNoOption.No);

        // Criando o DropCalculator
        var dropCalculator = new DropCalculator();

        // Adicionando os itens à tabela de drops com suas respectivas taxas de drop
        dropCalculator.AddItemDrop(sword, 0.2f); // 20% de chance de drop
        dropCalculator.AddItemDrop(potion, 1.0f); // 100% de chance de drop
        dropCalculator.AddItemDrop(armor, 0.01f); // 1% de chance de drop

        // Simulando o drop de um item

        var droppedItem = dropCalculator.GetRandomItemDrop();
        if (droppedItem.Count > 0)
            foreach (var item in droppedItem)
                Console.WriteLine("Item dropped: " + item.Name);
        else
            Console.WriteLine("No item dropped");

        return droppedItem;
    }
}