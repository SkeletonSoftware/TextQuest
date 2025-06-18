using TextQuest.Contracts;
using TextQuest.Models;

namespace TextQuest.Logic;

/// <summary>
/// SLouží k reprezentaci předmětu ve hře.
/// </summary>
public class Item : IConsumable
{
    /// <summary>
    /// Typ předmětu.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// Síla, nebo účinek předmětu.
    /// </summary>
    public int Power { get; set; }

    /// <summary>
    /// "Továrna" pro vytvoření předmětu s daným typem a body.
    /// </summary>
    /// <returns></returns>
    public static Item Create(ItemType type, int power)
    {
        return new Item() { Type = type, Power = power };
    }

    /// <summary>
    /// Použití předmětu na daného bojovníka (hráče nebo nepřítele).
    /// </summary>
    /// <param name="combatant"></param>
    /// <returns></returns>
    public string Use(ICombatant combatant)
    {
        switch (Type)
        {
            case ItemType.Potion:
                combatant.Health += Power;
                return $"You drank a potion and restored {Power} health.";

            case ItemType.Weapon:
                combatant.AttackBase += Power;
                return $"You equipped a weapon and gained {Power} attack.";

            default:
                return "Nothing happened.";
        }
    }

    public override string ToString() => $"{Type} (+{Power})";
}