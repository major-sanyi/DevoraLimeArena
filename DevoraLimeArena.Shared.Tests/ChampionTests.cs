using DevoraLimeArena.Shared.Models;

namespace DevoraLimeArena.Shared.Tests
{
    [TestFixture]
    public class ChampionTests
    {
        private static readonly object[][] maxHpTestCaseSource = [
            [new Archer(), 100],
            [new Knight(), 150],
            [new Fighter(), 120]
        ];
        private static readonly object[][] restTestCaseSource = [
            [new Archer(), 60],
            [new Knight(), 85],
            [new Fighter(), 70]
        ];
        private static readonly object[][] nameTestCaseSource = [
            [new Archer(), "Archer"],
            [new Knight(), "Knight"],
            [new Fighter(), "Fighter"]
        ];
        private static readonly object[][] fightTestCaseSource = GenerateFightTestCase(true);
        private static readonly object[][] fightTestCaseSourceWithSuddenDeath = GenerateFightTestCase(false);
        private static object[][] GenerateFightTestCase(bool isNotSuddenDeath)
        {
            return [
                [new Archer(), new Knight(new KnightLives()), 100, 150],
                [new Archer(), new Knight(new KnightDies()), 100, isNotSuddenDeath ? 75 : 0],
                [new Archer(), new Fighter(), 100, isNotSuddenDeath ? 60 : 0],
                [new Archer(), new Archer(), 100, isNotSuddenDeath ? 50 : 0],
                [new Knight(), new Archer(), 150, isNotSuddenDeath ? 50 : 0],
                [new Knight(), new Knight(), 150, isNotSuddenDeath ? 75 : 0],
                [new Knight(), new Fighter(), isNotSuddenDeath ? 75 : 0, 120],
                [new Fighter(), new Archer(), 120, isNotSuddenDeath ? 50 : 0],
                [new Fighter(), new Knight(), 120, 150],
                [new Fighter(), new Fighter(), 120, isNotSuddenDeath ? 60 : 0],
            ];
        }
        private static int deadHasBeenCalledNTimes = 0;

        [TearDown]
        public void TearDown() { Champion.SuddenDeath = false; deadHasBeenCalledNTimes = 0; }

        [TestCaseSource(nameof(maxHpTestCaseSource))]
        public void ChampionsShouldStart_WithTheirMaxHp(Champion champion, int maxHp)
        {
            Assert.Multiple(() =>
            {
                Assert.That(champion.MaxHp, Is.EqualTo(maxHp));
                Assert.That(champion.Hp, Is.EqualTo(maxHp));
            });
        }

        [TestCaseSource(nameof(restTestCaseSource))]
        public void ChampionsShouldRegainHp_AfterARest(Champion champion, int wantedHp)
        {
            champion.TakeDmg();
            champion.Rest();
            Assert.That(champion.Hp, Is.EqualTo(wantedHp));
        }
        [TestCaseSource(nameof(maxHpTestCaseSource))]
        public void ChampionsShouldNotHaveMoreHpThan_MaxAfterRest(Champion champion, int maxHp)
        {
            champion.Rest();
            champion.Rest();
            champion.Rest();
            champion.Rest();
            champion.Rest();
            Assert.That(champion.Hp, Is.EqualTo(maxHp));
        }

        [TestCaseSource(nameof(nameTestCaseSource))]
        public void ChampionShouldHaveCorrectName(Champion champion, string name)
        {
            Assert.That(champion.Name, Does.StartWith(name));
        }

        [TestCaseSource(nameof(fightTestCaseSource))]
        public void CorrectChampionShouldTakeDmg_InAFight(Champion attacker, Champion defender, int attackerEndHp, int defenderEndHp)
        {
            defender.Fight(attacker);
            Assert.Multiple(() =>
            {
                Assert.That(attacker.Hp, Is.EqualTo(attackerEndHp));
                Assert.That(defender.Hp, Is.EqualTo(defenderEndHp));
            });
        }

        [TestCaseSource(nameof(fightTestCaseSourceWithSuddenDeath))]
        public void CorrectChampionShouldDie_InASuddenDeathFight(Champion attacker, Champion defender, int attackerEndHp, int defenderEndHp)
        {
            Champion.SuddenDeath = true;
            attacker.ChampionDied += ChampionDied;
            defender.ChampionDied += ChampionDied;
            defender.Fight(attacker);
            Assert.Multiple(() =>
            {
                Assert.That(attacker.Hp, Is.EqualTo(attackerEndHp));
                Assert.That(defender.Hp, Is.EqualTo(defenderEndHp));
                if (attackerEndHp == 0 || defenderEndHp == 0)
                    Assert.That(deadHasBeenCalledNTimes, Is.EqualTo(1));
            });
        }

        private void ChampionDied(object? sender, EventArgs e)
        {
            if (sender is Champion deadOne)
            {
                Assert.That(deadOne.Hp, Is.EqualTo(0));
                deadHasBeenCalledNTimes++;
            }
        }
    }

    internal class KnightLives() : Random
    {
        public override int Next(int maxValue)
        {
            return 6;
        }
    }
    internal class KnightDies() : Random
    {
        public override int Next(int maxValue)
        {
            return 1;
        }
    }
}