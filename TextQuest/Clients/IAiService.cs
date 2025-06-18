namespace TextQuest.Clients;

/// <summary>
/// Obec rozhraní pro klienta, který odesílá požadavky a přijímá odpovědi.
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Pošle prompt na AI službu a vrátí výstup.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<AiServiceOutput?> SendPromptAsync(string input);
}