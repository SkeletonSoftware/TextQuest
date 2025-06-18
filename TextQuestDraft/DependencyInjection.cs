using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TextQuestDraft.Clients;
using TextQuestDraft.Common;
using TextQuestDraft.Logic;

namespace TextQuestDraft;

public static class DependencyInjection
{
    /// <summary>
    /// Přidání služeb pro TextQuest do DI kontejneru.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IServiceCollection AddTextQuestServices(this IServiceCollection services, HostBuilderContext context)
    {
        // Konfigurace
        services.AddOptions<TextQuestOptions>()
            .Bind(context.Configuration.GetSection(TextQuestOptions.ConfigurationSectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // AI klient
        // services.AddSingleton<IAiService, OllamaAiService>();
        services.AddSingleton<IAiService, MockAiService>();

        // Wrapper kolem Console, kvuli hezcim vypisum
        services.AddTransient<TextQuestTerminal, TextQuestTerminal>();

        // Registrace herní logiky
        services.AddSingleton<Player>();
        services.AddTransient<Enemy>();
        services.AddTransient<GameLoop>();

        return services;
    }
}