
namespace PatternsLab.Prototype;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
}

public class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }
    public Enemy Clone()
    {
        return new Enemy {Name = Name, 
            Health = Health,
            _modelData = _modelData,
            Weapon = new Weapon {Name = Weapon.Name, Damage = Weapon.Damage} };
    }


}



public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e) => e.Clone();
}
