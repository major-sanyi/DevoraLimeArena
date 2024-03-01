namespace DevoraLimeArena.Shared
{
    /// <summary>
    /// Common properties for the Game.
    /// </summary>
    public static class CommonProperties
    {
        /// <summary>
        /// Random generator for the game.
        /// </summary>
        public static Random Rnd { get; } = new();

        /// <summary>
        /// Default Size of the arena.
        /// </summary>
        public const int DefaultArenaSize = 12;
    }
}
