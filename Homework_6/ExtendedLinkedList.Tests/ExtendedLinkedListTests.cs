using System;
using NUnit.Framework;
using Homework_6;

namespace Homework_6
{
    [TestFixture()]
    public class ExtendedLinkedListTests
    {
        [SetUp]
        public void CreateList()
        {
            list = new ExtendedLinkedList<string>();
            list.InsertFirst("first");
            list.InsertFirst("second");
            list.InsertFirst("third");
        }

        [Test]
        public void Map_Success()
        {
            list.Map(ToUpper);
            Assert.AreNotEqual(null, list.Find("abc"));
        }

        private void ToUpper(string item)
        {
            item = "abc";
        }



        private ExtendedLinkedList<string> list;
    }
}

