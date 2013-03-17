using System;
using System.Collections.Generic;

namespace MyClasses
{
    //HANDLE NULLS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class ResizableArray<T> : IEnumerable<T> where T : IComparable
    {
        public ResizableArray()
        {
            values = new T[1];
            writeIndex = 0;
            hasValue[0] = false;
        }

        public void Insert(T value)
        {
            CheckSize();
            values[writeIndex] = value;
            hasValue[writeIndex] = true;
        }

        public int Find(T value)
        {
            for (int i = 0; i < writeIndex; i++)
                if (value.CompareTo(values[i]) == 0)
                    return i;
            return -1;
        }

        public void Remove(T value)
        {
            for (int i = 0; i < writeIndex; i++)
                if (value.CompareTo(values[i]) == 0)
                    hasValue[i] = false;;
            actualSize--;
        }

        private void CheckSize()
        {
            if (actualSize == values.Length)
                Resize(values.Length * 2);
            else if ((double) actualSize / (double) values.Length < 0.25 && values.Length > 1)
                Resize(values.Length / 2);
        }

        private void Resize(int newSize)
        {
            T?[] newValues = new T?[newSize];
            int counter = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].HasValue)
                    newValues[counter++] = values[i];
            }
            writeIndex = counter;
            values = newValues;
        }

        private int writeIndex;
        private int actualSize;
        private T[] values;
        private bool[] hasValue;
    }
}
