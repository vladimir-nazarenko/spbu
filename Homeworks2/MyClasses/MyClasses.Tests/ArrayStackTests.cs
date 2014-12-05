namespace MyClasses
{
    using System;
    using System.Collections.Generic;
    using MyClasses.DataStructures;
    using NUnit.Framework;

    [TestFixture]
    public class ArrayStackTests
    {
        private MyClasses.DataStructures.IStack<int> stack;

        public ArrayStackTests()
        {
            this.stack = new ArrayStack<int>();
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
            this.stack = new ArrayStack<int>();
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
    }
}