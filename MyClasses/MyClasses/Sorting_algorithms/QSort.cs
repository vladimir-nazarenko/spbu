namespace MyClasses.SortingAlgorithms
{
    using System;

    public static class QSort<T> where T : IComparable
    {
        /// <summary>
        /// If the array is smaller, it would be sorted by insertion sort
        /// </summary>
        private const int CutOff = 10;

        /// <summary>
        /// Sort the array
        /// </summary>
        /// <param name="values">
        /// Values to be sorted.
        /// </param>
        public static void Sort(ref T[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new Exceptions.EmptyArrayException();
            } 
            else
            {
                Shuffle(ref values);
                Sort(ref values, 0, values.Length - 1);
            }
        }

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
        private static void Sort(ref T[] values, int lo, int hi)
        {
            if (hi <= lo)
            {
                return;
            }

            if (hi - lo < CutOff)
            {
                InsertionSort<T>.Sort(ref values, lo, hi);
                return;
            }

            // Invariant: elements between lt and gt are equal to pivot
            int lt = lo;
            int gt = hi;

            // Choose pivot
            int mid = (lo + hi) / 2;
            if (Less(values[mid], values[lo]))
            {
                Exchange(ref values, mid, lo);
            }

            if (Less(values[hi], values[mid]))
            {
                Exchange(ref values, hi, mid);
            }

            if (Less(values[hi], values[lo]))
            {
                Exchange(ref values, hi, lo);
            }

            T pivot = values[mid];

            // Reaching invariant statet above
            int i = lo;
            while (i <= gt)
            {
                int cmp = values[i].CompareTo(pivot);
                if (cmp < 0)
                {
                    Exchange(ref values, lt++, i++);
                }
                else 
                {
                    if (cmp > 0)
                    {
                    Exchange(ref values, gt--, i);
                    }
                    else
                    {
                    i++;
                    }
                }
            }

            // Recursevly sort left and right part
            Sort(ref values, lo, lt - 1);
            Sort(ref values, gt + 1, hi);
        }

        private static void Exchange(ref T[] values, int firstIndex, int secondIndex)
        {
            T temp = values[firstIndex];
            values[firstIndex] = values[secondIndex];
            values[secondIndex] = temp;
        }

        // Randomly shuffle the array to obtain perfomance guarantee.
        private static void Shuffle(ref T[] values)
        {
            var generator = new Random();
            for (int i = 0; i < values.Length; i++)
            {
                int r = generator.Next(0, i + 1);
                Exchange(ref values, i, r);
            }
        }

        private static bool Less(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }
    }
}