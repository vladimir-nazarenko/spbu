namespace FinalExam
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compares strings in alphabetic order.
    /// </summary>
    public class StringAlphabeticComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            return string.Compare(first, second);
        }
    }
}