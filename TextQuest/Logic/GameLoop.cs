using Microsoft.Extensions.Options;
using TextQuest.Clients;
using TextQuest.Common;
using TextQuest.Models;

namespace TextQuest.Logic;

public partial class GameLoop(IAiService service, Player player, TextQuestTerminal terminal, IOptions<TextQuestOptions> options)
{
    /// <summary>
    /// Zda hra běží
    /// </summary>
    private bool IsRunning { get; set; } = true;

    /// <summary>
    /// Startuje hru
    /// </summary>
    public async Task StartAsync()
    {
        // Výběr postavy
        ChooseCharacter();

        // Hra začíná
        await TheLoop();
    }

    private async Task TheLoop()
    {
        // Hráč je na tahu vždy po AI
        // Tak je potřeba uchovat poslední vstup od hráče
        string? input = null!;

        // Herní smyčka
        while (IsRunning)
        {
            // Zobraz stav hráče
            WriteStatusLine();

            // AI je na "tahu"
            var result = await Prompt(input);

            // Vstup od hráče
            input = ReadInput();

            switch (input)
            {
                case "fight":
                    Fight(result.Response!);
                    CheckPlayerHealth(); // Zkontroluje zdraví hráče po boji
                    input = null;
                    break;
                case "potion":
                    UseItem(ItemType.Potion);
                    CheckPlayerHealth(); // Zkontroluje zdraví hráče po použití lektvaru (mohl být jedovatý)
                    input = null;
                    break;
                case "weapon":
                    UseItem(ItemType.Weapon);
                    input = null;
                    break;
                case "quit":
                    IsRunning = false;
                    terminal.WriteNarrator("Exiting the game. Goodbye!");
                    break;
            }
        }
    }
}