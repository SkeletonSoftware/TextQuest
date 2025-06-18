using TextQuestDraft.Models;

namespace TextQuestDraft.Logic;

/// <summary>
/// SLouží k reprezentaci předmětu ve hře.
/// </summary>
public class Item
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
        // TODO: Vytvořit továrnu pro předměty
        return null;
    }
}