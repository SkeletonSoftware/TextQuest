namespace TextQuestDraft.Logic;

/// <summary>
/// Slouží k reprezentaci nepřítele ve hře.
/// </summary>
public class Enemy
{
    /// <summary>
    /// Typ nepřítele, např. "Goblin", "Skeleton", "Wolf", atd. - vymyšlí ho AI.
    /// </summary>
    public string? Type { get; private set; }

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
        // TODO: Vytvořit továrnu pro předměty
        return null;
    }

}