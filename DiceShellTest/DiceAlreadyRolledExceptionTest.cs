using DiceShell;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiceShellTest
{
    [TestClass]
    public class DiceRolledExceptionTest
    {
        [TestMethod]
        public void ExceptionMessageTest()
        {
            Exception e = new DiceAlreadyRolledException(10);

            e.Message.Should().Be("Dice has already been rolled with value 10");
        }
    }
}
