using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class LinkedList<T> :  ICollection<T>
    {

        public LinkedList()
        {
            head = new ListElement<T>(default(T));
        }

        /// <summary>
        /// Inserts value after given listElement.
        /// </summary>
        /// <param name='value'>
        /// Value.
        /// </param>
        /// <param name='position'>
        /// Position.
        /// </param>
        public virtual void InsertAfter(T value, ListElement<T> position)
        {
            if (value == null)
                throw new ArgumentNullException();
            ListElement<T> oldNext = position.Next;
            position.Next = new ListElement<T>(value);
            position.Next.Next = oldNext;
            size++;
        }

        /// <summary>
        /// Inserts value on the first position.
        /// </summary>
        /// <param name='value'>
        /// Value.
        /// </param>
        public virtual void InsertFirst(T value)
        {
            InsertAfter(value, head);
        }

        /// <summary>
        /// Remove the element on specified position.
        /// </summary>
        /// <param name='position'>
        /// Position.
        /// </param>
        public void Remove(ListElement<T> position)
        {
            ListElement<T> temp = head;
            while (!temp.Next.Equals(position))
                temp = temp.Next;
            temp.Next = temp.Next.Next;
            size--;
        }

        public bool Remove(T item)
        {
            ListElement<T> seek = Find(item);
            if (seek == null)
                Remove(seek);
            return seek == null;
        }

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Count
        {
            get { return this.size;}
        }

        /// <summary>
        /// Gets the position of a first value.
        /// </summary>
        public ListElement<T> First
        {
            get
            {
                if (head.Next == null)
                    throw new KeyNotFoundException("List is empty");
                return head.Next;
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
            get{ return false;}
        }

        /// <summary>
        /// Checks existance of a given value.
        /// </summary>
        /// <param name='value'>
        /// If there is such value - its first position,
        /// otherwise - null.
        /// </param>
        public ListElement<T> Find(T value)
        {
            ListElement<T> seek = head.Next;
            while (seek != null && !seek.Item.Equals(value))
                seek = seek.Next;
            return seek;
        }

        /// <summary>
        /// Gets the value on specified position.
        /// </summary>
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
        /// Item.
        /// </param>
        public void Add(T item)
        {
            InsertFirst(item);
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        public void Clear()
        {
            head = null;
        }

        /// <summary>
        /// Determines whether this instance contains the value.
        /// </summary>
        /// <param name='item'>
        /// If set to <c>true</c> item.
        /// </param>
        /// <param name='comp'>
        /// If set to <c>true</c> comp.
        /// </param>
        public bool Contains(T item)
        {
            bool found = false;
            foreach (var element in this)
                if (element.Equals(item))
                    found = true;
            return found;
        }

        /// <summary>
        /// Gets the last element of a list.
        /// </summary>
        public ListElement<T> GetLast()
        {
            ListElement<T> seek = head.Next;
            while (seek != null)
                seek = seek.Next;
            return seek;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ListElement<T> seek = head.Next;
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
            ListElement<T> seek = head;
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
            return GetEnumerator();
        }

        private ListElement<T> head;
        private int size;

    }

    /// <summary>
    /// List element for linked list.
    /// </summary>
    public class ListElement<T>
    {
        private T item;
        private ListElement<T> next;
        public ListElement<T> Next { get; set; }
        public T Item{ get { return item; } }

        public ListElement(T value)
        {
            this.item = value;
        }

        public ListElement(T value, ListElement<T> nextNode)
        {
            this.item = value;
            this.next = nextNode;
        }
    }
}