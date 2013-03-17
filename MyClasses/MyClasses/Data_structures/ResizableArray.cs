using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class ResizableArray<T> : IEnumerable<T> where T : IComparable
    {
        public ResizableArray()
        {
            this.values = new T[1];
            this.writeIndex = 0;
            this.hasValue = new bool[1];
            this.hasValue[0] = false;
            locked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.Data_structures.ResizableArray"/> class.
        /// </summary>
        /// <param name='packed'>
        /// Whether array would be autoshrinked.
        /// </param>
        public ResizableArray(bool packed)
        {
            this.values = new T[1];
            this.writeIndex = 0;
            this.hasValue = new bool[1];
            this.hasValue[0] = false;
            locked = !packed;
        }

        /// <summary>
        /// Inserts the specified value.
        /// </summary>
        /// <param name='value'>
        /// Value.
        /// </param>
        /// <returns>
        /// Key of the value.
        /// </returns>
        public int Insert(T value)
        {
            if (value == null)
                throw new NullReferenceException("nulls are not allowed");
            CheckSize();
            values[writeIndex] = value;
            hasValue[writeIndex++] = true;
            actualSize++;
            return writeIndex - 1;
        }

        /// <summary>
        /// Check existance of the value.
        /// </summary>
        /// <param name='value'>
        /// Key of the value.
        /// </param>
        public int Find(T value)
        {
            if (value == null)
                throw new NullReferenceException("nulls are not allowed");
            for (int i = 0; i < writeIndex; i++)
                if (value.CompareTo(values[i]) == 0 && hasValue[i])
                    return i;
            return -1;
        }

        /// <summary>
        /// Remove the specified value.
        /// </summary>
        public void Remove(T value)
        {
            if (value == null)
                throw new NullReferenceException("nulls are not allowed");
            for (int i = 0; i < writeIndex; i++)
                if (value.CompareTo(values[i]) == 0)
                    hasValue[i] = false;
            actualSize--;
            CheckSize();
        }

        /// <summary>
        /// Retrieves the value specified by the key, can be used 
        /// only with not packed array.
        /// </summary>
        /// <param name='key'>
        /// Key.
        /// </param>
        public T RetrieveByKey(int key)
        {
            if (!locked)
                throw new NotSupportedException("Instance of an array is packed");
            return values[key];
        }

        /// <summary>
        /// Remove the value specified by the key, can be used 
        /// only with not packed array.
        /// </summary>
        /// <param name='key'>
        /// Key.
        /// </param>
        public void RemoveByKey(int key)
        {
            if (!locked)
                throw new NotSupportedException("Instance of an array is packed");
            hasValue[key] = false;
            actualSize--;
        }

        /// <summary>
        /// Trim extra space of this instance, almost
        /// every time breaks keys of values unpleasant to use.
        /// </summary>
        public void Trim()
        {
            bool oldState = locked;
            locked = false;
            CheckSize();
            locked = oldState;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The enumerator from last inserted to first one.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = writeIndex - 1; i > 0; i--)
            {
                if (!hasValue[i]) 
                    continue;
                yield return values[i];
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The enumerator from last inserted to first one.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void CheckSize()
        {
            if (actualSize == values.Length || (writeIndex == values.Length && locked))
                Resize(values.Length * 2);
            else if ((double)actualSize / (double)values.Length < 0.25 && values.Length > 1 && !locked)
                Resize(values.Length / 2);
        }

        private void Resize(int newSize)
        {
            T[] newValues = new T[newSize];
            bool[] newHasValue = new bool[newSize];
            int counter = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (hasValue[i] || locked)
                {
                    newValues[counter] = values[i];
                    newHasValue[counter++] = hasValue[i];
                }
            }
            writeIndex = counter;
            values = newValues;
            hasValue = newHasValue;
            CheckSize();
        }

        private int writeIndex;
        private int actualSize;
        private T[] values;
        private bool[] hasValue;
        private bool locked;
    }
}