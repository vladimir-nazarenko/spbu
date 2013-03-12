using System;
using NUnit.Framework;
using MyClasses.Data_structures;

namespace MyClasses.Tests
{
    [TestFixture()]
    public class LinkedListTests
    {

        public LinkedListTests()
        {
            list = new Data_structures.LinkedList<int>();
        }

        [SetUp] 
        public void Prepare()
        {
            list = new Data_structures.LinkedList<int>();
            list.InsertFirst(5);
            list.InsertFirst(6);
            list.InsertFirst(7);
        }

        [Test]
        public void InsertFirst_FewElements_Proccessed()
        {
            Assert.AreEqual(7, this.list.First.Item);
            Assert.AreEqual(6, this.list.First.next.Item);
            Assert.AreEqual(5, this.list.First.next.next.Item);
            Assert.AreEqual(3, this.list.Length);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Remove_FewElements_Processed()
        {
            list.Remove(list.First);
            list.Remove(list.First);
            list.Remove(list.First);
            list.Remove(list.First);
        }

        [Test]
        public void Find_AddAndFind_Found()
        {
            Assert.AreEqual(6, this.list.Find(6).Item);
            Assert.AreEqual(null, this.list.Find(9));
        }

        LinkedList<int> list;
    }
}

