using RPG.Attributes;
using RPG.Enums;
using RPG.Interface;

namespace RPG.Core;

public class Npc : Character
{
    public Npc(string name, float health, PointsAttributes pointsAttributes) : base(name, health, pointsAttributes)
    {
    }

    public IBehavior Behavior { get; set; }
    public NpcType Type { get; set; }
    public bool IsHostile { get; set; } // Define se o NPC é hostil ou não.
    public string Dialogue { get; set; } // Uma mensagem ou diálogo que o NPC pode dizer ao jogador.

    public void PerformBehavior()
    {
        Behavior.Execute(this);
    }

    public void Provoke()
    {
        IsHostile = true;
    }

    public override void Attack(Character target)
    {
        if (IsHostile)
        {
            Console.WriteLine($"{Name} está atacando {target.Name}!");
            // Adicione aqui a lógica de dano, defesa, etc.
            target.TakeDamage(GetModifiedPAttack());
        }
        else
        {
            Console.WriteLine($"{Name} não é hostil e não atacará a menos que seja provocado.");
        }
    }

    public void Talk(Character interactor)
    {
        if (IsHostile)
        {
            Console.WriteLine($"{Name}: Fora daqui antes que eu te ataque!");
            Console.WriteLine($"{interactor.Name}: Calma aí, eu só queria conversar.");
        }
        else
        {
            Console.WriteLine(Dialogue);
        }
    }

    public void Trade(Player player)
    {
    }
}