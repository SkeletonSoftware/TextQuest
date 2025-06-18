using TextQuest.Clients;
using TextQuest.Contracts;

namespace TextQuest.Logic;

public partial class GameLoop
{
    /// <summary>
    /// Souboj s nepřítelem.
    /// </summary>
    /// <param name="response"></param>
    private void Fight(Response response)
    {
        // Zde by měl být kód pro vytvoření nepřítele
        if (!response.FightAvailable.HasValue)
        {
            terminal.WriteError("❌ You cannot fight right now.");
            return;
        }

        // Vytvoření nepřítele
        var enemy = Enemy.Create(
            response.EnemyType ?? "Spider",
            response.EnemyHealth ?? 30,
            response.EnemyAttack ?? 10);
        terminal.WriteNarrator($"👹 You have met {enemy.Type}! It has {enemy.Health} HP!");

        // Soubojová smyčka
        // Hráč a nepřítel se střídají v útocích, dokud jeden z nich nezemře
        while (((ICombatant)enemy).IsAlive && ((ICombatant)player).IsAlive)
        {
            // Hráč útočí
            var winBonus = (response.ChanceOfWinning ?? 0) * 10;
            if (Attack(player, enemy, winBonus) <= 0)
            {
                terminal.WriteNarrator($"🎉 You defeated {enemy.Type}!");
                break;
            }

            // Nepřítel útočí
            if (Attack(enemy, player, 0) <= 0)
            {
                terminal.WriteNarrator("💀 You lost without a chance!");
                return;
            }
        }

        Loot(response);
    }

    /// <summary>
    /// Útok mezi dvěma bojovníky.
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <param name="bonus"></param>
    /// <returns></returns>
    private int Attack(ICombatant attacker, ICombatant defender, double bonus)
    {
        // Vypočítám poškození útoku
        var random = new Random().Next(-5, 5);
        var attackPower = attacker.AttackBase + bonus + random;
        var damage = (int)Math.Max(attackPower, 0);

        // Odečtu poškození od zdraví obránce
        defender.Health -= damage;

        // Utočí hráč nebo NPC
        if (attacker is Player)
            terminal.WritePlayer($"🗡️ {attacker.Name} attacks {defender.Name}💥 for {damage} dmg. {defender.Name} has {Math.Max(defender.Health, 0)} HP left.");
        else
            terminal.WriteError($"⚔️ {attacker.Name} hits {defender.Name}💥 for {damage} dmg. {defender.Name} has {Math.Max(defender.Health, 0)} HP left.");

        return defender.Health;
    }
}