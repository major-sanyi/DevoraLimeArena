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
        public Dictionary<Arena, Task> Arenas { get; set; } = [];

        /// <summary>
        /// Creates an arena.
        /// </summary>
        /// <returns>The id of the arena.</returns>
        public Guid CreateArena()
        {
            Arena arena = new Arena();

            Task task = Task.Run(arena.FightUntilOneStands);
            Arenas.Add(arena, task);
            return arena.Id;
        }

        /// <summary>
        /// Waits until an arena fight is finished then returns the arena.
        /// </summary>
        /// <param name="id"></param>
        public async Task<List<Fight>> GetArenaFights(Guid id)
        {
            KeyValuePair<Arena, Task> arenaFight = Arenas.First(x => x.Key.Id == id);
            if (arenaFight.Value is not null)
            {
                await arenaFight.Value;
            }
            return arenaFight.Key.Fights;
        }

        private Task StartFighting(Arena arena)
        {
            return Task.Run(arena.FightUntilOneStands);
        }
    }
}
