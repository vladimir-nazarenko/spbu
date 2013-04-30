namespace MyClasses.SortingTests
{
    using System;
    using MyClasses.SortingAlgorithms;
    using NUnit.Framework;

    [TestFixture]
    public class InsertionSortTests
    {
        [Test]
        public void Sort_OneItem_Sorted()
        {
            int[] data = SortingTestsAuxiliary.GenerateArray(1, SortingTestsAuxiliary.Modes.Random);
            int[] backup = data;
            InsertionSort<int>.Sort(ref data);
            Assert.AreEqual(backup[0], data[0]);
        }

        [Test]
        [ExpectedException(typeof(Exceptions.EmptyArrayException))]
        public void Sort_NullArray_ExceptionThrown()
        {
            int[] data = null;
            InsertionSort<int>.Sort(ref data);
        }

        [Test]
        [ExpectedException(typeof(Exceptions.EmptyArrayException))]
        public void Sort_EmptyArray_ExceptionThrown()
        {
            int[] data = new int[0];
            InsertionSort<int>.Sort(ref data);
        }

        [Test]
        public void Sort_RandomInput_Sorted()
        {
            int[] data = SortingTestsAuxiliary.GenerateArray(100, SortingTestsAuxiliary.Modes.Random);
            Assert.AreEqual(false, SortingTestsAuxiliary.IsSorted(ref data));
            InsertionSort<int>.Sort(ref data);
            Assert.AreEqual(true, SortingTestsAuxiliary.IsSorted(ref data));
        }

        [Test]
        public void Sort_FewUnique_Sorted()
        {
            int[] data = SortingTestsAuxiliary.GenerateArray(100, SortingTestsAuxiliary.Modes.Few_Unique);
            Assert.AreEqual(false, SortingTestsAuxiliary.IsSorted(ref data));
            InsertionSort<int>.Sort(ref data);
            Assert.AreEqual(true, SortingTestsAuxiliary.IsSorted(ref data));
        }

        [Test]
        public void Sort_AllEqual_Sorted()
        {
            int[] data = SortingTestsAuxiliary.GenerateArray(100, SortingTestsAuxiliary.Modes.All_Equal);
            InsertionSort<int>.Sort(ref data);
            Assert.AreEqual(true, SortingTestsAuxiliary.IsSorted(ref data));
        }
    }
}