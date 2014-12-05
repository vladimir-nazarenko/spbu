namespace FinalExam
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bubble without optimizations.
    /// </summary>
    public static class BubbleSort<T>
    {
        /// <summary>
        /// Sort the specified values via comparer.
        /// </summary>
        /// <param name='values'>
        /// Values to be sorted.
        /// </param>
        /// <param name='comparer'>
        /// Comparer realization.
        /// </param>
        public static void Sort(ref T[] values, IComparer<T> comparer)
        {
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length - 1; j++)
                {
                    if (comparer.Compare(values[j], values[j + 1]) == 1)
                    {
                        BubbleSort<T>.Exchange(ref values, j, j + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Exchange the specified values, firstIndex and secondIndex.
        /// </summary>
        /// <param name='values'>
        /// Array with values.
        /// </param>
        /// <param name='firstIndex'>
        /// Index of the second value.
        /// </param>
        /// <param name='secondIndex'>
        /// Index of the first value.
        /// </param>
        private static void Exchange(ref T[] values, int firstIndex, int secondIndex)
        {
            T temp = values[firstIndex];
            values[firstIndex] = values[secondIndex];
            values[secondIndex] = temp;
        }
    }
}

