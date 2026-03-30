using RPG.Characters;

namespace RPG.Characters.NPCs.Behaviors;

/// <summary>
/// Contrato para comportamentos executáveis por NPCs.
/// </summary>
public interface IBehavior
{
    /// <summary>
    /// Executa o comportamento para o NPC informado.
    /// </summary>
    /// <param name="npc">NPC que executa o comportamento.</param>
    void Execute(Npc npc);
}
