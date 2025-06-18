using TextQuest.Clients;
using TextQuest.Contracts;
using TextQuest.Models;

namespace TextQuest.Logic;

public partial class GameLoop
{
    private void Loot(Response response)
    {
        // Vyhodnocení loot
        // Pokud hráč přežil a loot je k dispozici, přidá se do inventáře
        if (((ICombatant)player).IsAlive && (response.ChanceOfLoot ?? 0) > options.Value.LootTreshold)
        {
            terminal.WriteNarrator("🎁 You found some loot!");

            var rnd = new Random();
            var isPotion = (response.ChanceOfLoot ?? 0) < (1d + options.Value.LootTreshold) / 2;
            var loot = Item.Create(isPotion ? ItemType.Potion : ItemType.Weapon, rnd.Next(options.Value.MinItemPower, options.Value.MaxItemPower));

            player.Inventory.Add(loot);
            terminal.WriteNarrator($"🎁 You found {loot.Type}!");
        }
        else if (player.Health > 0)
        {
            terminal.WriteNarrator("No loot this time.");
        }
    }

    /// <summary>
    /// Použití zbran
    /// </summary>
    private void UseItem(ItemType itemType)
    {
        // Zkontroluj, zda hráč má nějaké zbraně
        var item = player.Inventory.FirstOrDefault(i => i.Type == itemType);
        if (item is IConsumable consumable)
        {
            // Pokud má hráč zbraň, použij ji
            var message = consumable.Use(player);
            // Odstraň zbraň z inventáře
            player.Inventory.Remove(item);
            // Vypiš zprávu o použití zbraně
            terminal.WriteItem(message);
        }
        else
        {
            terminal.WriteNarrator("❌ You don't have any weapons.");
        }
    }
}