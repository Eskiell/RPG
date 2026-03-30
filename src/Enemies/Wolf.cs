namespace RPG.Enemies;

public class Wolf : Animal
{
    public Wolf(string name, float health) : base(name, health)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} uiva.");
    }

    public void HowlAtTheMoon()
    {
        Console.WriteLine($"{Name} uiva para a lua.");
    }
}