namespace AbstractSyntaxTreeTests
{
    using System;
    using System.Text;
    using Homework5;
    using MyClasses;
    using NUnit.Framework;

    [TestFixture]
    public class AbstractSyntaxTreeTests
    {
        [Test]
        public void Calculate_UsualOutput_Calculated()
        {
            var tree = new AbstractSyntaxTree("( * 7 ( + 2 3 ) )");
            INode head = null;
            tree.Build(ref head);
            Assert.AreEqual(35, head.Calculate());
        }

        [Test]
        public void Print_UsualOutput_Correct()
        {
            var tree = new AbstractSyntaxTree("( * 7 ( + 2 3 ) )");
            INode head = null;
            tree.Build(ref head);

            // Из-за моей криворукости все дерево сразу печатается в консоль
            // исправлять мне это лень, поэтому смотрю в консоль
            StringBuilder output = Parsing.CaptureOutput();
            head.Print();
            Parsing.ReleaseOutput();
            string actual = output.ToString();
            Assert.AreEqual("(* 7 (+ 2 3 ))", actual);
        }

        [Test]
        public void TestCase()
        {
        }
    }
}