using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class LinkedList<T> : IEnumerable<T> where T : IComparable
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
            ListElement<T> oldNext = position.next;
            position.next = new ListElement<T>(value);
            position.next.next = oldNext;
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
            while (!temp.next.Equals(position))
                temp = temp.next;
            temp.next = temp.next.next;
            size--;
        }

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Length
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
                if (head.next == null)
                    throw new KeyNotFoundException("List is empty");
                return head.next;
            }
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
            ListElement<T> seek = head.next;
            while (seek != null && seek.Item.CompareTo(value) != 0)
                seek = seek.next;
            return seek;
        }

        /// <summary>
        /// Gets the value on specified position.
        /// </summary>
        public T Retrieve(ListElement<T> position)
        {
            return position.Item;
        }

        /// <summary>
        /// Gets the last element of a list.
        /// </summary>
        public ListElement<T> GetLast()
        {
            ListElement<T> seek = head.next;
            while (seek != null)
                seek = seek.next;
            return seek;
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
            while (seek.next != null)
            {
                seek = seek.next;
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
    public class ListElement<T> where T : IComparable
    {
        private T item;
        public ListElement<T> next;

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