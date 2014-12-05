namespace FinalExam
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compares strings in reversed alphabetic order.
    /// </summary>
    public class StringReverseComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            return -string.Compare(first, second);
        }
    }
}