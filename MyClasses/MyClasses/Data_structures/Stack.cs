using System;
using MyClasses.Data_structures;

namespace MyClasses
{
    public class Stack<T> where T : IComparable
    {
        public Stack()
        {
            list = new LinkedList<T>();
        }

        public void Push(T item)
        {
            list.InsertFirst(item);
        }

        public T Top()
        {
            return list.First.Item;
        }

        public T Pop()
        {
            T value =  list.First.Item;
            list.Remove(list.First);
            return value;
        }

        private LinkedList<T> list;
    }
}

