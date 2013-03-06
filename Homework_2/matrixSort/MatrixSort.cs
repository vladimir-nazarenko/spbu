using System;
using MyClasses;
using MyClasses.SortingAlgorithms;

namespace Homework_2
{
    public class MatrixSort
    {
        public static int Main(string[] args)
        {
            int[][] data = Parsing.ReadIntegerMatrixFromFile("test.txt");
            ComparableRow[] rows = new ComparableRow[data.GetLength(0)];
            for (int i = 0; i < data.GetLength(0); i++)
                rows[i] = new ComparableRow(data[i]);
            QSort<ComparableRow>.Sort(ref rows);
            return 0;
        }

        class ComparableRow : IComparable
        {
            public ComparableRow(int[] data)
            {
                row = data;
            }

            public int CompareTo(Object obj)
            {
                if (obj == null) 
                    return 1;
                ComparableRow another = obj as ComparableRow;
                if (another.row[0] == this.row[0])
                    return 0;
                if (another.row[0] > this.row[0])
                    return 1;
                return -1;
            }

            private int[] row;
        }
    }
}