namespace Homework6
{
    using System;
    using Homework6;
    using NUnit.Framework;

    [TestFixture]
    public class ExtendedLinkedListTests
    {
        private ExtendedLinkedList<string> list;

        [SetUp]
        public void CreateList()
        {
            this.list = new ExtendedLinkedList<string>();
            this.list.InsertFirst("first");
            this.list.InsertFirst("second");
            this.list.InsertFirst("third");
        }

        [Test]
        public void Map_Success()
        {
            var mappedList = this.list.Map(s => s.ToUpper());
            Assert.True(mappedList.Contains("SECOND"));
        }

        [Test]
        public void Fold_Success()
        {
            string concated = string.Empty;
            concated = this.list.Fold(concated, (first, second) => string.Concat(first, second));
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
    }
}
