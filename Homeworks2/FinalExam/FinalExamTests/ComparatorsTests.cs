using System;
using NUnit.Framework;
using FinalExam;

namespace FinalExamTests
{
    [TestFixture]
    public class ComparatorsTests
    {
        
        private StringAlphabeticComparer alpha;
        private StringReverseComparer reverse;
        private StringLengthComparer len;

        public ComparatorsTests()
        {
            alpha = new StringAlphabeticComparer();
            reverse = new StringReverseComparer();
            len = new StringLengthComparer();
        }

        [Test]
        public void StringAlphabeticComparer_Correct()
        {
            Assert.AreEqual(0, alpha.Compare("1", "1"));
            Assert.AreEqual(1, alpha.Compare("b", "a"));
            Assert.AreEqual(-1, alpha.Compare("a", "b"));
            Assert.AreEqual(1, alpha.Compare("ac", "ab"));
        }

        [Test]
        public void StringReverseComparer_Correct()
        {
            Assert.AreEqual(0, reverse.Compare("1", "1"));
            Assert.AreEqual(1, reverse.Compare("a", "b"));
            Assert.AreEqual(-1, reverse.Compare("b", "a"));
        }

        [Test]
        public void StringLengthComparer_Correct()
        {
            Assert.AreEqual(0, len.Compare("1", "1"));
            Assert.AreEqual(1, len.Compare("22", "1"));
            Assert.AreEqual(-1, len.Compare("1", "22"));
        }
    }
}

