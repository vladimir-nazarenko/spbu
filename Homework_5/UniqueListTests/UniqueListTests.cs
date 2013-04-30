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
    }
}