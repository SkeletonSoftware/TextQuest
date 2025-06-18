using TextQuestDraft.Common;

namespace TextQuestDraft.Clients;

/// <summary>
/// Služba pro simulaci AI, která vrací náhodné hodnoty pro testování.
/// </summary>
/// <param name="terminal"></param>
public class MockAiService(TextQuestTerminal terminal) : IAiService
{
    /// <inheritdoc />
    public async Task<AiServiceOutput?> SendPromptAsync(string input)
    {
        // TODO: Přidat logiku pro odeslání promptu na AI službu
        return new AiServiceOutput();
    }
}