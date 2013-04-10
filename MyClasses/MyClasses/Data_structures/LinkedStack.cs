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

        /// <summary>
        /// Push the specified item.
        /// </summary>
        /// <param name='item'>
        /// Item.
        /// </param>
        public void Push(T item)
        {
            list.InsertFirst(item);
        }

        /// <summary>
        /// Get last added item without popping it.
        /// </summary>
        public T Top()
        {
            return list.First.Item;
        }
        
        /// <summary>
        /// Get last added item.
        /// </summary>
        public T Pop()
        {
            T value = list.First.Item;
            list.Remove(list.First);
            return value;
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        private LinkedList<T> list;       
    }
}