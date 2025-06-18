namespace TextQuestDraft.Common;

public class TextQuestTerminal
{
    public TextQuestTerminal()
    {
        // Nastavení konzole podle operačního systému
        SetUpByOs();
    }

    private void SetUpByOs()
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Title = "TextQuest - Linux/Mac";
                break;
            default:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Title = "TextQuest - Windows";
                break;
        }
    }

    /// <summary>
    /// Vypíše chybovou hlášku na konzoli červeně.
    /// </summary>
    /// <param name="message"></param>
    public void WriteError(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }

    /// <summary>
    /// Vypraví zprávu vypravěče na konzoli žlutě.
    /// </summary>
    /// <param name="message"></param>
    public void WriteNarrator(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }

    /// <summary>
    /// Vypíše zprávu hráče na konzoli zeleně.
    /// </summary>
    /// <param name="message"></param>
    public void WritePlayer(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }

    public void WriteItem(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }

    /// <summary>
    /// Simuluje načítání s animací teček.
    /// </summary>
    /// <param name="token"></param>
    public async Task ShowLoadingAsync(CancellationToken token)
    {
        var dots = new[] { ".", "..", "..." };
        var i = 0;
        try
        {
            while (!token.IsCancellationRequested)
            {
                Console.Write($"\r⌛ Narrator is thinking{dots[i % dots.Length]}   ");
                await Task.Delay(500, token);
                i++;
            }
        }
        catch (TaskCanceledException)
        {
            // Ignoruj – normální při Cancel
        }

        Console.Write("\r                                                                                    \r"); // Smazání řádku
    }
}