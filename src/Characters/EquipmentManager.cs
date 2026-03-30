using RPG.Items;

namespace RPG.Characters;

/// <summary>
/// Gerencia o equipamento de um personagem: arma, armadura e anéis.
/// </summary>
public class EquipmentManager
{
    private const int MaxRings = 4;
    private readonly List<Ring> _equippedRings = new();
    private readonly Character _owner;

    /// <summary>
    /// Inicializa o gerenciador de equipamentos para o personagem informado.
    /// </summary>
    /// <param name="owner">Personagem dono dos equipamentos.</param>
    public EquipmentManager(Character owner)
    {
        _owner = owner;
    }

    /// <summary>Arma atualmente equipada, ou <c>null</c> se nenhuma.</summary>
    public Weapon? EquippedWeapon { get; private set; }

    /// <summary>Armadura atualmente equipada, ou <c>null</c> se nenhuma.</summary>
    public Armor? EquippedArmor { get; private set; }

    /// <summary>Lista somente leitura dos anéis equipados (máximo de 4).</summary>
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

    /// <summary>Remove a arma equipada do personagem.</summary>
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

    /// <summary>Remove a armadura equipada do personagem.</summary>
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

    /// <summary>
    /// Remove um anel equipado do personagem.
    /// </summary>
    /// <param name="ring">Anel a ser removido.</param>
    public void UnequipRing(Ring ring)
    {
        if (_equippedRings.Remove(ring))
            Console.WriteLine($"{_owner.Name} unequipped the ring: {ring.Name}.");
        else
            Console.WriteLine($"{_owner.Name} does not have the ring: {ring.Name} equipped.");
    }
}
