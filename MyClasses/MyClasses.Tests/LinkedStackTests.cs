using System;
using NUnit.Framework;
using MyClasses.Data_structures;
using System.Collections.Generic;

namespace MyClasses
{
    [TestFixture()]
    public class LinkedStackTests
    {
        public LinkedStackTests()
        {
            stack = new LinkedStack<int>();
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Pop_FromEmpty_ExceptionThrown()
        {
            stack.Pop();
        }

        [Test]
        public void Push_PopCooperated_Success()
        {
            stack = new LinkedStack<int>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(i, stack.Top());
                Assert.AreEqual(i, stack.Pop());
            }
        }


        private MyClasses.Data_structures.IStack<int> stack;
    }
}

