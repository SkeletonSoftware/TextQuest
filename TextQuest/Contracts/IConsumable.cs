using TextQuest.Logic;

namespace TextQuest.Contracts;

public interface IConsumable
{
    string Use(ICombatant combatant);
}