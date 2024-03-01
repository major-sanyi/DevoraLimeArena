namespace DevoraLimeArena.Shared.Models
{
    /// <summary>
    /// Parent class for every type of champion.
    /// </summary>
    public abstract class Champion
    {
        /// <summary>
        /// When set all champion dies from a single hit.
        /// </summary>
        public static bool SuddenDeath { get; set; } = false;

        /// <summary>
        /// When true the champions will have part of the GUID.
        /// </summary>
        public static bool ShowIdInToString { get; set; } = false;

        /// <summary>
        /// Raised when the cah
        /// </summary>
        public event EventHandler? ChampionDied;

        /// <summary>
        /// Id of a champion.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Maximum HP of a Champion.
        /// </summary>
        public abstract int MaxHp { get; }

        /// <summary>
        /// Hp of a champion.
        /// A champion with 0 Hp is dead.
        /// </summary>
        public abstract int Hp { get; protected set; }

        /// <summary>
        /// The champion gets challanged to a fight.
        /// </summary>
        /// <param name="attacker">The challanger of the fight.</param>
        public abstract void Fight(Champion attacker);

        /// <summary>
        /// The champions rests and recovers some HP.
        /// </summary>
        public virtual void Rest()
        {
            if (Hp + 10 > MaxHp)
            {
                Hp = MaxHp;
            }
            else
            {
                Hp += 10;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Champion"/> class.
        /// </summary>
        protected Champion()
        {
            Hp = MaxHp;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is not Champion other) return false;
            else return Id.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            // Workaround for tests.
            // Tests are run twice in VS Test adapter and in this case Test1 and Test2 name is different because of GUID.
            if (ShowIdInToString)
                return $"{GetType().Name} {Id.ToString()[..4]}";
            else
                return $"{GetType().Name}";
        }

        /// <summary>
        /// The champions take dmg.
        /// If it's below the threshold, the champion dies.
        /// </summary>
        public virtual void TakeDmg()
        {
            if (SuddenDeath)
            {
                Hp = 0;
                ChampionDied?.Invoke(this, new());
            }
            else
            {
                Hp /= 2;
                if (Hp < MaxHp / 4)
                {
                    Hp = 0;
                    ChampionDied?.Invoke(this, new());
                }
            }
        }
    }
}
