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

            result.Should().BeGreaterOrEqualTo(2);
        }

        [TestMethod]
        public void NegativeAtomTest()
        {
            DiceParser parser = Setup("-4d8");
            DiceParser.SignedAtomContext context = parser.signedAtom();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitSignedAtom(context);

            result.Should().BeLessOrEqualTo(-4);
        }

        [TestMethod]
        public void SimpleExpressionTest()
        {
            DiceParser parser = Setup("3d6");
            DiceParser.ExpressionContext context = parser.expression();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitExpression(context);

            result.Should().BeGreaterOrEqualTo(3);
        }

        [TestMethod]
        public void SimpleExpressionArithmeticTest()
        {
            DiceParser parser = Setup("3d6 + 100");
            DiceParser.ExpressionContext context = parser.expression();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitExpression(context);

            result.Should().BeGreaterOrEqualTo(103);
        }

        [TestMethod]
        public void ComplexExpressionTest()
        {
            DiceParser parser = Setup("-100 + 0d100 + 2d6 + 100");
            DiceParser.ExpressionContext context = parser.expression();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitExpression(context);

            result.Should().BeGreaterOrEqualTo(2);
            result.Should().BeLessOrEqualTo(12);
        }

        [TestMethod]
        public void SignlessExpressionTest()
        {
            DiceParser parser = Setup("1 2 3 4");
            DiceParser.ExpressionContext context = parser.expression();
            DiceVisitor visitor = new DiceVisitor();

            int result = (int)visitor.VisitExpression(context);

            result.Should().Be(10);
        }

        private static DiceParser Setup(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DiceLexer diceLexer = new DiceLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(diceLexer);
            DiceParser diceParser = new DiceParser(commonTokenStream);
            return diceParser;
        }
    }
}
