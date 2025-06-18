using TextQuest.Contracts;

namespace TextQuest.Logic;

/// <summary>
/// Slouží k reprezentaci nepřítele ve hře.
/// </summary>
public class Enemy : ICombatant
{
    /// <summary>
    /// Typ nepřítele, např. "Goblin", "Skeleton", "Wolf", atd. - vymyšlí ho AI.
    /// </summary>
    public string? Type { get; private set; }

    public string? Name => Type;

    /// <summary>1
    /// Počáteční zdraví nepřítele.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Základní počet útoku nepřítele.
    /// </summary>
    public int AttackBase { get; set; }

    /// <summary>
    /// "Továrna" pro vytvoření  nepřítele s daným typem, zdravím a základním útokem.
    /// </summary>
    /// <returns></returns>
    public static Enemy Create(string enemyType, int health, int attackBase)
    {
        return new Enemy() { Type = enemyType, Health = health, AttackBase = attackBase };
    }

}