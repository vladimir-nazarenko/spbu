namespace MyClasses.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MyClasses.DataStructures;

    public class ArrayStack<T> : IStack<T>
    {
        /// <summary>
        /// The array for storing items.
        /// </summary>
        private ResizableArray<T> array;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.ArrayStack{T}"/> class.
        /// </summary>
        public ArrayStack()
        {
            this.array = new ResizableArray<T>(false);
        }

        /// <summary>
        /// Push the specified item.
        /// </summary>
        /// <param name='item'>
        /// Item to push.
        /// </param>
        public void Push(T item)
        {
            this.array.Insert(item);
        }

        /// <summary>
        /// Get last added item without popping it out.
        /// </summary>
        /// <returns>Value from the edge of the stack.</returns> 
        public T Top()
        {
            IEnumerator<T> e = this.array.GetEnumerator();
            e.MoveNext();
            return e.Current;
        }

        /// <summary>
        /// Get last added item and remove it from stack.
        /// </summary>
        /// <returns>Value from the edge of the stack.</returns>
        public T Pop()
        {
            if (this.IsEmpty())
            {
                throw new KeyNotFoundException("List is empty");
            }

            IEnumerator<int> e = this.array.KeysEnumerator();
            e.MoveNext();
            T value = this.array.RetrieveByKey(e.Current);
            this.array.RemoveByKey(e.Current);
            this.array.Trim();
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
            return this.array.Size == 0;
        }
    }
}