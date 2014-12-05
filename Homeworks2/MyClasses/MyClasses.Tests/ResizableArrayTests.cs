namespace MyClasses
{
    using System;
    using MyClasses.DataStructures;
    using NUnit.Framework;

    [TestFixture]
    public class ResizableArrayTests
    {
        private ResizableArray<int> shrinkable;
        private ResizableArray<int> steady;

        [SetUp]
        public void Init()
        {
            this.shrinkable = new ResizableArray<int>(true);
            this.steady = new ResizableArray<int>(false);
        }

        [Test]
        public void Insert_ManyIems_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Insert(i);
            }
        }

        [Test]
        public void Remove_AfterInsertions_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Insert(i);
            }

            for (int i = 0; i < 99; i++)
            {
                this.shrinkable.Remove(i);
            }

            Assert.AreNotEqual(-1, this.shrinkable.Find(99));
            Assert.AreEqual(-1, this.shrinkable.Find(98));
        }

        [Test]
        public void Insert_InCleared_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Insert(i);
            }

            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Remove(i);
            }

            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Insert(i);
            }

            for (int i = 0; i < 99; i++)
            {
                this.shrinkable.Remove(i);
            }

            Assert.AreNotEqual(-1, this.shrinkable.Find(99));
            Assert.AreEqual(-1, this.shrinkable.Find(98));
        }

        [Test]
        public void Iterator_IterateForeach_Success()
        {
            for (int i = 0; i < 100; i++)
            {
                this.shrinkable.Insert(i);
            }

            int cnt = 99;
            foreach (int value in this.shrinkable)
            {
                Assert.AreEqual(cnt--, value);
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void RemoveByKey_FromPacked_ExceptionThrown()
        {
            this.shrinkable.RemoveByKey(5);
        }

        [Test]
        public void RemoveByKey_AfterUsualInsert_FindReturnedFalse()
        {
            for (int i = 0; i < 100; i++)
            {
                this.steady.Insert(i);
            }

            int key = this.steady.Insert(189);
            Assert.AreEqual(key, this.steady.Find(189));
            this.steady.RemoveByKey(key);
            Assert.AreEqual(-1, this.steady.Find(189));
        }
    }
}