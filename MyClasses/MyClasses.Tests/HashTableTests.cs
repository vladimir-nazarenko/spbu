namespace MyClasses
{
    using System;
    using MyClasses.DataStructures;
    using MyClasses.SortingAlgorithms;
    using NUnit.Framework;
    [TestFixture]
    public class HashTableTests
    {
        private HashTable<string> table;

        [SetUp]
        public void Init()
        {
            this.table = new HashTable<string>(10, new FNVHash<string>());
        }

        [Test]
        public void Insert_FewItems_FindReturnsTrue()
        {
            this.table.Insert("First");
            this.table.Insert("Second");
            this.table.Insert("Third");
            Assert.True(this.table.Exists("First"));
            Assert.True(this.table.Exists("Second"));
            Assert.True(this.table.Exists("Third"));
        }

        [Test]
        public void Remove_AfterInsertion_ExistsReturnedFalse()
        {
            this.table.Insert("First");
            this.table.Remove("First");
            Assert.False(this.table.Exists("First"));
        }

        [Test]
        public void Iterator_IterateForeach_Success()
        {
            HashTable<int> tableOfIntegers = new HashTable<int>(80, new FNVHash<int>());
            for (int i = 0; i < 100; i++)
            {
                tableOfIntegers.Insert(i);
            }

            int[] values = new int[100];
            int cnt = 0;
            foreach (int item in tableOfIntegers)
            {
                values[cnt++] = item;
            }

            QSort<int>.Sort(ref values);
            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(i, values[i]);
            }
        }

        [Test]
        public void FNVHash_CalculateForString_Calculated()
        {
            var hashF = new FNVHash<string>();
            Assert.AreEqual(hashF.CalculateHash("test"), hashF.CalculateHash("test"));
        }

        [Test]
        public void ChangeHashFunction_RehashDone()
        {
            this.table.Insert("First");
            this.table.Insert("Second");
            this.table.Insert("Third");
            Assert.True(this.table.Exists("First"));
            Assert.True(this.table.Exists("Second"));
            Assert.True(this.table.Exists("Third"));
            this.table.ChangeHashFunction(new Adler32Hash<string>());
            Assert.True(this.table.Exists("First"));
            Assert.True(this.table.Exists("Second"));
            Assert.True(this.table.Exists("Third"));
        }
    }
}