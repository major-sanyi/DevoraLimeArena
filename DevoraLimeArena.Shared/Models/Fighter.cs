namespace DevoraLimeArena.Shared.Models
{
    /// <summary>
    /// Fighter Champion.
    /// </summary>
    public class Fighter : Champion
    {
        /// <inheritdoc/>
        public override int MaxHp => 120;

        /// <inheritdoc/>
        public override int Hp { get; protected set; }

        /// <inheritdoc/>
        public override void Fight(Champion attacker)
        {
            switch (attacker)
            {
                case Archer: TakeDmg(); break;
                case Fighter: TakeDmg(); break;
                case Knight knight: knight.TakeDmg(); break;
            }
        }
    }
}
