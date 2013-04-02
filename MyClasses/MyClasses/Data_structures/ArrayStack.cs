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

        public void Push(T item)
        {
            array.Insert(item);
        }

        public T Top()
        {
            IEnumerator<T> e = array.GetEnumerator();
            e.MoveNext();
            return e.Current;
        }

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

        public bool IsEmpty()
        {
            return array.Size == 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (!this.IsEmpty())
                yield return this.Pop();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private ResizableArray<T> array;
    }
}



