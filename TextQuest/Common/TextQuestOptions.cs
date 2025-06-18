using System.ComponentModel.DataAnnotations;

namespace TextQuest.Common;

public class TextQuestOptions
{
    public static string ConfigurationSectionName => "TextQuest";

    /// <summary>
    /// BaseUrl k api, které bude použito pro komunikaci s AI.
    /// </summary>
    [Required]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Nastavení pro AI model, které bude použito pro generování textu.
    /// Toto není dobré je to svázané s Ollama AI, ale pro jednoduchost to necháme takto.
    /// </summary>
    [Required]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Cesta k souboru s promptem, který bude použit pro generování textu.
    /// </summary>
    [Required]
    public string PromptFilePath { get; set; } = string.Empty;

    /// <summary>
    /// Minimální šance na to získání kořisti po boji.
    /// </summary>
    [Required]
    public double LootTreshold { get; set; }

    /// <summary>
    /// Minimální síla předmětu, který může hráč získat.
    /// </summary>
    [Required]
    public int MinItemPower { get; set; }

    /// <summary>
    /// Maximální síla předmětu, který může hráč získat.
    /// </summary>
    [Required]
    public int MaxItemPower { get; set; }
}