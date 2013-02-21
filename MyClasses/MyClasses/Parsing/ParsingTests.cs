using System;
using NUnit.Framework;
using System.Text;

namespace MyClasses.MyClassesTests
{
    /// <summary>
    /// NUnit tests for <see cref="Parsing"/>
    /// </summary>
    [TestFixture()]
    public class ParsingTests
    {   
        [Test()]
        public void ParseOneIntegerFromConsoleArgs_EmptyInput_ExceptionHandled()
        {
            string[] zeroArgs = null;
            Parsing.ParseOneIntegerFromConsoleArgs(zeroArgs);
            Assert.AreEqual("no input at all", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_EmptyArgsArray_ExceptionHandled()
        {
            string[] args = {""};
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_StringInsteadOfNumber_ExceptionHandled()
        {
            string[] args = {"123some string123"};
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_ProperInput_GotInteger()
        {
            string[] args = {"-123456"};
            int result = Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual(-123456, result);
            Assert.AreEqual("none", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_RealNumber_ExceptionHandled()
        {
            string[] args = {"999.15"};
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_TooLongNumber_ExceptionHandled()
        {
            string hundredLenthValue = new String('1', 100);
            string[] args = {hundredLenthValue};
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("value is too large", Parsing.LastErrorMessage);
        }

        [Test()]
        public void ParseOneIntegerFromConsoleArgs_CorrectInputAfterIncorrect_GotInteger()
        {
            string hundredLenthValue = new String('1', 100);
            string[] args = {hundredLenthValue};
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("value is too large", Parsing.LastErrorMessage);
            args [0] = "-123456";
            int result = Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual(-123456, result);
            Assert.AreEqual("none", Parsing.LastErrorMessage);
        }

        [Test()]
        public void CaptureOutput_GetOutput_Succes()
        {
            StringBuilder sb = Parsing.CaptureOutput();
            Console.Write("test");
            Parsing.ReleaseOutput();
            Assert.AreEqual("test", sb.ToString());
        }

        [Test()]
        public void ReleaseOutput_GetOutputAndSetItBack_Succes()
        {
            //capture
            StringBuilder sb = Parsing.CaptureOutput();
            //release
            Parsing.ReleaseOutput();
            //check if released
            Console.Write("test");
            Assert.AreEqual("", sb.ToString());
        }
    }
}

