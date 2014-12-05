using System;
using MyClasses;
using System.IO;

namespace Homework_2
{
    public class SpiralTraverse
    {
        public static void Main(string[] args)
        {
            SpiralTraverse t = new SpiralTraverse(args[0]);
            int[] values = t.Traverse();
            for (int i = 0; i < values.Length; i++)
                Console.Write(String.Format("{0} ", values[i]));
        }

        public SpiralTraverse(string path)
        {
            data = Parsing.ReadIntegerMatrixFromFile(path);
        }

        public int[] Traverse()
        {
            int level = 1;
            int columnNumber = data.Length;
            int rowNumber = data[0].Length;
            int[] values = new int[columnNumber * rowNumber];
            int i = rowNumber / 2;
            int j = columnNumber / 2;
            int writeIndex = 0;
            values[writeIndex++] = data[i][j];
            while (!(i == 0 && j == 0))
            {
                i--;
                j--;
                level++; 
                int border = j + level;
                for (j++; j <= border; j++)
                    values[writeIndex++] = data[i][j];
                border = i + level;
                j--;
                for (i++; i <= border; i++)
                    values[writeIndex++] = data[i][j];
                border = j - level;
                i--;
                for (j--; j >= border; j--)
                    values[writeIndex++] = data[i][j];
                border = i - level;
                j++;
                for (i--; i >= border; i--)
                    values[writeIndex++] = data[i][j];
                i++;
            }
            return values;
        }

        private int[][] data;
    }
}