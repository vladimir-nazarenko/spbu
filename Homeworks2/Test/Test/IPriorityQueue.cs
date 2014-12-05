using System;

namespace PQueue
{
    public interface IPriorityQueue<T>
    {
        void Enqueue(T item, int priority);
        T Dequeue();
        bool IsEmpty();
    }
}

