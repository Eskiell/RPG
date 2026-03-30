using RPG.Attributes;
using RPG.Enums;
using RPG.Characters.NPCs.Behaviors;

namespace RPG.Characters;

/// <summary>
/// Representa um NPC (Non-Player Character) do jogo, com comportamento, diálogo e tipo configuráveis.
/// </summary>
public class Npc : Character
{
    /// <summary>
    /// Inicializa um novo NPC com nome, vida e atributos.
    /// </summary>
    /// <param name="name">Nome do NPC.</param>
    /// <param name="health">Vida base.</param>
    /// <param name="pointsAttributes">Atributos primários.</param>
    public Npc(string name, float health, PointsAttributes pointsAttributes) : base(name, health, pointsAttributes)
    {
    }

    /// <summary>Comportamento executado pelo NPC (ex.: vender, patrulhar).</summary>
    public IBehavior Behavior { get; set; }

    /// <summary>Tipo do NPC (ex.: Merchant, Guard).</summary>
    public NpcType Type { get; set; }

    /// <summary>Define se o NPC é hostil ao jogador.</summary>
    public bool IsHostile { get; set; }

    /// <summary>Diálogo exibido ao interagir com o NPC.</summary>
    public string Dialogue { get; set; }

    /// <summary>Executa o comportamento atual do NPC.</summary>
    public void PerformBehavior()
    {
        Behavior.Execute(this);
    }

    /// <summary>Torna o NPC hostil.</summary>
    public void Provoke()
    {
        IsHostile = true;
    }

    /// <summary>Remove a hostilidade do NPC.</summary>
    public void CalmDown()
    {
        IsHostile = false;
    }

    /// <summary>
    /// Ataca um alvo caso o NPC esteja em estado hostil.
    /// </summary>
    /// <param name="target">Personagem alvo do ataque.</param>
    public override void Attack(Character target)
    {
        if (IsHostile)
        {
            Console.WriteLine($"{Name} está atacando {target.Name}!");
            target.TakeDamage(GetModifiedPAttack());
        }
        else
        {
            Console.WriteLine($"{Name} não é hostil e não atacará a menos que seja provocado.");
        }
    }

    /// <summary>
    /// Interage verbalmente com outro personagem.
    /// </summary>
    /// <param name="interactor">Personagem que inicia a conversa.</param>
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

    /// <summary>
    /// Realiza uma troca com o jogador (não implementado).
    /// </summary>
    /// <param name="player">Jogador envolvido na troca.</param>
    public void Trade(Player player)
    {
    }
}
