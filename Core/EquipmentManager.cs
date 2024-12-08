using RPG.Items;

namespace RPG.Core;

public class EquipmentManager
{
    private const int MaxRings = 4;
    private readonly List<Ring> _equippedRings = new();

    private readonly Character _owner;

    public EquipmentManager(Character owner)
    {
        _owner = owner;
    }

    public Weapon? EquippedWeapon { get; private set; }
    public Armor? EquippedArmor { get; private set; }
    public IReadOnlyList<Ring> EquippedRings => _equippedRings;

    // Gerenciamento de armas
    public void EquipWeapon(Weapon weapon)
    {
        if (EquippedWeapon != null)
            Console.WriteLine($"{_owner.Name} unequipped the weapon: {EquippedWeapon.Name}.");

        EquippedWeapon = weapon;
        Console.WriteLine($"{_owner.Name} equipped the weapon: {weapon.Name}.");
    }

    public void UnequipWeapon()
    {
        if (EquippedWeapon == null)
        {
            Console.WriteLine($"{_owner.Name} has no weapon to unequip.");
            return;
        }

        Console.WriteLine($"{_owner.Name} unequipped the weapon: {EquippedWeapon.Name}.");
        EquippedWeapon = null;
    }

    // Gerenciamento de armaduras
    public void EquipArmor(Armor armor)
    {
        if (EquippedArmor != null)
            Console.WriteLine($"{_owner.Name} unequipped the armor: {EquippedArmor.Name}.");

        EquippedArmor = armor;
        Console.WriteLine($"{_owner.Name} equipped the armor: {armor.Name}.");
    }

    public void UnequipArmor()
    {
        if (EquippedArmor == null)
        {
            Console.WriteLine($"{_owner.Name} has no armor to unequip.");
            return;
        }

        Console.WriteLine($"{_owner.Name} unequipped the armor: {EquippedArmor.Name}.");
        EquippedArmor = null;
    }

    // Gerenciamento de anéis
    public void EquipRing(Ring ring)
    {
        if (_equippedRings.Count >= MaxRings)
            throw new InvalidOperationException("Cannot equip more than 4 rings.");

        _equippedRings.Add(ring);
        Console.WriteLine($"{_owner.Name} equipped the ring: {ring.Name}.");
    }

    public void UnequipRing(Ring ring)
    {
        if (_equippedRings.Remove(ring))
            Console.WriteLine($"{_owner.Name} unequipped the ring: {ring.Name}.");
        else
            Console.WriteLine($"{_owner.Name} does not have the ring: {ring.Name} equipped.");
    }
}