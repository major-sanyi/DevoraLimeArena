
namespace DevoraLimeArena.Shared.Services
{
    /// <summary>
    /// Service for handling multiple arenas.
    /// </summary>
    public interface IArenaWarService
    {
        /// <summary>
        /// Creates an arena.
        /// </summary>
        /// <returns>The id of the arena.</returns>
        Guid CreateArena(int N);

        /// <summary>
        /// Waits until an arena fight is finished then returns the arena.
        /// </summary>
        /// <param name="id"> The ID of the arena.</param>
        /// <exception cref="ArgumentException">Thrown if the arena does not exists.</exception>
        Task<List<Fight>> GetArenaFights(Guid id);
    }
}