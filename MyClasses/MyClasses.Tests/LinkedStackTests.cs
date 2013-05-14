namespace MyClasses
{
    using System;
    using System.Collections.Generic;
    using MyClasses.DataStructures;
    using NUnit.Framework;

    [TestFixture]
    public class LinkedStackTests
    {
        private MyClasses.DataStructures.IStack<int> stack;

        public LinkedStackTests()
        {
            this.stack = new LinkedStack<int>();
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Pop_FromEmpty_ExceptionThrown()
        {
            this.stack.Pop();
        }

        [Test]
        public void Push_PopCooperated_Success()
        {
            this.stack = new LinkedStack<int>();
            for (int i = 0; i < 10; i++)
            {
                this.stack.Push(i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(i, this.stack.Top());
                Assert.AreEqual(i, this.stack.Pop());
            }
        }

        [Test]
        public void Pop_FromStackOfDouble_Correct()
        {
            var doubleStack = new LinkedStack<double>();
            doubleStack.Push(88.7);
            Assert.AreEqual(88.7, doubleStack.Top());
            Assert.AreEqual(88.7, doubleStack.Pop());
        }

        [Test]
        public void Push_PopCooperatedStackOfDouble_Success()
        {
            var doubleStack = new LinkedStack<double>();
            for (double i = 0; i < 1.0; i += 0.1)
            {
                doubleStack.Push(i);
            }

            for (double i = 1.0; i >= 0.0; i -= 0.1)
            {
                Assert.AreEqual(i, doubleStack.Top(), 0.0001);
                Assert.AreEqual(i, doubleStack.Pop(), 0.0001);
            }
        }
    }
}
