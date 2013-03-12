using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class LinkedList<T>  where T : IComparable
    {

        public LinkedList()
        {
            head = new ListElement<T>(default(T));
        }

        public void InsertAfter(T value, ListElement<T> position)
        {
            if (value == null)
                throw new ArgumentNullException();
            ListElement<T> oldNext = position.next;
            position.next = new ListElement<T>(value);
            position.next.next = oldNext;
            size++;
        }

        public void InsertFirst(T value)
        {
            InsertAfter(value, head);
        }

        public void Remove(ListElement<T> position)
        {
            ListElement<T> temp = head;
            while (!temp.next.Equals(position))
                temp = temp.next;
            temp.next = temp.next.next;
            size--;
        }

        public int Length
        {
            get { return this.size;}
        }

        public ListElement<T> First
        {
            get
            {
                return head.next;
            }
        }

        public ListElement<T> Find(T value)
        {
            ListElement<T> seek = head;
            while (seek != null && seek.Item.CompareTo(value) != 0)
                seek = seek.next;
            return seek;
        }

        public T Retrieve(ListElement<T> element)
        {
            return element.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnum(head);
        }

        private ListElement<T> head;
        private int size;

        public class ListEnum : IEnumerator<T>
        {
            public ListEnum(ListElement<T> list)
            {
                seek = list.next;
                head = list;
            }

            public T Current
            {
                get
                {
                    return seek.Item;
                }
            }
            //below noted compilation error
            object IEnumerator<T>.Current
            {
                get //...is an accessor not found in interface member `System.Collections.Generic.IEnumerator<T>.Current' (CS0550)
                {
                    return seek.Item;
                }
            }

            public bool MoveNext()
            {
                return (!(seek.next == null));
            }

            public void Reset()
            {
                seek = head.next;
            }

            void IDisposable.Dispose()
            {
            }

            private ListElement<T> seek;
            private ListElement<T> head;
    
        }

    }

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