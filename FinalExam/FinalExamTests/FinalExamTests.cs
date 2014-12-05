using System;
using NUnit.Framework;
using FinalExam;

namespace FinalExamTests
{
    [TestFixture]
    public class FinalExamTests
    {
        private string[] alphabetic = new string[3]{"a", "ab", "cd"};

        [Test]
        public void BubbleSort_Sort_Correct()
        {
            string[] data = new string[3]{"a", "cd", "ab"};
            BubbleSort<string>.Sort(ref data, new StringAlphabeticComparer());
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(data[i], alphabetic[i]);
            }
        }
    }
}

