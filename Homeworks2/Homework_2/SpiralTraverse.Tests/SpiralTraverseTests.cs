using System;
using NUnit.Framework;
using Homework_2;
using MyClasses;

namespace Homework_2.Tests
{
    [TestFixture()]
    public class SpiralTraverseTests
    {
        [Test]
        public void Traverse_TinyMatrix_Traversed()
        {
            int[][] data = GenerateArray();
            Parsing.WriteIntegerMatrixToFile("matrix.txt", ref data);
            var tr = new SpiralTraverse("matrix.txt");
            int[] values = tr.Traverse();
            for (int i = 0; i < values.Length; i++)
                Assert.AreEqual(i + 1, values[i]);
        }

        private int[][] GenerateArray()
        {
            int[][] values = new int[][]
            {
                new int[3]{9, 2, 3},
                new int[3]{8, 1, 4},
                new int[3]{7, 6, 5}
            };
            return values;
        }
    }
}