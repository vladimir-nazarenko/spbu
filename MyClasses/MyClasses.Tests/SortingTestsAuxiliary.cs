using System;

namespace MyClasses.MyClassesTests.SortingTests
{
    /// <summary>
    /// Axuiliary methods common for all sorting test clients
    /// </summary>
    public static class SortingTestsAuxiliary
    {
        public enum Modes
        {
            Random,
            Few_Unique,
            All_Equal
        };

        /// <summary>
        /// Generates the array of integer for sorting algorithms.
        /// </summary>
        /// <returns>
        /// The array.
        /// </returns>
        /// <param name='size'>
        /// Number of values in the array.
        /// </param>
        /// <param name='mode'>
        /// Mode of generating.
        /// </param>
        public static int[] GenerateArray(int size, Modes mode)
        {
            int[] values = new int[size];
            Random generator = new Random();
            for (int i = 0; i < size; i++)
            {
                switch (mode)
                {
                    case Modes.Random:
                        values [i] = generator.Next();
                        break;
                    case Modes.Few_Unique:
                        values [i] = generator.Next() % (size / 10);
                        break;
                    case Modes.All_Equal:
                        values [i] = 42;
                        break;
                }
            }
            return values;
        }

        /// <summary>
        /// Exchange the specified integers, that are placed at the 
        /// firstIndex and the secondIndex in the values array.
        /// </summary>
        public static void Exchange(ref int[] values, int firstIndex, int secondIndex)
        {
            int temp = values [firstIndex];
            values [secondIndex] = values [firstIndex];
            values [firstIndex] = temp;
        }

        /// <summary>
        /// Determines whether given array is completely sorted in ascending order.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this array is sorted; otherwise, <c>false</c>.
        /// </returns>
        /// <param name='values'>
        /// Array of integers to check.
        /// </param>
        public static bool IsSorted(ref int[] values)
        {
            for (int i = 1; i < values.Length; i++)
                if (values [i] < values [i - 1]) 
                    return false;
            return true;
        }
    }
}

