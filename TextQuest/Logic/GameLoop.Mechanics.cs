using TextQuest.Clients;
using TextQuest.Contracts;
using TextQuest.Models;

namespace TextQuest.Logic;

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
        var result = await service.SendPromptAsync(input ?? "Continue the story");

        // Kontrola, zda AI služba vrátila platný výstup
        if (result is null)
        {
            terminal.WriteError("❌ AI service returned null response.");
            throw new Exception("<UNK> AI service returned null response.");
        }

        // Pokud AI vrátila výstup, vypíšeme zprávu
        terminal.WriteNarrator(result.Response?.Message ?? "Narrator is silent.");
        return result;
    }

    /// <summary>
    /// KOntrola zdraví hráče.
    /// </summary>
    private void CheckPlayerHealth()
    {
        // Pokud hráč má zdraví větší než 0, pokračuje hra
        if (((ICombatant)player).IsAlive) return;

        // Pokud hráč nemá žádné zdraví, hra končí
        terminal.WriteNarrator("❌ You have been defeated! GameLoop over.");
        IsRunning = false;
    }

    /// <summary>
    /// Výběr postavy na začátku hry.
    /// </summary>
    private void ChooseCharacter()
    {
        terminal.WriteNarrator("Press a number to select your character:");
        terminal.WriteNarrator("""
                               1.🗡️ Knight - Easy peasy!
                               2.👹 Programmer - You wont survive!
                               """);

        while (player.Type is null)
        {
            var key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    player.SetCharacterType(CharacterType.Knight);
                    break;
                case "2":
                    player.SetCharacterType(CharacterType.Programmer);
                    break;
                default:
                    terminal.WriteNarrator("Please type 1 or 2");
                    continue;
            }
        }

        terminal.WriteNarrator($"You chose {player.Type}!");
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