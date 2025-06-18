namespace TextQuest.Contracts;

/// <summary>
/// Rozhraní pro postavy, které mohou bojovat.
/// </summary>
public interface ICombatant
{
    /// <summary>
    /// Jméno při výpisu do konzole.
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Aktuální počet životů postavy.
    /// </summary>
    int Health { get; set; }

    /// <summary>
    /// Útok postavy, který se použije při boji.
    /// </summary>
    int AttackBase { get; set; }

    /// <summary>
    /// Explicitně určuje, zda je postava naživu.
    /// </summary>
    bool IsAlive => Health > 0;
}
