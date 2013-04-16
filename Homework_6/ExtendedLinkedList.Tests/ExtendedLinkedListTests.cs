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
            var mappedList = list.Map(s => s.ToUpper());
            Assert.True(mappedList.Contains("SECOND"));
        }

        [Test]
        public void Fold_Success()
        {
            string concated = "";
            concated = this.list.Fold(concated, (first, second) => String.Concat(first, second));
            Assert.AreEqual("thirdsecondfirst", concated);
        }

        [Test]
        public void Filter_Success()
        {
            var filteredList = this.list.Filter(s => (s[0] == 'f' || s[0] == 's'));
            Assert.AreEqual(2, filteredList.Count);
            Assert.True(this.list.Contains("first"));
            Assert.True(this.list.Contains("second"));
        }

        private ExtendedLinkedList<string> list;
    }
}
