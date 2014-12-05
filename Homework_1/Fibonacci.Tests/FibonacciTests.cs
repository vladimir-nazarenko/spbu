using System;
using NUnit.Framework;
using System.Text;
using MyClasses;
using Homework_1;

namespace Homework_1.Tests
{
    [TestFixture()]
    public class FibonacciTests
    {
        [Test()]
        public void Main_CorrectInput_Computed()
        {
            StringBuilder output = Parsing.CaptureOutput();
            string[] args = {"5"};
            Homework_1.Fibonacci.Main(args);
            Parsing.ReleaseOutput();
            Assert.AreEqual("5\n", output.ToString()); 
        }

        [Test()]
        public void Calculate_NegativeValue_MinusOneReturned()
        {
            Assert.AreEqual(-1, Fibonacci.Calculate(-99)); 
        }

        [Test()]
        public void Calculate_PositiveValue_Computed()
        {
            Assert.AreEqual(8, Fibonacci.Calculate(6)); 
        }
    }
}

