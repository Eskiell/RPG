using RPG.Items;

namespace RPG.Characters;

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

    /// <summary>
    /// Equipa uma arma no personagem.
    /// </summary>
    /// <param name="weapon">Arma a ser equipada.</param>
    /// <exception cref="ArgumentNullException">Lançada quando weapon é null.</exception>
    public void EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            throw new ArgumentNullException(nameof(weapon), "Arma não pode ser nula.");

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

    /// <summary>
    /// Equipa uma armadura no personagem.
    /// </summary>
    /// <param name="armor">Armadura a ser equipada.</param>
    /// <exception cref="ArgumentNullException">Lançada quando armor é null.</exception>
    public void EquipArmor(Armor armor)
    {
        if (armor == null)
            throw new ArgumentNullException(nameof(armor), "Armadura não pode ser nula.");

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

    /// <summary>
    /// Equipa um anel no personagem (máximo de 4 anéis).
    /// </summary>
    /// <param name="ring">Anel a ser equipado.</param>
    /// <exception cref="ArgumentNullException">Lançada quando ring é null.</exception>
    /// <exception cref="InvalidOperationException">Lançada quando já há 4 anéis equipados.</exception>
    public void EquipRing(Ring ring)
    {
        if (ring == null)
            throw new ArgumentNullException(nameof(ring), "Anel não pode ser nulo.");
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