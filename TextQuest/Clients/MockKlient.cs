using TextQuest.Common;

namespace TextQuest.Clients;

/// <summary>
/// Služba pro simulaci AI, která vrací náhodné hodnoty pro testování.
/// </summary>
/// <param name="terminal"></param>
public class MockAiService(TextQuestTerminal terminal) : IAiService
{
    /// <inheritdoc />
    public async Task<AiServiceOutput?> SendPromptAsync(string input)
    {
        using var cts = new CancellationTokenSource();

        // Budeme potřebovat náhodná čísla
        var random = new Random();

        // Zobrazíme načítací animaci
        var loadingTask = terminal.ShowLoadingAsync(cts.Token);

        // Simulace zpoždění pro načítání
        await Task.Delay(1000, CancellationToken.None);

        // Vytvoříme výstupní objekt s náhodnými hodnotami
        AiServiceOutput result = new()
        {
            Response = new Response
            {
                Message = $"Stojíš před zástupem nepřátel. Zatímco se snažíš najít cestu, tvé srdce buší jako splašené. Co uděláš? (fight/heal/weapon/quit :D)",
                ChanceOfEscaping = random.NextDouble(),
                ChanceOfWinning = random.NextDouble(),
                ChanceOfLoot = random.NextDouble(),
                EnemyHealth = random.Next(1, 100),
                EnemyAttack = random.Next(1, 50),
                EnemyType = "Zombák",
                FightAvailable = true
            }
        };

        // Vypnu načítací animaci
        cts.Cancel();
        await loadingTask;
        return result;
    }
}