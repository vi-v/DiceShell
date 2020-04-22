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

            er.AtomList.Should().BeEmpty();
        }

        [TestMethod]
        public void AddAtomTest()
        {
            Atom firstAtom = new Atom();
            Atom secondAtom = new Atom();
            firstAtom.SetDiceGroup(FourDEightGroup());
            secondAtom.SetDiceGroup(TwoDTwentyGroup());
            ExpressionResult er = new ExpressionResult();

            er.AddAtom(firstAtom);
            er.AddAtom(secondAtom);

            er.AtomList.Should().BeEquivalentTo(new List<Atom> { firstAtom, secondAtom }, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void AddAtomListTest()
        {
            Atom firstAtom = new Atom();
            Atom secondAtom = new Atom();
            Atom thirdAtom = new Atom();
            Atom fourthAtom = new Atom();
            firstAtom.SetDiceGroup(FourDEightGroup());
            secondAtom.SetDiceGroup(TwoDTwentyGroup());
            thirdAtom.SetDiceGroup(TwoDTwentyGroup());
            fourthAtom.SetDiceGroup(FourDEightGroup());
            List<Atom> firstAtomList = new List<Atom> { firstAtom, secondAtom };
            List<Atom> secondAtomList = new List<Atom> { thirdAtom, fourthAtom };
            IEnumerable<Atom> atomsCombined = firstAtomList.Concat(secondAtomList);
            ExpressionResult er = new ExpressionResult();

            er.AddAtoms(firstAtomList);
            er.AddAtoms(secondAtomList);

            er.AtomList.Should().BeEquivalentTo(atomsCombined, opt => opt.WithStrictOrdering());
        }

        [TestMethod]
        public void ExecuteResultTest()
        {
            Atom firstAtom = new Atom();
            Atom secondAtom = new Atom();
            Atom thirdAtom = new Atom();
            Atom fourthAtom = new Atom();
            firstAtom.SetDiceGroup(FourDEightGroup());
            secondAtom.SetDiceGroup(TwoDTwentyGroup());
            thirdAtom.SetModifier(1000);
            fourthAtom.SetModifier(500);
            List<Atom> firstAtomList = new List<Atom> { firstAtom, secondAtom };
            List<Atom> secondAtomList = new List<Atom> { thirdAtom, fourthAtom };
            ExpressionResult er = new ExpressionResult();

            er.AddAtoms(firstAtomList);
            er.AddAtoms(secondAtomList);
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
