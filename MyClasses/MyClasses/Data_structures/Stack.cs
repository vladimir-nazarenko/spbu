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
            //list.InsertFirst(15);
        }

        public T Top()
        {
            return default(T);//list.First.Item;
        }

        public T Pop()
        {
            T value =  list.First.Item;
            return value;
        }

        private LinkedList<T> list;
    }
}

