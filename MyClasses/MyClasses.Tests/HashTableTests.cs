using System;
using NUnit.Framework;
using MyClasses.Data_structures;
using MyClasses.SortingAlgorithms;

namespace MyClasses
{
    [TestFixture()]
    public class HashTableTests
    {
        [SetUp]
        public void Init()
        {
            table = new HashTable<string>(10, new FNVHash());
        }

        [Test]
        public void Insert_FewItems_FindReturnsTrue()
        {
            table.Insert("First");
            table.Insert("Second");
            table.Insert("Third");
            Assert.True(table.Exists("First"));
            Assert.True(table.Exists("Second"));
            Assert.True(table.Exists("Third"));
        }

        [Test]
        public void Remove_AfterInsertion_ExistsReturnedFalse()
        {
            table.Insert("First");
            table.Remove("First");
            Assert.False(table.Exists("First"));
        }

        [Test]
        public void Iterator_IterateForeach_Success()
        {
            HashTable<int> tableOfIntegers = new HashTable<int>(80, new FNVHash());
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
                Assert.AreEqual(i, values[i]);
        }

        [Test]
        public void FNVHash_CalculateForString_Calculated()
        {
            FNVHash hashF = new FNVHash();
            Assert.AreEqual(hashF.CalculateHash("test"), hashF.CalculateHash("test"));
        }

        private HashTable<string> table;
    }
}