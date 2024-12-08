using RPG.Attributes;
using RPG.Calculators;
using RPG.Enums;
using RPG.Items;

namespace RPG.Core;

public class Player : Character
{
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

    public int Level { get; private set; }
    public double Experience { get; private set; }

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


    public void GainExperience(double amount)
    {
        Experience += amount;
        while (Experience >= ExperienceCalculator.ExpForLevel(Level + 1)) LevelUp();
    }

    private void LevelUp()
    {
        Level++;
        Console.WriteLine($" {Name} Leveled up! New level is {Level}.");
    }

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