using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TextQuest.Common;
using TextQuest.Logic;

namespace TextQuest.Clients;

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
        // Zobrazit loading indikátor
        using var cts = ShowLoading(out var loadingTask);

        // 1. Request tělo
        var content = await PreparePayload(input);

        // 2. Pošli požadavek
        var response = await Post(content);

        // 3. Zpracuj odpověď
        var result = await ReadResponse(response);

        // Schovat loading indikátor
        await HideLoading(cts, loadingTask);

        // Vrať výsledek
        return new AiServiceOutput { Response = result };
    }

    /// <summary>
    /// Zpracovává HTTP odpověď a vrací deserializovaný objekt Response.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static async Task<Response?> ReadResponse(HttpResponseMessage response)
    {
        // Převést odpověď na JsonDument, abychom mohli snadno získat vnořený JSON
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        // Zkontrolovat, zda obsahuje vnořený JSON vlastnost "response"
        if (!doc.RootElement.TryGetProperty("response", out var responseElement))
            throw new Exception("Missing 'response' property.");

        // Zkontrolujeme, že hodnota 'response' není prázdná
        var responseJson = responseElement.GetString();
        if (string.IsNullOrWhiteSpace(responseJson))
            throw new Exception("Empty 'response' value.");

        // Deserializovat vnořený JSON do objektu Response
        // Pozor - zde předpokládáme, že Response je třída, která odpovídá struktuře vnořeného JSON
        var result = JsonSerializer.Deserialize<Response>(responseJson);
        if (result == null)
            throw new Exception("Failed to deserialize inner response.");

        return result;
    }

    /// <summary>
    /// Odesílá HTTP POST požadavek na AI službu.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private async Task<HttpResponseMessage> Post(StringContent content)
    {
        // Provést POST požadavek na AI službu
        var response = await _httpClient.PostAsync("/api/generate", content);

        // Zkontrolovat, zda požadavek byl úspěšný
        response.EnsureSuccessStatusCode();
        return response;
    }

    /// <summary>
    /// Připraví payload pro HTTP POST požadavek.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task<StringContent> PreparePayload(string input)
    {
        // Načíst AI prompt z souboru
        var prompt = await File.ReadAllTextAsync(_options.PromptFilePath);

        // Serializovat stav hráče do JSON formátu
        var player = JsonSerializer.Serialize(_player, JsonSerializerOptions.Web);

        // Vytvořit tělo požadavku
        // Jedná se o anonymní objekt, který obsahuje model, prompt a stream
        // Lze do něj přidat další vlastnosti, pokud je potřeba
        // Například můžeme přidat další informace o hráči, aktuálním stavu hry atd.
        // a celé to pak jednoduše serializovat do JSON formátu
        var request = new
        {
            model = _options.Model,

            // !!! Chyták - prompt musí být ve formátu string, který AI model očekává
            // Nejde tedy poslat přímo objekt, ale musíme ho serializovat striktně jako string
            prompt = $"""
                      AI Prompt:
                      {prompt}

                      Player State:
                      {player}

                      Current Input:
                      {input}
                      """,
            stream = false
        };

        // Serializovat tělo požadavku do JSON formátu
        var json = JsonSerializer.Serialize(request, JsonSerializerOptions.Web);

        // Vytvořit StringContent s JSON daty a nastavit Content-Type na application/json
        // Protože AI model očekává JSON formát
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        return content;
    }


    /// <summary>
    /// Zobrazí načítací indikátor a vrátí CancellationTokenSource pro jeho zrušení.
    /// </summary>
    /// <param name="loadingTask"></param>
    /// <returns></returns>
    private CancellationTokenSource ShowLoading(out Task loadingTask)
    {
        CancellationTokenSource? cts = null;
        try
        {
            cts = new CancellationTokenSource();
            loadingTask = _terminal.ShowLoadingAsync(cts.Token);
            return cts;
        }
        catch
        {
            cts?.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Schová načítací indikátor.
    /// </summary>
    /// <param name="cts"></param>
    /// <param name="loadingTask"></param>
    private static async Task HideLoading(CancellationTokenSource cts, Task loadingTask)
    {
        cts.Cancel();
        await loadingTask;
    }
}