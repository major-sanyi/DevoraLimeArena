using DevoraLimeArena.Shared.Services;

namespace DevoraLimeArena.Shared.Tests
{
    internal class ArenaWarServiceTests
    {
        private ArenaWarService arenaWarService;

        [SetUp]
        public void SetUp()
        {
            arenaWarService = new ArenaWarService();
        }

        [TestCase(5)]
        [TestCase(200)]
        public void CreateArenaShouldNotThrow_WhenCorrectN(int N)
        {
            Assert.DoesNotThrow(() =>
            {
                Guid arenaId = arenaWarService.CreateArena(N);
                Assert.That(arenaId, Is.Not.EqualTo(Guid.Empty));
            });
        }

        [TestCase(1)]
        [TestCase(-5)]
        [TestCase(0)]
        public void CreateArenaShouldThrow_WhenInCorrectN(int N)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Guid arenaId = arenaWarService.CreateArena(N);
                Assert.That(arenaId, Is.Not.EqualTo(Guid.Empty));
            });
        }

        [Test]
        public void GetArenaFightsShouldReturnFights()
        {
            Assert.DoesNotThrowAsync(async () =>
           {
               Guid arenaId = arenaWarService.CreateArena(5);
               List<Fight> fights = await arenaWarService.GetArenaFights(arenaId);
               Assert.That(fights, Is.Not.Empty);
           });
        }

        [Test]
        public void GetArenaFightsShouldThrow_WhenArenaDoesNotExists()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                Guid arenaId = Guid.NewGuid();
                List<Fight> fights = await arenaWarService.GetArenaFights(arenaId);
            });
        }
    }
}
