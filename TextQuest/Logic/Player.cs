using TextQuest.Common;
using TextQuest.Contracts;
using TextQuest.Models;

namespace TextQuest.Logic;

/// <summary>
/// Slouží k reprezentaci hráče v textové hře.
/// </summary>
/// <param name="terminal"></param>
public class Player(TextQuestTerminal terminal) : ICombatant
{
    /// <summary>
    /// Typ hráče
    /// </summary>
    public CharacterType? Type { get; private set; }

    public string? Name => Type.ToString();

    /// <summary>
    /// Počet životů hráče
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Základní počet útoku hráče
    /// </summary>
    public int AttackBase { get; set; }

    /// <summary>
    /// Inventář hráče, který obsahuje předměty
    /// </summary>
    public List<Item?> Inventory { get; private set; } = [];

    /// <summary>
    /// Nastaví typ postavy hráče.
    /// </summary>
    /// <param name="type"></param>
    public void SetCharacterType(CharacterType type)
    {
        if (Type is not null)
        {
            terminal.WriteError("Character type has already been set to " + Type);
            return;
        }

        Type = type;
        AttackBase = type switch
        {
            CharacterType.Knight => 30,
            CharacterType.Programmer => 5,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown character type")
        };

        Health = type switch
        {
            CharacterType.Knight => 1000,
            CharacterType.Programmer => 100,
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unknown character type")
        };
    }
}