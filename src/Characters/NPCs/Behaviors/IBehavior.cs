using RPG.Characters;

namespace RPG.Characters.NPCs.Behaviors;

public interface IBehavior
{
    void Execute(Npc npc);
}