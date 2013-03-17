using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public interface IStack<T> : IEnumerable<T> where T : IComparable
    {
        void Push(T item);
        T Top();
        T Pop();
        bool IsEmpty();
    }
}

