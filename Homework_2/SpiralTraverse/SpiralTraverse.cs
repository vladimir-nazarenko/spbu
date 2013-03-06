using System;
using MyClasses;
using System.IO;

namespace Homework_2
{
    class SpiralTraverse
    {
        public static void Main(string[] args)
        {
            SpiralTraverse t = new SpiralTraverse(args [0]);
            t.Traverse();

        }

        public SpiralTraverse(string path)
        {
            data = Parsing.ReadIntegerMatrixFromFile(path);
        }

        public int[] Traverse()
        {

            int level = 1;
            int columnNumber = data.GetLength(1);
            int rowNumber = data.GetLength(0);
            int[] values = new int[columnNumber * rowNumber];
            int i = rowNumber / 2;
            int j = columnNumber / 2;
            int writeIndex = 0;
            values [writeIndex++] = data [i][j];
            while (!(i == 0 && j == 0))
            {
                i--;
                j--;
                level++; 
                int border = j + level;
                for (j++; j <= border; j++)
                    values [writeIndex++] = data [i][j];
                border = i + level;
                j--;
                for (i++; i <= border; i++)
                    values [writeIndex++] = data [i][j];
                border = j - level;
                i--;
                for (j--; j >= border; j--)
                    values [writeIndex++] = data [i][j];
                border = i - level;
                j++;
                for (i--; i >= border; i--)
                    values [writeIndex++] = data [i][j];
                i++;
            }
            return values;
        }

        private int[][] data;
    }
}