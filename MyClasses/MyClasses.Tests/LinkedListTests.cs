namespace MyClasses
{
    using System;
    using System.Collections.Generic;
    using MyClasses.Data_structures;
    using NUnit.Framework;

    [TestFixture]
    public class LinkedListTests
    {
        private MyClasses.Data_structures.LinkedList<int> list;

        public LinkedListTests()
        {
            this.list = new Data_structures.LinkedList<int>();
        }

        [SetUp] 
        public void Prepare()
        {
            this.list = new Data_structures.LinkedList<int>();
            this.list.InsertFirst(5);
            this.list.InsertFirst(6);
            this.list.InsertFirst(7);
        }

        [Test]
        public void InsertFirst_FewElements_Proccessed()
        {
            Assert.AreEqual(7, this.list.First.Item);
            Assert.AreEqual(6, this.list.First.Next.Item);
            Assert.AreEqual(5, this.list.First.Next.Next.Item);
            Assert.AreEqual(3, this.list.Count);
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Remove_FewElements_Processed()
        {
            this.list.Remove(this.list.First);
            this.list.Remove(this.list.First);
            this.list.Remove(this.list.First);
            this.list.Remove(this.list.First);
        }

        [Test]
        public void Find_AddAndFind_Found()
        {
            Assert.AreEqual(null, this.list.Find(9));
            Assert.AreEqual(6, this.list.Find(6).Item);
        }

        [Test]
        public void Iterator_IterateForeach_Success()
        {
            int i = 7;
            foreach (int item in this.list)
            {
                Assert.AreEqual(i, item);
                i--;
            }

            Assert.AreEqual(4, i);
        }
    }
}