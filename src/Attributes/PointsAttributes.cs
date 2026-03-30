namespace RPG.Attributes;

/// <summary>
/// Armazena os pontos brutos dos oito atributos primários de um personagem.
/// </summary>
public class PointsAttributes
{
    /// <summary>Pontos de Vitalidade — aumenta o HP máximo.</summary>
    public int Vitality { get; set; } = 10;

    /// <summary>Pontos de Vigor — relacionado à resistência e stamina.</summary>
    public int Vigor { get; set; } = 10;

    /// <summary>Pontos de Força — escala dano físico pesado.</summary>
    public int Strength { get; set; } = 10;

    /// <summary>Pontos de Destreza — escala dano de armas ágeis.</summary>
    public int Dexterity { get; set; } = 10;

    /// <summary>Pontos de Inteligência — escala magia arcana e MP.</summary>
    public int Intelligence { get; set; } = 10;

    /// <summary>Pontos de Fé — escala magia divina e MP.</summary>
    public int Faith { get; set; } = 10;

    /// <summary>Pontos de Resistência — aumenta a stamina máxima.</summary>
    public int Endurance { get; set; } = 10;

    /// <summary>Pontos de Arcano — escala magia sombria.</summary>
    public int Arcane { get; set; } = 10;

    /// <summary>Adiciona pontos ao atributo Vitalidade.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddVitalityPoints(int points) { Vitality += points; }

    /// <summary>Adiciona pontos ao atributo Vigor.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddVigorPoints(int points) { Vigor += points; }

    /// <summary>Adiciona pontos ao atributo Força.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddStrengthPoints(int points) { Strength += points; }

    /// <summary>Adiciona pontos ao atributo Destreza.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddDexterityPoints(int points) { Dexterity += points; }

    /// <summary>Adiciona pontos ao atributo Inteligência.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddIntelligencePoints(int points) { Intelligence += points; }

    /// <summary>Adiciona pontos ao atributo Resistência.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddEndurancePoints(int points) { Endurance += points; }

    /// <summary>Adiciona pontos ao atributo Fé.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddFaithPoints(int points) { Faith += points; }

    /// <summary>Adiciona pontos ao atributo Arcano.</summary>
    /// <param name="points">Quantidade de pontos a adicionar.</param>
    public void AddArcanePoints(int points) { Arcane += points; }
}
