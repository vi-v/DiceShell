using DiceShell;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiceShellTest
{
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void InitializeTest()
        {
            Dice d = new Dice(20);

            d.Size.Should().Be(20);
            d.HasBeenRolled.Should().BeFalse();
            d.Value.Should().Be(0);
        }

        [TestMethod]
        public void UpperBoundTest()
        {
            Dice d = new Dice(20);
            Random r = new Random(10);

            d.Roll(r);

            d.Value.Should().Be(20);
        }

        [TestMethod]
        public void LowerBoundTest()
        {
            Dice d = new Dice(20);
            Random r = new Random(14);

            d.Roll(r);

            d.Value.Should().Be(1);
        }

        [TestMethod]
        public void SameDiceRolledAgainTest()
        {
            Dice d = new Dice(8);
            Action a = () => d.Roll();

            d.Roll();

            a.Should().Throw<DiceAlreadyRolledException>();
        }
    }
}
