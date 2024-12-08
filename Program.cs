using RPG.Attributes;
using RPG.Core;
using RPG.Effects;
using RPG.Enums;
using RPG.Items;
using RPG.Npcs;
using RPG.Utils;

namespace RPG;

internal class Program
{
    private static void Main(string[] args)
    {
        Logger.Log("Application started.");
        var strengthBuff = new AttributeBuff("Strength Buff", 3, CharacterAttribute.Strength, 10f);
        var dexterityBuff = new AttributeBuff("Dexterity Buff", 3, CharacterAttribute.Dexterity, 15f);
        var malenia = new Player(
            "Malenia, Blade of Miquella",
            2000f,
            new PointsAttributes
            {
                Vitality = 10,
                Vigor = 10,
                Strength = 10,
                Dexterity = 10,
                Intelligence = 10,
                Faith = 10,
                Endurance = 10,
                Arcane = 10
            });
        malenia.Equipment.EquipWeapon(new Sword(
            "Sword Waterfowl Dance",
            25,
            1.0f,
            10,
            new[]
            {
                new ScalingAttribute(CharacterAttribute.Strength, 0.10f),
                new ScalingAttribute(CharacterAttribute.Dexterity, 0.10f)
            }
        ));
        // Aplicando o buff
        // 1 forma de aplicar o buff
        malenia.ActiveEffects.Add(strengthBuff);
        // 2 forma de aplicar o buff
        dexterityBuff.ApplyEffect(malenia);

        // Simulando turnos
        for (var turn = 1; turn <= 5; turn++)
        {
            Console.WriteLine($"Turn {turn}");
            malenia.UpdateEffects();
        }

        var radahn = new Player(
            "Starscourge Radahn",
            2000f,
            new PointsAttributes
            {
                Vitality = 10,
                Vigor = 10,
                Strength = 10,
                Dexterity = 10,
                Intelligence = 10,
                Faith = 10,
                Endurance = 10,
                Arcane = 10
            });
        radahn.Equipment.EquipWeapon(new Sword(
            "Sword Star Dance",
            25,
            1.0f,
            10,
            new[]
            {
                new ScalingAttribute(CharacterAttribute.Strength, 0.10f),
                new ScalingAttribute(CharacterAttribute.Intelligence, 0.10f)
            }
        ));


        var ringOfStrength = new Ring(
            "Ring of Strength",
            "Increases strength by 10%",
            new[]
            {
                new ScalingAttribute(CharacterAttribute.Strength, 0.10f)
            }
        );

        var ringOfDexterity = new Ring(
            "Ring of Dexterity",
            "Increases dexterity by 15%",
            new[]
            {
                new ScalingAttribute(CharacterAttribute.Dexterity, 0.15f)
            }
        );
        // Equipando anéis
        malenia.Equipment.EquipRing(ringOfStrength);
        malenia.Equipment.EquipRing(ringOfDexterity);

        var bleedingEffect = new BleedingEffect(5, 200);
        bleedingEffect.ApplyEffect(radahn);
        while (bleedingEffect.IsActive()) ;


        // Criando um novo NPC
        var shopkeeper = new Npc(
            "Brom, the Merchant",
            500f,
            new PointsAttributes
            {
                Vitality = 5,
                Vigor = 5,
                Strength = 5,
                Dexterity = 5,
                Intelligence = 5,
                Faith = 5,
                Endurance = 5,
                Arcane = 5
            }
        );

// Supondo que NPCs também possam ser equipados com armas (o que pode ou não ser o caso em seu jogo)
        shopkeeper.Equipment.EquipWeapon(new Sword(
            "Merchant's Dagger",
            10,
            0.5f,
            5,
            new[]
            {
                new ScalingAttribute(CharacterAttribute.Strength, 0.10f),
                new ScalingAttribute(CharacterAttribute.Dexterity, 0.10f)
            }
        ));
        var poisonSword = new Sword("Poison Blade", 30, 1.2f, 100, new[]
        {
            new ScalingAttribute(CharacterAttribute.Strength, 0.10f),
            new ScalingAttribute(CharacterAttribute.Dexterity, 0.10f)
        });
        poisonSword.Effects.Add(new WeaponPoisonEffect(30, 5));
        shopkeeper.Equipment.EquipWeapon(poisonSword);
        malenia.Equipment.EquipWeapon(poisonSword);
        for (var turn = 1; turn <= 4; turn++)
        {
            Console.WriteLine($"Turn {turn}");
            malenia.UpdateEffects();
        }

// Definindo propriedades específicas do NPC
        shopkeeper.IsHostile = true;
        shopkeeper.Dialogue = "Olá, viajante! Você está interessado em comprar algo?";

// Interagindo com o NPC
        if (!shopkeeper.IsHostile)
        {
            Console.WriteLine(shopkeeper.Dialogue);
            shopkeeper.Talk(malenia); // Assumindo que "malenia" seja a instância do Player que você criou anteriormente
        }
        else
        {
            shopkeeper.Provoke();
            shopkeeper.Attack(malenia);
        }

        var hemorrhageEffect = new HemorrhageEffect(5, 200);
        hemorrhageEffect.ApplyEffect(radahn);
        while (hemorrhageEffect.IsActive()) hemorrhageEffect.IncreaseBloodBar(0.1f);

        while (malenia.IsAlive() && radahn.IsAlive())
        {
            malenia.Attack(radahn);
            radahn.Attack(malenia);
        }

        if (!malenia.IsAlive())
        {
            radahn.GainExperience(100);
            Console.WriteLine(malenia.Name + " foi derrotada!");
        }

        if (!radahn.IsAlive())
        {
            malenia.GainExperience(100);
            Console.WriteLine(radahn.Name + " foi derrotado!");
        }

        malenia.Drops();

        var itemsForSale = new List<Item>
        {
            new("Poção", 10, Rarity.Common, "Uma poção simples", 5.0, YesNoOption.Yes),
            new("Espada", 100, Rarity.Rare, "Uma espada afiada", 50.0, YesNoOption.No)
        };
        var loader = new LoadItems();
        var items = loader.Items;
        Console.WriteLine(loader.Items![0].Consumable); // Exibirá: "Poção" 
        var merchant = new Merchant(
            "Brom, the Merchant",
            500f,
            new PointsAttributes
            {
                Vitality = 5,
                Vigor = 5,
                Strength = 5,
                Dexterity = 5,
                Intelligence = 5,
                Faith = 5,
                Endurance = 5,
                Arcane = 5
            },
            items
        );
        merchant.Attack(malenia); // Exibirá: "Brom, the Merchant não é hostil e não atacará a menos que seja provocado."
        merchant.PerformBehavior(); // Exibirá: "Bob tem os seguintes itens à venda: Poção, Espada, Armadura"
        Logger.Log("O {attacker} atacou {target} e causou {damage} de dano!");
    }
}