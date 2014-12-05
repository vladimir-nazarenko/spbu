using System;
using NUnit.Framework;
using Homework3;
using MyClasses.DataStructures;

namespace Homework3.Tests
{
    [TestFixture()]
    public class StackCalculatorTest
    {
        [Test]
        public void Calculate_UsualExpression_Calculated()
        {
            StackCalculator calc = new StackCalculator("9 6 - 1 2 + *", new LinkedStack<double>());
            Assert.AreEqual(-9, calc.Calculate());
        }
    }
}

