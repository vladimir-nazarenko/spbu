using System;
using MyClasses.Data_structures;

namespace Homework_6
{
    public class ExtendedLinkedList<T> : LinkedList<T> where T : IComparable
    {
        public ExtendedLinkedList()
            : base()
        {
        }

        public ExtendedLinkedList(LinkedList<T> list)
        {
            foreach(T item in list)
                InsertFirst(item);
        }

        public void Map(Action<T> performAction)
        {
            foreach(T item in this)
                performAction(item);
        }

        public T Fold(T acc, Func<T, T, T> action)
        {
            ListElement<T> seek = First;
            while (seek != null)
            {
                acc = action(acc, seek.Item);
                seek = seek.next;
            }
            return acc;
        }

        public LinkedList<T> Filter(Func<T, bool> check)
        {
            LinkedList<T> matched = new LinkedList<T>();
            foreach(T item in this)
                if (check(item))
                    matched.InsertFirst(item);
            return matched;
        }
    }
}

