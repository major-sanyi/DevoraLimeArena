namespace DevoraLimeArena.Shared.Models
{
    /// <summary>
    /// Archer Champion.
    /// </summary>
    public class Archer : Champion
    {
        /// <inheritdoc/>
        public override int MaxHp => 100;

        /// <inheritdoc/>
        public override int Hp { get; protected set; }

        /// <inheritdoc/>
        public override void Fight(Champion attacker)
        {
            // Archer's gonna have a bad day, if it's attacked.
            switch (attacker)
            {
                case Archer: TakeDmg(); break;
                case Fighter: TakeDmg(); break;
                case Knight: TakeDmg(); break;
            }
        }
    }
}
