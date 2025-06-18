using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TextQuestDraft.Common;
using TextQuestDraft.Logic;

namespace TextQuestDraft.Clients;

/// <summary>
/// Zajišťuje komunikaci s AI službou pomocí HTTP klienta.
/// Připojuje se na projekt Ollama, který poskytuje AI modely pro generování textu.
/// Lokálně běží na adrese http://localhost:[Port]
/// Lze pouštět na vlastním stroji nebo v Docker kontejneru.
/// </summary>
public class OllamaAiService : IAiService
{
    private readonly TextQuestOptions _options;
    private readonly TextQuestTerminal _terminal;
    private readonly Player _player;
    private readonly HttpClient _httpClient;

    public OllamaAiService(IOptions<TextQuestOptions> options, TextQuestTerminal terminal, Player player)
    {
        _options = options.Value;
        _terminal = terminal;
        _player = player;

        // Nastavit HttpClient s URL AI služby
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_options.Url);
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TextQuest", "1.0"));

    }

    /// <inheritdoc />
    public async Task<AiServiceOutput?> SendPromptAsync(string input)
    {
        // TODO: Přidat logiku pro odeslání promptu na AI službu
        return new AiServiceOutput();
    }
}