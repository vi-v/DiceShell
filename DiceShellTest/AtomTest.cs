﻿using DiceShell;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiceShellTest
{
    [TestClass]
    public class AtomTest
    {
        [TestMethod]
        public void InitializationTest()
        {
            Atom a = new Atom();

            a.IsDiceGroup.Should().BeFalse();
            a.IsModifier.Should().BeFalse();
            a.Sign.Should().Be(AtomSign.Plus);
        }

        [TestMethod]
        public void SetDiceGroupTest()
        {
            DiceGroup dg = FourDEightGroup();
            Atom a = new Atom();

            a.SetDiceGroup(dg);

            a.IsDiceGroup.Should().BeTrue();
            a.IsModifier.Should().BeFalse();
            a.DiceGroupInstance.Should().Be(dg);
        }

        [TestMethod]
        public void SetModifierTest()
        {
            Atom a = new Atom();

            a.SetModifier(4);

            a.IsModifier.Should().BeTrue();
            a.IsDiceGroup.Should().BeFalse();
            a.ModifierInstance.Should().Be(4);
        }

        [TestMethod]
        public void SetNegativeModifierTest()
        {
            Atom a = new Atom();

            a.SetModifier(-4);

            a.IsModifier.Should().BeTrue();
            a.IsDiceGroup.Should().BeFalse();
            a.ModifierInstance.Should().Be(4);
            a.Sign.Should().Be(AtomSign.Minus);
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
    }
}
