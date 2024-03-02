using DevoraLimeArena.Shared.Models;

namespace DevoraLimeArena.Shared.Services
{
    /// <summary>
    /// Service for handling multiple arenas.
    /// </summary>
    public class ArenaWarService : IArenaWarService
    {
        /// <summary>
        /// The arenas and their fights.
        /// </summary>
        private Dictionary<Arena, Task> Arenas { get; set; } = [];

        /// <inheritdoc/>
        public Guid CreateArena(int N)
        {
            Arena arena = new(N);

            Task task = Task.Run(arena.FightUntilOneStands);
            Arenas.Add(arena, task);
            return arena.Id;
        }

        /// <inheritdoc/>
        public async Task<List<Fight>> GetArenaFights(Guid id)
        {
            if (Arenas.Any(x => x.Key.Id == id))
            {
                KeyValuePair<Arena, Task> arenaFight = Arenas.First(x => x.Key.Id == id);
                await arenaFight.Value;
                return arenaFight.Key.Fights;
            }
            else { throw new ArgumentException("This arena does not exists."); }
        }

        private static Task StartFighting(Arena arena)
        {
            return Task.Run(arena.FightUntilOneStands);
        }
    }
}
