using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TextQuestDraft;
using TextQuestDraft.Logic;

// Nastavení hostitele a registrace služeb
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) => services.AddTextQuestServices(context))
    .Build();

// Spuštění hry přes DI
var game = host.Services.GetRequiredService<GameLoop>();
await game.StartAsync();