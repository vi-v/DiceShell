using Antlr4.Runtime;
using DiceShell;
using DiceShell.Gen;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceShellTest
{
    [TestClass]
    public class DiceVisitorTest
    {
        [TestMethod]
        public void TestFunc()
        {
            DiceParser parser = Setup("2d4 + 2");
            DiceParser.DiceGroupContext context = parser.diceGroup();
            DiceVisitor visitor = new DiceVisitor();
            visitor.Visit(context);
        }

        [TestMethod]
        public void MultipleDiceDiceGroupTest()
        {
            DiceParser parser = Setup("2d4");
            DiceParser.DiceGroupContext context = parser.diceGroup();
            DiceVisitor visitor = new DiceVisitor();

            DiceGroup dg = (DiceGroup)visitor.VisitDiceGroup(context);

            dg.Count.Should().Be(2);
            dg.DiceList[0].Size.Should().Be(4);
            dg.DiceList[1].Size.Should().Be(4);
        }

        [TestMethod]
        public void SingleDiceDiceGroupTest()
        {
            DiceParser parser = Setup("d20");
            DiceParser.DiceGroupContext context = parser.diceGroup();
            DiceVisitor visitor = new DiceVisitor();

            DiceGroup dg = (DiceGroup)visitor.VisitDiceGroup(context);

            dg.Count.Should().Be(1);
            dg.DiceList.First().Size.Should().Be(20);
        }

        [TestMethod]
        public void NumberAtomTest()
        {
            DiceParser parser = Setup("3");
            DiceParser.AtomContext context = parser.atom();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitAtom(context);

            result.Should().Be(3);
        }

        [TestMethod]
        public void DiceGroupAtomTest()
        {
            DiceParser parser = Setup("2d6");
            DiceParser.AtomContext context = parser.atom();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitAtom(context);

            result.Should().BeGreaterThan(2);
        }

        private static DiceParser Setup(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DiceLexer speakLexer = new DiceLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            DiceParser speakParser = new DiceParser(commonTokenStream);
            return speakParser;
        }
    }
}
