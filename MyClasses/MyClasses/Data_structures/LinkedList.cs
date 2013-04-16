namespace MyClasses.Data_structures
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Linked list.
    /// </summary>
    /// <typeparam name="T">
    /// Type of encapsulated value.
    /// </typeparam>
    public class LinkedList<T> : ICollection<T>
    {
        /// <summary>
        /// The head of the list.
        /// </summary>
        private ListElement<T> head;

        /// <summary>
        /// The current size of the list.
        /// </summary>
        private int size;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.Data_structures.LinkedList{T}"/> class.
        /// </summary>
        public LinkedList()
        {
            this.head = new ListElement<T>(default(T));
        }

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Count
        {
            get { return this.size; }
        }

        /// <summary>
        /// Gets the position of a first value.
        /// </summary>
        public ListElement<T> First
        {
            get
            {
                if (this.head.Next == null)
                {
                    throw new KeyNotFoundException("List is empty");
                }

                return this.head.Next;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Inserts value after given List Element.
        /// </summary>
        /// <param name='value'>Value to be inserted.</param>
        /// <param name='position'>Position where to put value.</param>
        public virtual void InsertAfter(T value, ListElement<T> position)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            ListElement<T> oldNext = position.Next;
            position.Next = new ListElement<T>(value);
            position.Next.Next = oldNext;
            this.size++;
        }

        /// <summary>
        /// Inserts value on the first position.
        /// </summary>
        /// <param name='value'>
        /// Value to be inserted.
        /// </param>
        public virtual void InsertFirst(T value)
        {
            this.InsertAfter(value, this.head);
        }

        /// <summary>
        /// Remove the element on specified position.
        /// </summary>
        /// <param name = 'position'>
        /// Position of ewmoving value.
        /// </param>
        public void Remove(ListElement<T> position)
        {
            ListElement<T> temp = this.head;
            while (!temp.Next.Equals(position))
            {
                temp = temp.Next;
            }

            temp.Next = temp.Next.Next;
            this.size--;
        }

        /// <Docs>
        /// The item to remove from the current collection.
        /// </Docs>
        /// <para>
        /// Removes the first occurrence of an item from the current collection.
        /// </para>
        /// <summary>
        /// Remove the specified item.
        /// </summary>
        /// <param name='item'>
        /// If set to <c>true</c> item.
        /// </param>
        /// <returns>Value indicating if there was item to remove</returns>
        public bool Remove(T item)
        {
            ListElement<T> seek = this.Find(item);
            if (seek == null)
            {
                this.Remove(seek);
            }

            return seek == null;
        }

        /// <summary>
        /// Checks existance of a given value.
        /// </summary>
        /// <param name='value'>Value to be looked for.</param>
        /// <returns>Position of found value, otherwise - null</returns>
        public ListElement<T> Find(T value)
        {
            ListElement<T> seek = this.head.Next;
            while (seek != null && !seek.Item.Equals(value))
            {
                seek = seek.Next;
            }

            return seek;
        }

        /// <summary>
        /// Retrieve the specified position.
        /// </summary>
        /// <param name='position'>
        /// Position of a value to be retrieved.
        /// </param>
        /// <returns>Value from given position.</returns>
        public T Retrieve(ListElement<T> position)
        {
            return position.Item;
        }

        /// <Docs>
        /// The item to add to the current collection.
        /// </Docs>
        /// <para>
        /// Adds an item to the current collection.
        /// </para>
        /// <remarks>
        /// To be added.
        /// </remarks>
        /// <exception cref='System.NotSupportedException'>
        /// The current collection is read-only.
        /// </exception>
        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <param name='item'>
        /// Item to be added.
        /// </param>
        public void Add(T item)
        {
            this.InsertFirst(item);
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            this.head.Next = null;
            this.size = 0;
        }

        /// <summary>
        /// Determines whether this instance contains the value.
        /// </summary>
        /// <param name='item'>
        /// If set to <c>true</c> item.
        /// </param>
        /// <returns><c>true</c> if given item was found in instance.</returns>
        public bool Contains(T item)
        {
            bool found = false;
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        /// <summary>
        /// Gets the last element of a list.
        /// </summary>
        /// <returns>Position of the last element.</returns>
        public ListElement<T> GetLast()
        {
            ListElement<T> seek = this.head.Next;
            while (seek != null)
            {
                seek = seek.Next;
            }

            return seek;
        }

        /// <summary>
        /// Copies current instanse to an array.
        /// </summary>
        /// <param name='array'>
        /// Array where to copy.
        /// </param>
        /// <param name='arrayIndex'>
        /// Array index where the copy process will start.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            ListElement<T> seek = this.head.Next;
            for (int i = 0; i < this.Count; i++, seek = seek.Next)
            {
                array[arrayIndex + i] = seek.Item;
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The enumerator from first to the last element.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            ListElement<T> seek = this.head;
            while (seek.Next != null)
            {
                seek = seek.Next;
                yield return seek.Item;
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The enumerator from first to the last element.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
} 