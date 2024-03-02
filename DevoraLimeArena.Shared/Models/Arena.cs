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

        private readonly Random randomGenerator;

        /// <param name="N">The size of arena.</param>
        /// <param name="randomGenerator">Random generator for luck based encounters.</param>
        public Arena(int N = CommonProperties.DefaultArenaSize, Random? randomGenerator = null)
        {
            this.randomGenerator = randomGenerator ?? CommonProperties.Rnd;
            if (N < 2)
            {
                throw new ArgumentException("Arena should contain at least 2 heros");
            }
            for (int i = 0; i < N; i++)
            {
                Champion champion = this.randomGenerator.Next(3) switch
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
                Champion attacker = Champions.ElementAt(randomGenerator.Next(Champions.Count));
                Champion defender = Champions.ElementAt(randomGenerator.Next(Champions.Count));
                while (attacker.Equals(defender))
                {
                    defender = Champions.ElementAt(randomGenerator.Next(Champions.Count));
                }

                // Prepare
                AttackerDefenderHp HpBeforeBattle = new(attacker.Hp, defender.Hp);

                // Fight
                defender.Fight(attacker);
                // Locks the list on the case when trying to get partial results.
                Fight currentFight = new(attacker, defender, HpBeforeBattle, new(attacker.Hp, defender.Hp));
                Fights.Add(currentFight);
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
