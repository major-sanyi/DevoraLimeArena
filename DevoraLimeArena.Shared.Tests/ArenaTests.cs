using DevoraLimeArena.Shared.Models;

namespace DevoraLimeArena.Shared.Tests
{
    [TestFixture]
    public class ArenaTests
    {

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
    }
}
