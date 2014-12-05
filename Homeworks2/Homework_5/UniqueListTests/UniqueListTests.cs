namespace UniqueListTests
{
    using System;
    using Homework5;
    using NUnit.Framework;

    [TestFixture]
    public class UniqueListTests
    {
        [ExpectedException(typeof(RepeatingElementException))]
        [Test]
        public void Insert_EqualElements_ExceptionRaised()
        {
            var list = new UniqueList<int>();
            list.Add(15);
            list.Add(15);
        }

        [Test]
        public void Insert_BunchOfvalues_Success()
        {
            var list = new UniqueList<int>();
            list.Add(15);
            list.Add(16);
            Assert.IsTrue(list.Contains(15));
            Assert.IsTrue(list.Contains(16));
        }
    }
}