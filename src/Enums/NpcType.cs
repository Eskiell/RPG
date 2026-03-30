namespace RPG.Enums;

/// <summary>Categorias funcionais de NPCs no mundo do jogo.</summary>
public enum NpcType
{
    /// <summary>NPC que vende itens ao jogador.</summary>
    Merchant,
    /// <summary>NPC que cura o jogador.</summary>
    Healer,
    /// <summary>NPC que ensina habilidades.</summary>
    SkillMaster,
    /// <summary>NPC que oferece missões.</summary>
    QuestGiver,
    /// <summary>NPC aliado ao jogador.</summary>
    Ally,
    /// <summary>NPC antagonista.</summary>
    Antagonist,
    /// <summary>NPC neutro.</summary>
    Neutral,
    /// <summary>NPC de guarda.</summary>
    Guard,
    /// <summary>NPC do tipo animal.</summary>
    Animal,
    /// <summary>NPC que transporta o jogador.</summary>
    Transporter,
    /// <summary>NPC artesão que cria itens.</summary>
    Craftsman,
    /// <summary>NPC coletor de recursos.</summary>
    Gatherer,
    /// <summary>NPC de entretenimento.</summary>
    Entertainer,
    /// <summary>NPC criança.</summary>
    Child,
    /// <summary>NPC líder de uma facção ou grupo.</summary>
    Leader
}
