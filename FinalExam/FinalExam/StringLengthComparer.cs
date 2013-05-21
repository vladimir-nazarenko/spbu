namespace FinalExam
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Compares strings by their lengths.
    /// </summary>
    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            if (first.Length == second.Length)
            {
                return 0;
            } 
            else
            {
                if (first.Length > second.Length)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}