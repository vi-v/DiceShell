using DiceShell;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiceShellTest
{
    [TestClass]
    public class DiceGroupTest
    {
        [TestMethod]
        public void InitializeEmptyTest()
        {
            DiceGroup dg = new DiceGroup();

            dg.Count.Should().Be(0);
            dg.Value.Should().Be(0);
            dg.HasBeenRolled.Should().BeFalse();
        }

        [TestMethod]
        public void InitializeWithDiceTest()
        {
            DiceGroup dg = new DiceGroup(FourDEight());

            dg.Count.Should().Be(4);
            dg.Value.Should().Be(0);
            dg.HasBeenRolled.Should().BeFalse();
        }

        [TestMethod]
        public void RollDiceGroupTest()
        {
            DiceGroup dg = new DiceGroup(FourDEight());

            dg.Roll();

            dg.Value.Should().BeGreaterOrEqualTo(4);
            dg.HasBeenRolled.Should().BeTrue();
        }

        [TestMethod]
        public void RollDiceGroupTwiceTest()
        {
            DiceGroup dg = new DiceGroup(FourDEight());
            Action roll = () => dg.Roll();

            dg.Roll();

            roll.Should().Throw<DiceAlreadyRolledException>();
        }

        private static List<Dice> FourDEight()
        {
            return new List<Dice>
            {
                new Dice(8),
                new Dice(8),
                new Dice(8),
                new Dice(8),
            };
        }
    }
}
