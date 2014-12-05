namespace MyClasses.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MyClasses.DataStructures;

    public class LinkedStack<T> : IStack<T>
    {        
        /// <summary>
        /// The list for storing items.
        /// </summary>
        private MyClasses.DataStructures.LinkedList<T> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.LinkedStack{T}"/> class.
        /// </summary>
        public LinkedStack()
        {
            this.list = new LinkedList<T>();
        }

        /// <summary>
        /// Push the specified item.
        /// </summary>
        /// <param name='item'>
        /// Item to be inserted.
        /// </param>
        public void Push(T item)
        {
            this.list.Add(item);
        }

        /// <summary>
        /// Get last added item without popping it.
        /// </summary>
        /// <returns>Value from the edge of the stack.</returns>
        public T Top()
        {
            return this.list.First.Item;
        }
        
        /// <summary>
        /// Get last added item and remove it from the instance.
        /// </summary>
        /// <returns>Value from the edge of the stack.</returns>
        public T Pop()
        {
            T value = this.list.Retrieve(this.list.First);
            this.list.Remove(this.list.First);
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
            return this.list.Count == 0;
        }
    }
}