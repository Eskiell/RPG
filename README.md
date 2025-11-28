# RPG - Sistema de Batalha por Turnos

Sistema de RPG em C# com foco em combate, atributos, equipamentos e efeitos de status. O projeto implementa um sistema completo de batalha com personagens, NPCs, cálculos de dano, experiência e drops.

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Tecnologias](#tecnologias)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Sistema de Atributos](#sistema-de-atributos)
- [Sistema de Combate](#sistema-de-combate)
- [Sistema de Equipamentos](#sistema-de-equipamentos)
- [Sistema de Efeitos](#sistema-de-efeitos)
- [NPCs e Comportamentos](#npcs-e-comportamentos)
- [Calculadoras](#calculadoras)
- [Exemplos de Uso](#exemplos-de-uso)
- [Como Executar](#como-executar)

## 🎮 Visão Geral

Este projeto é um sistema de RPG que implementa:

- **Personagens**: Jogadores e NPCs com atributos, níveis e experiência
- **Combate**: Sistema de batalha com cálculo de dano, defesa e variação
- **Equipamentos**: Armas, armaduras e anéis com escalonamento de atributos
- **Efeitos de Status**: Buffs, debuffs, veneno, sangramento e hemorragia
- **NPCs**: Comerciantes e NPCs com comportamentos personalizáveis
- **Drops**: Sistema de drops com probabilidades configuráveis

## 🛠 Tecnologias

- **.NET 10.0**: Framework principal
- **C# 13.0**: Linguagem de programação
- **NCalc 1.3.8**: Biblioteca para cálculos de expressões
- **Newtonsoft.Json 13.0.3**: Serialização e deserialização JSON

## 📁 Estrutura do Projeto

```
RPG/
├── src/                    # Código-fonte organizado
│   ├── Core/               # Classes principais
│   │   ├── Character.cs    # Classe base para personagens
│   │   ├── Player.cs       # Classe do jogador
│   │   ├── Npc.cs          # Classe base de NPCs
│   │   └── EquipmentManager.cs # Gerenciador de equipamentos
│   ├── Entities/           # Entidades do jogo
│   │   ├── Animals/        # Classes de animais
│   │   │   ├── Animal.cs
│   │   │   └── Wolf.cs
│   │   └── Items/          # Itens e equipamentos
│   │       ├── Item.cs
│   │       ├── Equipment.cs
│   │       ├── Weapon.cs
│   │       ├── Sword.cs
│   │       ├── Armor.cs
│   │       └── Ring.cs
│   ├── Systems/            # Sistemas do jogo
│   │   ├── Attributes/     # Sistema de atributos
│   │   │   ├── PointsAttributes.cs
│   │   │   ├── ScalingAttribute.cs
│   │   │   ├── AttributeScaler.cs
│   │   │   └── AttributeBonus.cs
│   │   ├── Combat/         # Sistema de combate
│   │   │   ├── Damage.cs
│   │   │   └── Combo.cs
│   │   └── Drops/          # Sistema de drops
│   │       └── ItemDrop.cs
│   ├── Effects/            # Efeitos de status
│   │   ├── StatusEffect.cs
│   │   ├── AttributeBuff.cs
│   │   ├── PoisonEffect.cs
│   │   ├── BleedingEffect.cs
│   │   ├── HemorrhageEffect.cs
│   │   ├── WeaponPoisonEffect.cs
│   │   └── IWeaponEffect.cs
│   ├── Calculators/        # Calculadoras
│   │   ├── CombatCalculator.cs
│   │   ├── DamageCalculator.cs
│   │   ├── StatCalculator.cs
│   │   ├── ScalingCalculator.cs
│   │   ├── ExperienceCalculator.cs
│   │   └── DropCalculator.cs
│   ├── Behaviors/          # Comportamentos
│   │   └── SellingBehavior.cs
│   ├── NPCs/               # NPCs específicos
│   │   └── Merchant.cs
│   ├── Enums/              # Enumerações
│   │   ├── CharacterAttribute.cs
│   │   ├── DamageType.cs
│   │   ├── NpcType.cs
│   │   ├── Rarity.cs
│   │   └── YesNoOption.cs
│   ├── Interfaces/         # Interfaces
│   │   └── IBehavior.cs
│   └── Utils/              # Utilitários
│       ├── Logger.cs
│       ├── Localization.cs
│       └── LoadItems.cs
├── Data/                   # Dados do jogo
│   ├── Database/           # Arquivos JSON de dados
│   │   ├── items.json
│   │   ├── personagens.json
│   │   ├── players.json
│   │   └── swords.json
│   └── localization.json
└── Program.cs              # Ponto de entrada da aplicação
```

## ⚙️ Sistema de Atributos

O sistema utiliza 8 atributos principais:

- **Vitality** (Vitalidade): Aumenta vida máxima
- **Vigor** (Vigor): Aumenta resistência
- **Strength** (Força): Aumenta dano físico e defesa física
- **Dexterity** (Destreza): Aumenta dano físico e velocidade de ataque
- **Intelligence** (Inteligência): Aumenta mana e defesa mágica
- **Faith** (Fé): Aumenta mana e defesa mágica
- **Endurance** (Resistência): Aumenta stamina máxima
- **Arcane** (Arcano): Aumenta defesa mágica

### PointsAttributes

Classe que armazena os pontos de atributos do personagem:

```csharp
var attributes = new PointsAttributes
{
    Vitality = 10,
    Vigor = 10,
    Strength = 10,
    Dexterity = 10,
    Intelligence = 10,
    Faith = 10,
    Endurance = 10,
    Arcane = 10
};
```

### Escalonamento de Atributos

Os atributos utilizam um sistema de escalonamento com três tipos de crescimento:

- **GrowthLinear**: Crescimento linear
- **GrowthSoft**: Crescimento suave (diminui com o tempo)
- **GrowthHard**: Crescimento duro (aumenta com o tempo)

## ⚔️ Sistema de Combate

### Cálculo de Dano

O dano é calculado usando a fórmula:

```
dano = (danoBase * (1 + modificadorDano) * (1 + modificadorAlvo) - defesaAlvo) * variação
```

Onde:
- **danoBase**: Dano base do atacante
- **modificadorDano**: Modificador de dano físico
- **modificadorAlvo**: Modificador do alvo
- **defesaAlvo**: Defesa do alvo
- **variação**: Variação aleatória de ±10%

### Defesa

A defesa é calculada separadamente para dano físico e mágico:

**Defesa Física:**
```
defesa = defesaBase + (bonusVitality * bonusEndurance * bonusStrength)
```

**Defesa Mágica:**
```
defesa = defesaBase + (bonusIntelligence * bonusFaith * bonusArcane)
```

### Ataque

Personagens podem atacar com ou sem arma:

- **Com arma**: Usa o dano e efeitos da arma equipada
- **Sem arma**: Usa dano base calculado pelos atributos

## 🛡️ Sistema de Equipamentos

### Armas

As armas possuem:
- **Dano base**: Dano fixo da arma
- **Velocidade de ataque**: Multiplicador de velocidade
- **Durabilidade**: Vida útil da arma
- **Escalonamento**: Atributos que aumentam o dano da arma
- **Efeitos**: Efeitos especiais aplicados ao atacar

Exemplo de criação de espada:

```csharp
var sword = new Sword(
    "Sword Waterfowl Dance",
    25,                    // Dano base
    1.0f,                  // Velocidade de ataque
    10,                    // Durabilidade
    new[]
    {
        new ScalingAttribute(CharacterAttribute.Strength, 0.10f),
        new ScalingAttribute(CharacterAttribute.Dexterity, 0.10f)
    }
);
```

### Anéis

Anéis podem ser equipados (máximo de 4) e fornecem bônus de atributos:

```csharp
var ring = new Ring(
    "Ring of Strength",
    "Increases strength by 10%",
    new[]
    {
        new ScalingAttribute(CharacterAttribute.Strength, 0.10f)
    }
);
```

### Armaduras

Armaduras fornecem defesa adicional e possuem durabilidade:

```csharp
var armor = new Armor(
    "Steel Armor",
    100,        // Durabilidade
    50f         // Valor de defesa
);
```

As armaduras podem ser reparadas e têm sua durabilidade reduzida com o uso.

## ✨ Sistema de Efeitos

### StatusEffect

Classe base abstrata para todos os efeitos de status. Todos os efeitos possuem:
- **Nome**: Nome do efeito
- **Duração**: Número de turnos que o efeito dura
- **Valor do Efeito**: Valor numérico do efeito

### Tipos de Efeitos

#### AttributeBuff

Aumenta temporariamente um atributo:

```csharp
var strengthBuff = new AttributeBuff(
    "Strength Buff",
    3,                              // Duração em turnos
    CharacterAttribute.Strength,
    10f                            // Bônus
);
```

#### PoisonEffect

Causa dano por turno:

```csharp
var poison = new PoisonEffect(30f, 5); // 30 de dano por 5 turnos
```

#### BleedingEffect

Causa dano contínuo ao longo do tempo:

```csharp
var bleeding = new BleedingEffect(5, 200); // 200 de dano total em 5 segundos
```

#### HemorrhageEffect

Versão mais forte do sangramento com barra de sangue. Quando a barra de sangue atinge 100%, causa dano massivo:

```csharp
var hemorrhage = new HemorrhageEffect(5, 200); // 200 de dano quando a barra enche
hemorrhage.ApplyEffect(character);
hemorrhage.IncreaseBloodBar(10f); // Aumenta a barra de sangue
```

#### WeaponPoisonEffect

Efeito de veneno aplicado por armas:

```csharp
var poisonSword = new Sword("Poison Blade", 30, 1.2f, 100, scaling);
poisonSword.Effects.Add(new WeaponPoisonEffect(30, 5));
```

### Aplicação de Efeitos

Efeitos podem ser aplicados de duas formas:

```csharp
// Forma 1: Adicionar diretamente à lista
character.ActiveEffects.Add(effect);

// Forma 2: Usar o método ApplyEffect
effect.ApplyEffect(character);
```

### Atualização de Efeitos

Efeitos são atualizados a cada turno:

```csharp
character.UpdateEffects(); // Reduz duração e remove efeitos expirados
```

## 🤖 NPCs e Comportamentos

### NPC

Classe base para NPCs com:
- **IsHostile**: Define se o NPC é hostil
- **Dialogue**: Diálogo do NPC
- **Behavior**: Comportamento personalizado (interface `IBehavior`)

### Merchant

NPC comerciante que vende itens:

```csharp
var merchant = new Merchant(
    "Brom, the Merchant",
    500f,
    attributes,
    itemsForSale
);
```

### Comportamentos

NPCs podem ter comportamentos personalizados implementando `IBehavior`:

```csharp
public interface IBehavior
{
    void Execute(Npc npc);
}

public class SellingBehavior : IBehavior
{
    public void Execute(Npc npc)
    {
        // Lógica de venda
    }
}
```

O comportamento é executado através do método `PerformBehavior()` do NPC.

## 🧮 Calculadoras

### CombatCalculator

Calcula valores de combate modificados:
- `CalculateModifiedAttack()`: Ataque modificado
- `CalculateModifiedDefense()`: Defesa modificada
- `CalculatePhysicalAttack()`: Ataque físico
- `ApplyEffects()`: Aplica efeitos aos valores

### DamageCalculator

Calcula o dano total considerando:
- Dano base
- Modificadores
- Defesa do alvo
- Variação aleatória

### StatCalculator

Calcula bônus de atributos usando escalonamento:
- `ComputeBonus()`: Calcula bônus baseado no atributo e pontos

### ExperienceCalculator

Calcula experiência e níveis:
- `ExpForLevel()`: Experiência necessária para um nível
- `LevelForExp()`: Nível baseado na experiência

### ScalingCalculator

Calcula o escalonamento de armas baseado nos atributos do personagem.

### DropCalculator

Gerencia drops de itens com probabilidades:
- `AddItemDrop()`: Adiciona item à tabela de drops
- `GetRandomItemDrop()`: Retorna itens dropados aleatoriamente

## 📝 Exemplos de Uso

### Criando um Jogador

```csharp
var player = new Player(
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
    }
);
```

### Equipando Itens

```csharp
// Equipar arma
player.Equipment.EquipWeapon(sword);

// Equipar anel
player.Equipment.EquipRing(ringOfStrength);

// Equipar armadura
player.Equipment.EquipArmor(armor);
```

### Aplicando Efeitos

```csharp
// Aplicar buff
var buff = new AttributeBuff("Strength Buff", 3, CharacterAttribute.Strength, 10f);
buff.ApplyEffect(player);

// Atualizar efeitos (chamar a cada turno)
player.UpdateEffects();
```

### Combate

```csharp
// Ataque básico
player.Attack(enemy);

// Verificar se está vivo
if (player.IsAlive())
{
    // Lógica
}
```

### Ganhar Experiência

```csharp
player.GainExperience(100); // Ganha experiência e sobe de nível automaticamente
```

### Drops

```csharp
var droppedItems = player.Drops();
foreach (var item in droppedItems)
{
    Console.WriteLine($"Item dropado: {item.Name}");
}
```

### Interação com NPCs

```csharp
// Conversar com NPC
if (!npc.IsHostile)
{
    npc.Talk(player);
}

// Provocar NPC
npc.Provoke();
npc.Attack(player);

// Executar comportamento
npc.PerformBehavior();
```

## 🚀 Como Executar

### Pré-requisitos

- .NET 10.0 SDK ou superior
- Visual Studio, Rider ou VS Code com extensão C#

### Executando o Projeto

1. Clone o repositório:
```bash
git clone <url-do-repositorio>
cd RPG
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Compile o projeto:
```bash
dotnet build
```

4. Execute o projeto:
```bash
dotnet run
```

### Estrutura de Dados

O projeto utiliza arquivos JSON na pasta `Data/Database/` para carregar dados:
- `items.json`: Itens do jogo
- `personagens.json`: Dados de personagens
- `players.json`: Dados de jogadores
- `swords.json`: Dados de espadas

O arquivo `localization.json` está localizado em `Data/` e contém as traduções do jogo.

## 📚 Classes Principais

### Character

Classe base abstrata para todos os personagens (Player e NPC). Contém:
- Atributos básicos (nome, vida, nível)
- Sistema de equipamentos
- Sistema de efeitos
- Métodos de combate

### Player

Herda de `Character` e adiciona:
- Sistema de experiência
- Níveis
- Método `Drops()` para gerar drops

### Npc

Herda de `Character` e adiciona:
- Sistema de hostilidade
- Diálogos
- Comportamentos personalizados

### EquipmentManager

Gerencia equipamentos do personagem:
- Uma arma
- Uma armadura
- Até 4 anéis

## 🔧 Extensibilidade

O projeto foi projetado para ser extensível:

- **Novos Efeitos**: Herde de `StatusEffect` ou `IWeaponEffect`
- **Novos NPCs**: Herde de `Npc` e implemente comportamentos
- **Novas Armas**: Herde de `Weapon` e implemente `Attack()`
- **Novos Comportamentos**: Implemente `IBehavior`

## 📄 Licença

Ver arquivo `LICENSE` para mais informações.

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

---

**Desenvolvido com C# 13.0 e .NET 10.0**
