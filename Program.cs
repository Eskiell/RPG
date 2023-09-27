using RPG.Core;

namespace RPG;

internal class Program
{
    private static void Main(string[] args)
    {
        var miquella = new Player(
            "Miquella",
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
        miquella.EquipWeapon(new Sword(
            "Sword Waterfowl Dance",
            25,
            1.0f,
            10,
            new[]
            {
                new ScalingAttribute { Attribute = "strength", Value = 0.10f },
                new ScalingAttribute { Attribute = "dexterity", Value = 0.10f }
            }
        ));
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
        radahn.EquipWeapon(new Sword(
            "Sword Star Dance",
            25,
            1.0f,
            10,
            new[]
            {
                new ScalingAttribute { Attribute = "strength", Value = 0.10f }
            }
        ));

        //var bleedingEffect = new BleedingEffect(5, 200);
        //bleedingEffect.ApplyEffect(radahn);
        //while (bleedingEffect.IsActive());


        var hemorrhageEffect = new HemorrhageEffect(5, 200);
        hemorrhageEffect.ApplyEffect(radahn);
        while (hemorrhageEffect.IsActive()) hemorrhageEffect.IncreaseBloodBar(0.1f);

        while (miquella.IsAlive() && radahn.IsAlive())
        {
            miquella.Attack(radahn);
            radahn.Attack(miquella);
        }

        if (!miquella.IsAlive())
        {
            radahn.GainExperience(100);
            Console.WriteLine(miquella.Name + " foi derrotada!");
        }

        if (!radahn.IsAlive())
        {
            miquella.GainExperience(100);
            Console.WriteLine(radahn.Name + " foi derrotado!");
        }

        miquella.Drops();
    }
}