namespace MyClasses
{
    using System;
    using System.Text;

    using NUnit.Framework;
    [TestFixture]
    public class ParsingTests
    {   
        [Test]
        public void ParseOneIntegerFromConsoleArgs_EmptyInput_ExceptionHandled()
        {
            string[] zeroArgs = null;
            Parsing.ParseOneIntegerFromConsoleArgs(zeroArgs);
            Assert.AreEqual("no input at all", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_EmptyArgsArray_ExceptionHandled()
        {
            string[] args = { string.Empty };
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_StringInsteadOfNumber_ExceptionHandled()
        {
            string[] args = { "123some string123" };
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_ProperInput_GotInteger()
        {
            string[] args = { "-123456" };
            int result = Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual(-123456, result);
            Assert.AreEqual("none", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_RealNumber_ExceptionHandled()
        {
            string[] args = { "999.15" };
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("wrong input", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_TooLongNumber_ExceptionHandled()
        {
            string hundredLenthValue = new string('1', 100);
            string[] args = { hundredLenthValue };
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("value is too large", Parsing.LastErrorMessage);
        }

        [Test]
        public void ParseOneIntegerFromConsoleArgs_CorrectInputAfterIncorrect_GotInteger()
        {
            string hundredLenthValue = new string('1', 100);
            string[] args = { hundredLenthValue };
            Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual("value is too large", Parsing.LastErrorMessage);
            args[0] = "-123456";
            int result = Parsing.ParseOneIntegerFromConsoleArgs(args);
            Assert.AreEqual(-123456, result);
            Assert.AreEqual("none", Parsing.LastErrorMessage);
        }

        [Test]
        public void CaptureOutput_GetOutput_Succes()
        {
            StringBuilder sb = Parsing.CaptureOutput();
            Console.Write("test");
            Parsing.ReleaseOutput();
            Assert.AreEqual("test", sb.ToString());
        }

        [Test]
        public void ReleaseOutput_GetOutputAndSetItBack_Success()
        {
            // Capture
            StringBuilder sb = Parsing.CaptureOutput();

            // Release
            Parsing.ReleaseOutput();

            // Check if released
            Console.Write("test");
            Assert.AreEqual(string.Empty, sb.ToString());
        }

        [Test]
        public void WriteInegerMatrix_ReadIntegerMatrix_Success()
        {
            // Generate matrix
            int[][] matrix = new int[20][];
            for (int i = 19; i >= 0; i--)
            {
                matrix[i] = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    matrix[i][j] = i - j;
                }
            }

            // Write matrix, read it and compare
            Parsing.WriteIntegerMatrixToFile("test.txt", matrix);
            int[][] readMatrix = Parsing.ReadIntegerMatrixFromFile("test.txt");
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(matrix[i][j], readMatrix[i][j]);
                }
            }
        }
    }
}