using System;
using NUnit.Framework;
using Homework_2;
using MyClasses.MyClassesTests;
using MyClasses;
using MyClasses.SortingAlgorithms;

namespace Homework_2.Tests
{
    [TestFixture()]
    public class MatrixSortTests
    {
        public MatrixSortTests()
        {
            int[] temp = new int[5];

            rows = new MatrixSort.ComparableRow[20];
            for (int i = 20; i > 0; i--)
            {
                temp = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    temp[j] = i - j;
                }
                rows[20 - i] = new MatrixSort.ComparableRow(temp);


            }

            //put in some test
            int[][] matrix = new int[20][];
            for (int i = 0; i < 20; i++)
            {
                matrix [i] = rows[i].ToArray();
            }

            Parsing.WriteIntegerMatrixToFile("unsorted.txt", ref matrix);
            QSort<MatrixSort.ComparableRow>.Sort(ref rows);
            matrix = new int[20][];
            for (int i = 0; i < 20; i++)
            {
                matrix [i] = rows[i].ToArray();
            }
            Parsing.WriteIntegerMatrixToFile("sorted.txt", ref matrix);
        }

        [Test()]
        public void ComparableRowCompareTo_CompareTwoRows_CorrectResult()
        {
            Assert.AreEqual(1, rows[0].CompareTo(rows[1]));
            Assert.AreEqual(-1, rows[1].CompareTo(rows[0]));
            Assert.AreEqual(0, rows[1].CompareTo(rows[1]));
        }

        private MatrixSort.ComparableRow[] rows;
    }
}
