namespace DevoraLimeArena.Shared.Models
{
    /// <summary>
    /// Knight Champion.
    /// </summary>
    /// <param name="randomGenerator">Random generator for luck based encounters.</param>
    public class Knight(Random? randomGenerator = null) : Champion
    {
        private readonly Random randomGenerator = randomGenerator ?? CommonProperties.Rnd;

        /// <inheritdoc/>
        public override int MaxHp => 150;

        /// <inheritdoc/>
        public override int Hp { get; protected set; }

        /// <inheritdoc/>
        public override void Fight(Champion attacker)
        {
            switch (attacker)
            {
                case Archer:
                    if (randomGenerator.Next(10) < 4) TakeDmg();
                    break;
                case Fighter: break;
                case Knight: TakeDmg(); break;
            }
        }
    }
}
