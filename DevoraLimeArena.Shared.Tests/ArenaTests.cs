using DevoraLimeArena.Shared.Models;

namespace DevoraLimeArena.Shared.Tests
{
    [TestFixture]
    public class ArenaTests
    {
        // Number of champions, Number of fights, [N number for generation, and 2 int per fights]
        private static readonly object[][] arenaRandomNumberTests =
            [
                [3, 2, new int[] { 0, 0, 0, 0, 1, 0, 1 }]
            ];

        [TearDown]
        public void TearDown() { Champion.SuddenDeath = false; }

        [TestCase(2)]
        [TestCase(5)]
        [TestCase(12)]
        [TestCase(null)]
        public void ArenaShouldBeCreatedWithorWithoutN(int? N)
        {
            Arena arena;
            if (N is not null)
            {
                arena = new Arena((int)N);
                Assert.That(arena, Is.Not.Null);
                Assert.That(arena.Champions, Has.Count.EqualTo(N));
            }
            else
            {
                arena = new Arena();
                Assert.That(arena, Is.Not.Null);
                Assert.That(arena.Champions, Has.Count.EqualTo(CommonProperties.DefaultArenaSize));
            }
        }
        [TestCase(1)]
        [TestCase(-2)]
        [TestCase(-32)]
        public void ArenaShouldThrowErrorOnInvalidN(int N)
        {
            Assert.Throws<ArgumentException>(() => new Arena(N));
        }

        [Test]
        public void OnlyOneShouldStandAfterFight()
        {
            var arena = new Arena();
            arena.FightUntilOneStands();
            Assert.Multiple(() =>
            {
                Assert.That(arena.Champions, Has.Count.EqualTo(1));
                Assert.That(arena.Fights, Is.Not.Empty);
            });
        }

        [TestCaseSource(nameof(arenaRandomNumberTests))]
        public void ArenaMatchCorrectlyPlaysOut(int N, int numberOfFights, int[] randomNumbers)
        {
            Champion.SuddenDeath = true;
            ArenaRandomGen random = new()
            {
                TotallyRandomNumbers = randomNumbers,
            };
            var arena = new Arena(N, random);

            Assert.That(arena.Champions, Has.Count.EqualTo(N));
            arena.FightUntilOneStands();
            Assert.Multiple(() =>
            {
                Assert.That(arena.Champions, Has.Count.EqualTo(1));
                Assert.That(arena.Fights, Has.Count.EqualTo(numberOfFights));
                Assert.That(random.CurrentCounter, Is.EqualTo(randomNumbers.Length));
            });
        }


    }
    internal class ArenaRandomGen : Random
    {
        public int[] TotallyRandomNumbers { get; set; } = [];
        public int CurrentCounter { get; set; } = 0;

        public override int Next(int maxValue)
        {
            return TotallyRandomNumbers[CurrentCounter++];
        }
    }
}
