using System;
using MyClasses.Data_structures;
using System.Collections.Generic;
using System.Collections;

namespace MyClasses.Data_structures
{
    public class LinkedStack<T> : IStack<T>
    {
        public LinkedStack()
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
            T value = list.First.Item;
            list.Remove(list.First);
            return value;
        }

        public bool IsEmpty()
        {
            return list.Length == 0;
        }

        private LinkedList<T> list;       
}