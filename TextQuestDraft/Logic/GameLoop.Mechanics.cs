using TextQuestDraft.Clients;

namespace TextQuestDraft.Logic;

public partial class GameLoop
{
    /// <summary>
    /// Načít vstup od hráče z konzole.
    /// </summary>
    /// <returns></returns>
    private string? ReadInput()
    {
        terminal.WritePlayer("Player:");
        return Console.ReadLine()?.ToLowerInvariant();
    }

    /// <summary>
    /// Prompt pro AI službu.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task<AiServiceOutput> Prompt(string? input)
    {
        // TODO: Odeslat prompt na AI službu a vrátit výstup
        return new AiServiceOutput();
    }

    /// <summary>
    /// KOntrola zdraví hráče.
    /// </summary>
    private void CheckPlayerHealth()
    {
        // TODO: Přidat logiku pro kontrolu zdraví hráče
        // Pokud je zdraví hráče 0 nebo méně, hra končí a nastaví se IsRunning na false
    }

    /// <summary>
    /// Výběr postavy na začátku hry.
    /// </summary>
    private void ChooseCharacter()
    {
        // TODO: Přidat logiku pro výběr postavy
    }


    /// <summary>
    /// Vypíše stav hráče do konzole.
    /// A zároveň dostupné příkazy.
    /// </summary>
    private void WriteStatusLine()
    {
        terminal.WritePlayer($"❤️ Health: {player.Health} | 🗡️ Attack Power: {player.AttackBase} | 🎒 Inventory: {string.Join(',', player.Inventory.Select(i => i.Type.ToString()).ToList())}");
        terminal.WritePlayer("Available commands: fight, status, weapon and quit. Any other input will continue the story with AI.");
    }

 
}