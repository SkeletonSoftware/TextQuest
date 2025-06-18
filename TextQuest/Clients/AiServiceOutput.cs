namespace TextQuest.Clients;

/// <summary>
/// Tředa pro odpověď klienta.
/// </summary>
public class AiServiceOutput
{
    public Response? Response { get; set; }
}

/// <summary>
/// Třída pro odpověď klienta, která obsahuje zprávu a šance na útěk nebo výhru.
/// </summary>
public class Response
{
    /// <summary>
    /// Odpověď klienta, obvykle textový výstup AI modelu.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Šance na útěk z boje pokud je to relevantní pro danou situaci.
    /// </summary>
    public double? ChanceOfEscaping { get; set; }

    /// <summary>
    /// Šance na výhru v boji, pokud je to relevantní pro danou situaci.
    /// </summary>
    public double? ChanceOfWinning { get; set; }

    /// <summary>
    /// Zda, zda je boj aktuálně dostupný.
    /// </summary>
    public bool? FightAvailable { get; set; }

    /// <summary>
    /// Šance na získání kořisti po boji, pokud je to relevantní pro danou situaci.
    /// </summary>
    public double? ChanceOfLoot { get; set; }

    /// <summary>
    /// Jak se jmenuje nepřítel, se kterým bojujete.
    /// </summary>
    public string? EnemyType { get; set; }

    /// <summary>
    /// Kolik má zdraví nepřítel.
    /// </summary>
    public int? EnemyHealth { get; set; }
    /// <summary>
    /// Kolik má nepřítel útok.
    /// </summary>
    public int? EnemyAttack { get; set; }
}