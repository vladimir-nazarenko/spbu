using System;
using NUnit.Framework;
using MyClasses.Data_structures;

namespace MyClasses
{
    [TestFixture()]
    public class ResizableArrayTests
    {
        [SetUp]
        public void Init()
        {
            shrinkable = new ResizableArray<int>(true);
            steady = new ResizableArray<int>(false);
        }

        [Test]
        public void Insert_ManyIems_Success()
        {
            for (int i = 0; i < 100; i++)
                shrinkable.Insert(i);
        }

        [Test]
        public void Remove_AfterInsertions_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                shrinkable.Insert(i);
            }
            for (int i = 0; i < 99; i++)
            {
                shrinkable.Remove(i);
            }
            Assert.AreNotEqual(-1, shrinkable.Find(99));
            Assert.AreEqual(-1, shrinkable.Find(98));
        }

        [Test]
        public void Insert_InCleared_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                shrinkable.Insert(i);
            }
            for (int i = 0; i < 100; i++)
            {
                shrinkable.Remove(i);
            }
            for (int i = 0; i < 100; i++)
            {
                shrinkable.Insert(i);
            }
            for (int i = 0; i < 99; i++)
            {
                shrinkable.Remove(i);
            }
            Assert.AreNotEqual(-1, shrinkable.Find(99));
            Assert.AreEqual(-1, shrinkable.Find(98));
        }

        [Test]
        public void Iterator_IterateForeach_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                shrinkable.Insert(i);
            }
            int cnt = 99;
            foreach (int value in shrinkable)
            {
                Assert.AreEqual(cnt--, value);
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void RemoveByKey_FromPacked_ExceptionThrown()
        {
            shrinkable.RemoveByKey(5);
        }

        [Test]
        public void RemoveByKey_AfterUsualInsert_FindReturnedFalse()
        {
            for (int i = 0; i < 100; i++)
            {
                steady.Insert(i);
            }
            int key = steady.Insert(189);
            Assert.AreEqual(key, steady.Find(189));
            steady.RemoveByKey(key);
            Assert.AreEqual(-1, steady.Find(189));
        }

        private ResizableArray<int> shrinkable;
        private ResizableArray<int> steady;
    }
}