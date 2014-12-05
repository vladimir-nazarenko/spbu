namespace Homework8Tests
{
    using System;
    using Homework8;
    using NUnit.Framework;

    [TestFixture]
    public class SetTests
    {
        Set<int> firstSet;
        Set<int> secondSet;
        Set<int> unitedSet;
        Set<int> intersectedSet;

        [SetUp]
        public void Initialize()
        {
            firstSet = new Set<int>(){25, 63, 98};
            secondSet = new Set<int>(){35, 25, 98, 42};
            unitedSet = new Set<int>(){25, 98, 63, 35, 42};
            intersectedSet = new Set<int>(){25, 98};
        }

        [Test]
        public void Add_BunchOfAddOperationsWithContainsChecks_Success()
        {
            firstSet.Add(15);
            firstSet.Add(89);
            Assert.IsTrue(firstSet.Contains(15));
            Assert.IsTrue(firstSet.Contains(89));
            Assert.IsFalse(firstSet.Contains(49));
            firstSet.Add(15);
        }

        [Test]
        public void Unite_TwoSets_Correct()
        {
            Assert.IsTrue(unitedSet.Equals(firstSet.Unite(secondSet)));
        }

        [Test]
        public void Intersect_TwoSets_Correct()
        {
            Assert.IsTrue(intersectedSet.Equals(firstSet.Intersect(secondSet)));
        }

        [Test] 
        public void Equals_ReflexityTest_Success()
        {
            Assert.IsTrue(firstSet.Equals(firstSet));
        }

        [Test] 
        public void Equals_NonEqualityTest_Success()
        {
            Assert.IsFalse(firstSet.Equals(secondSet));
        }
    }
}

