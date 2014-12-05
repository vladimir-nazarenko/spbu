namespace MyClasses.DataStructures
{
    using System;
    using System.Collections.Generic;

    public interface IStack<T>
    {
        void Push(T item);

        T Top();

        T Pop();

        bool IsEmpty();
    }
}
