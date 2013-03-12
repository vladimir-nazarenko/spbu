using System;
using MyClasses;
using MyClasses.SortingAlgorithms;

namespace Homework_2
{
    public static class MatrixSort
    {
        public static int Main(string[] args)
        {
            int[][] data = Parsing.ReadIntegerMatrixFromFile(args[0]);
            ComparableRow[] rows = new ComparableRow[data.Length];
            for (int i = 0; i < data.GetLength(0); i++)
                rows[i] = new ComparableRow(data[i]);
            QSort<ComparableRow>.Sort(ref rows); 
            for (int i = 0; i < 20; i++)
            {
                data[i] = rows[i].ToArray();
            }
            Parsing.WriteIntegerMatrixToFile(args[1], ref data);
            return 0;
        }

        public class ComparableRow : IComparable
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
                if (another == null)
                    return 1;
                if (another.Length != this.Length)
                    throw new ArgumentException();
                if (this.row[0] == another.row[0])
                    return 0;
                if (this.row[0] > another.row[0])
                    return 1;
                return -1;
            }

            public int[] ToArray()
            {
                return row;
            }

            public int Length{ get { return this.row.Length; } }

            private int[] row;
        }
    }
}