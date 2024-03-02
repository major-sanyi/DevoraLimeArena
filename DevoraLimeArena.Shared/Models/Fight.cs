using DevoraLimeArena.Shared.Models;

namespace DevoraLimeArena.Shared.Services
{
    /// <summary>
    /// Record for a fight.
    /// </summary>
    /// <param name="Attacker">The champion who initiated the fight.</param>
    /// <param name="Defender">The champion who was attacked.</param>
    /// <param name="StartingHp">HPs before the battle.</param>
    /// <param name="EndingHp">HPs After the battle.</param>
    public record Fight(Champion Attacker, Champion Defender, AttackerDefenderHp StartingHp, AttackerDefenderHp EndingHp);

    public record AttackerDefenderHp(int AttackerHp, int DefenderHp);
}