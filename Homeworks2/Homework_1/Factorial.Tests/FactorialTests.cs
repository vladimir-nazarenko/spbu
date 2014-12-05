using System;
using NUnit.Framework;
using System.IO;
using System.Text;
using MyClasses;

namespace Homework_1.Tests
{
    [TestFixture()]
    public class FactorialTests
    {
        [Test()]
        public void Main_ProperInput_Computed()
        {
            StringBuilder output = Parsing.CaptureOutput();            
            string[] args = {"11"};
            Factorial.Main(args);
            string result = output.ToString();
            result = result.ToLower();
            Parsing.ReleaseOutput();
            Assert.AreEqual("39916800\n", result);
        }

        [Test()]
        public void Calculate_NegativeNumber_ReturnsZero()
        {
            Assert.AreEqual(0, Factorial.Calculate(-20));
        }

        [Test()]
        public void Calculate_Zero_Computed()
        {
            Assert.AreEqual(1, Factorial.Calculate(0));
        }

        [Test()]
        public void Calculate_PositiveNumber_Computed()
        {
            Assert.AreEqual(120, Factorial.Calculate(5));
        }
    }
}