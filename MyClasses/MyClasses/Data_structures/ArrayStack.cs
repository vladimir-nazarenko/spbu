using System;
using MyClasses.Data_structures;
using System.Collections.Generic;
using System.Collections;

namespace MyClasses.Data_structures
{
    public class ArrayStack<T> : IStack<T>
    {
        public ArrayStack()
        {
            array = new ResizableArray<T>(false);
        }

        /// <summary>
        /// Push the specified item.
        /// </summary>
        /// <param name='item'>
        /// Item.
        /// </param>
        public void Push(T item)
        {
            array.Insert(item);
        }

        /// <summary>
        /// Get last added item without popping it.
        /// </summary>
        public T Top()
        {
            IEnumerator<T> e = array.GetEnumerator();
            e.MoveNext();
            return e.Current;
        }

        /// <summary>
        /// Get last added item.
        /// </summary>
        public T Pop()
        {
            if (IsEmpty())
                throw new KeyNotFoundException("List is empty");
            IEnumerator<int> e = array.KeysEnumerator();
            e.MoveNext();
            T value = array.RetrieveByKey(e.Current);
            array.RemoveByKey(e.Current);
            array.Trim();
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
            return array.Size == 0;
        }

        private ResizableArray<T> array;
    }
}



