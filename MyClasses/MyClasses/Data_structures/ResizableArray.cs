namespace MyClasses.DataStructures
{
    using System;
    using System.Collections.Generic;

    public class ResizableArray<T> : IEnumerable<T>
    {
        private int writeIndex;
        private int actualSize;
        private T[] values;
        private bool[] hasValue;
        private bool locked;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.ResizableArray{T}"/> class.
        /// </summary>
        public ResizableArray()
        {
            this.values = new T[1];
            this.writeIndex = 0;
            this.hasValue = new bool[1];
            this.hasValue[0] = false;
            this.locked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.ResizableArray{T}"/> class.
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
            this.locked = !packed;
        }
        
        /// <summary>
        /// Gets the number of elements in the array.
        /// </summary>
        /// <value>
        /// Number of elements in the array.
        /// </value>
        public int Size
        {
            get
            {
                return this.actualSize;
            }
        }

        /// <summary>
        /// Inserts the specified value.
        /// </summary>
        /// <param name='value'>
        /// Value to insert.
        /// </param>
        /// <returns>
        /// Key of the value.
        /// </returns>
        public int Insert(T value)
        {
            if (value == null)
            {
                throw new Exceptions.NullOperandException();
            }

            this.CheckSize();
            this.values[this.writeIndex] = value;
            this.hasValue[this.writeIndex++] = true;
            this.actualSize++;
            return this.writeIndex - 1;
        }

        /// <summary>
        /// Check existance of the value.
        /// </summary>
        /// <param name='value'>
        /// Value to determine key.
        /// </param>
        /// <returns>Key of the value.</returns>
        public int Find(T value)
        {
            if (value == null)
            {
                throw new Exceptions.NullOperandException();
            }

            for (int i = 0; i < this.writeIndex; i++)
            {
                if (value.Equals(this.values[i]) && this.hasValue[i])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Remove the specified value.
        /// </summary>
        /// <param name="value">
        /// Value to be removed.
        /// </param>
        public void Remove(T value)
        {
            if (value == null)
            {
                throw new Exceptions.NullOperandException();
            }

            for (int i = 0; i < this.writeIndex; i++)
            {
                if (value.Equals(this.values[i]))
                {
                    this.hasValue[i] = false;
                }
            }

            this.actualSize--;
            this.CheckSize();
        }

        /// <summary>
        /// Retrieves the value specified by the key, can be used 
        /// only with not packed array.
        /// </summary>
        /// <param name='key'>
        /// Key of the value to be retrieved.
        /// </param>
        /// <returns>Value with given key.</returns>
        public T RetrieveByKey(int key)
        {
            if (!this.locked)
            {
                throw new NotSupportedException("Instance of an array is packed");
            }

            return this.values[key];
        }

        /// <summary>
        /// Remove the value specified by the key, can be used 
        /// only with not packed array.
        /// </summary>
        /// <param name='key'>
        /// Key of value to be removed.
        /// </param>
        public void RemoveByKey(int key)
        {
            if (!this.locked)
            {
                throw new NotSupportedException("Instance of an array is packed");
            }

            this.hasValue[key] = false;
            this.actualSize--;
        }

        /// <summary>
        /// Trim extra space of this instance, almost
        /// every time breaks keys of values, unpleasant to use.
        /// </summary>
        public void Trim()
        {
            bool oldState = this.locked;
            this.locked = false;
            this.CheckSize();
            this.locked = oldState;
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The enumerator from last inserted to first one.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.writeIndex - 1; i > 0; i--)
            {
                if (!this.hasValue[i]) 
                {
                    continue;
                }

                yield return this.values[i];
            }
        }

        /// <summary>
        /// Enumerator of the keys of the values.
        /// </summary>
        /// <returns>
        /// The enumerator from key of a last inserted 
        /// element to a first one.
        /// </returns>
        public IEnumerator<int> KeysEnumerator()
        {
            for (int i = this.writeIndex - 1; i > 0; i--)
            {
                if (!this.hasValue[i]) 
                {
                    continue;
                }

                yield return i;
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
            return this.GetEnumerator();
        }

        private void CheckSize()
        {
            if (this.actualSize == this.values.Length || (this.writeIndex == this.values.Length && this.locked))
            {
                this.Resize(this.values.Length * 2);
            } 
            else 
            {
                if ((double)this.actualSize / (double)this.values.Length < 0.25 && this.values.Length > 1 && !this.locked)
                {
                    this.Resize(this.values.Length / 2);
                }
            }
        }

        private void Resize(int newSize)
        {
            T[] newValues = new T[newSize];
            bool[] newHasValue = new bool[newSize];
            int counter = 0;
            for (int i = 0; i < this.values.Length; i++)
            {
                if (this.hasValue[i] || this.locked)
                {
                    newValues[counter] = this.values[i];
                    newHasValue[counter++] = this.hasValue[i];
                }
            }

            this.writeIndex = counter;
            this.values = newValues;
            this.hasValue = newHasValue;
            this.CheckSize();
        }
    }
}