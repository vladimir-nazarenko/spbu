using System;

namespace MyClasses.SortingAlgorithms
{
    /// <summary>
    /// Sort an array by insertion sort.
    /// </summary>
    public static class InsertionSort<T> where T : IComparable
    {
        /// <summary>
        /// Sort subarray.
        /// </summary>
        /// <param name='values'>
        /// Array to be sorted.
        /// </param>
        /// <param name='lo'>
        /// First element of subarray.
        /// </param>
        /// <param name='hi'>
        /// Last index of subarray.
        /// </param>
        public static void Sort(ref T[] values, int lo, int hi)
        {
            if (hi < lo)
                return;
            for (int i = lo; i < hi; i++)
            {
                int j = i + 1;
                while (j > lo && Less(values[j], values[j - 1]))
                {
                    Exchange(ref values, j - 1, j);
                    j--;
                }
            }                    
        }

        /// <summary>
        /// Perform the sort.
        /// </summary>
        /// <param name='values'>
        /// Array to be sorted.
        /// </param>
        public static void Sort(ref T[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new NullReferenceException();
            } else
            {
                Sort(ref values, 0, values.Length - 1);
            }
        }

        /// <summary>
        /// Exchange the specified integers, that are placed at the 
        /// firstIndex and the secondIndex in the values array.
        /// </summary>
        private static void Exchange(ref T[] values, int firstIndex, int secondIndex)
        {
            T temp = values [firstIndex];
            values [firstIndex] = values [secondIndex];
            values [secondIndex] = temp;
        }

        /// <summary>
        /// Check if the specified first element less than the second.
        /// </summary>
        private static bool Less(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }
    }
}