using DiceShell;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceShellTest
{
    [TestClass]
    public class ExpressionResultTest
    {
        [TestMethod]
        public void InitializationTest()
        {
            ExpressionResult er = new ExpressionResult();

            er.DiceGroupList.Should().BeEmpty();
            er.ModifierList.Should().BeEmpty();
        }

        [TestMethod]
        public void AddDiceGroupTest()
        {
            List<DiceGroup> diceGroups = new List<DiceGroup> { FourDEightGroup(), TwoDTwentyGroup() };
            ExpressionResult er = new ExpressionResult();

            er.AddDiceGroup(diceGroups[0]);
            er.AddDiceGroup(diceGroups[1]);

            er.DiceGroupList.Should().BeEquivalentTo(diceGroups, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void AddDiceGroupListTest()
        {
            List<DiceGroup> diceGroups1 = new List<DiceGroup> { FourDEightGroup(), TwoDTwentyGroup() };
            List<DiceGroup> diceGroups2 = new List<DiceGroup> { TwoDTwentyGroup(), FourDEightGroup() };
            IEnumerable<DiceGroup> diceGroupsCombined = diceGroups1.Concat(diceGroups2);
            ExpressionResult er = new ExpressionResult();

            er.AddDiceGroups(diceGroups1);
            er.AddDiceGroups(diceGroups2);

            er.DiceGroupList.Should().BeEquivalentTo(diceGroupsCombined, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void AddModifierTest()
        {
            ExpressionResult er = new ExpressionResult();

            er.AddModifier(5);

            er.ModifierList.Should().BeEquivalentTo(new List<int> { 5 });
        }

        [TestMethod]
        public void AddModifierListTest()
        {
            List<int> modifierList1 = new List<int> { 2, 3 };
            List<int> modifierList2 = new List<int> { 4, 5 };
            ExpressionResult er = new ExpressionResult();

            er.AddModifiers(modifierList1);
            er.AddModifiers(modifierList2);

            er.ModifierList.Should().BeEquivalentTo(new List<int> { 2, 3, 4, 5 });
        }

        [TestMethod]
        public void ExecuteResultTest()
        {
            List<DiceGroup> diceGroups = new List<DiceGroup> { FourDEightGroup(), TwoDTwentyGroup() };
            List<int> modifierList = new List<int> { 1000, 500 };
            ExpressionResult er = new ExpressionResult();

            er.AddDiceGroups(diceGroups);
            er.AddModifiers(modifierList);
            er.Roll();

            er.Value.Should().BeGreaterOrEqualTo(1500 + 4 + 2);
        }

        private static DiceGroup FourDEightGroup()
        {
            return new DiceGroup(new List<Dice>
            {
                new Dice(8),
                new Dice(8),
                new Dice(8),
                new Dice(8),
            });
        }

        private static DiceGroup TwoDTwentyGroup()
        {
            return new DiceGroup(new List<Dice>
            {
                new Dice(20),
                new Dice(20),
            });
        }
    }
}
