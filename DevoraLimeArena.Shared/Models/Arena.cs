using DevoraLimeArena.Shared.Services;

namespace DevoraLimeArena.Shared.Models
{
    /// <summary>
    /// Class for Handling Arena matches.
    /// </summary>
    public class Arena
    {
        /// <summary>
        /// Id of the arena.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Champions fighting in the arena.
        /// Only Contains live ones.
        /// </summary>
        public HashSet<Champion> Champions { get; set; } = [];

        /// <summary>
        /// Fights taken up in the arena until now.
        /// </summary>
        public List<Fight> Fights { get; } = [];

        /// <param name="N">The size of arena.</param>
        public Arena(int N = CommonProperties.DefaultArenaSize)
        {
            if (N < 2)
            {
                throw new ArgumentException("Arena should contain at least 2 heros");
            }
            for (int i = 0; i < N; i++)
            {
                Champion champion = CommonProperties.Rnd.Next(3) switch
                {
                    1 => new Fighter(),
                    2 => new Knight(),
                    _ => new Archer(),
                };
                champion.ChampionDied += Champion_ChampionDied;
                Champions.Add(champion);
            }
        }

        /// <summary>
        /// Starts fighting until 1 survives.
        /// </summary>
        public void FightUntilOneStands()
        {
            while (Champions.Count > 1)
            {
                //Select 2 separate champs random
                Champion attacker = Champions.ElementAt(CommonProperties.Rnd.Next(Champions.Count));
                Champion defender = Champions.ElementAt(CommonProperties.Rnd.Next(Champions.Count)); ;
                while (attacker.Equals(defender))
                {
                    defender = Champions.ElementAt(CommonProperties.Rnd.Next(Champions.Count));
                }

                // Prepare
                AttackerDefenderHp HpBeforeBattle = new(attacker.Hp, defender.Hp);

                // Fight
                defender.Fight(attacker);
                Fights.Add(new Fight(attacker, defender, HpBeforeBattle, new(attacker.Hp, defender.Hp)));
            }
        }
        private void Champion_ChampionDied(object? sender, EventArgs e)
        {
            if (sender is Champion champ)
            {
                champ.ChampionDied -= Champion_ChampionDied;
                Champions.Remove(champ);
            }
        }
    }
}
