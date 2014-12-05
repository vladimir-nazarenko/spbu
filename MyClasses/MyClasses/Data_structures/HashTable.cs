namespace MyClasses.DataStructures
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Hash table with open hashing.
    /// </summary>
    /// <typeparam name = "T">
    /// Type of value to be stored
    /// </typeparam>
    public class HashTable<T> : IEnumerable<T> where T : IComparable
    {        
        /// <summary>
        /// The hash function.
        /// </summary>
        private IHashFunction<T> hashFunction;

        /// <summary>
        /// The length of hash table.
        /// </summary>
        private ulong length;

        /// <summary>
        /// The table to store values.
        /// </summary>
        private LinkedList<T>[] table;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.HashTable{T}"/> class.
        /// </summary>
        /// <param name='size'>
        /// Base size of table.
        /// </param>
        /// <param name='hashF'>
        /// Hash function.
        /// </param>
        public HashTable(ulong size, IHashFunction<T> hashF)
        {
            this.table = new LinkedList<T>[size];
            this.hashFunction = hashF;
            this.length = size;
        }

        /// <summary>
        /// Stores specified value.
        /// </summary>
        /// <param name="value">
        /// Value to be inserted in the current instance.
        /// </param>
        public void Insert(T value)
        {
            ulong index = this.hashFunction.CalculateHash(value) % this.length;
            if (this.table[index] == null)
            {
                this.table[index] = new LinkedList<T>();
            }

            this.table[index].InsertFirst(value);
        }

        /// <summary>
        /// Check if there is a value with key
        /// specified if <param name='value'/>.
        /// </summary>
        /// <param name="value">
        /// Value to be found.
        /// </param>
        /// <returns><c>true</c> if value found, otherwise <c>false</c></returns>
        public bool Exists(T value)
        {
            ulong index = this.hashFunction.CalculateHash(value) % this.length;
            if (this.table[index] == null)
            {
                return false;
            }

            return this.table[index].Find(value) != null;
        }

        /// <summary>
        /// Remove the specified key.
        /// </summary>
        /// <param name="value">
        /// Value to be removed from instance.
        /// </param>
        public void Remove(T value)
        {
            ulong index = this.hashFunction.CalculateHash(value) % this.length;
            if (this.table[index] == null)
            {
                return;
            }

            ListElement<T> position = this.table[index].Find(value);
            if (position != null)
            {
                this.table[index].Remove(position);
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The unpredictable enumerator.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (ulong i = 0; i < this.length; i++)
            {
                if (this.table[i] == null) 
                {
                    continue;
                }

                foreach (T item in this.table[i])
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>
        /// The unpredictable enumerator.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Changes the hash function of the current instance.
        /// </summary>
        /// <param name='func'>
        /// New hash function.
        /// </param>
        public void ChangeHashFunction(IHashFunction<T> func)
        {
            this.hashFunction = func;
            LinkedList<T> exchangeBuffer = new LinkedList<T>();
            if (this.table == null)
            {
                return;
            }

            for (int i = 0; i < this.table.Length; i++)
            {
                if (this.table[i] == null)
                {
                    continue;
                }

                int length = this.table[i].Count;
                for (int j = 0; j < length; j++)
                {
                    exchangeBuffer.InsertFirst(this.table[i].Retrieve(this.table[i].First));
                    this.table[i].Remove(this.table[i].First);
                }
            }

            int size = exchangeBuffer.Count;
            for (int j = 0; j < size; j++)
            {
                this.Insert(exchangeBuffer.Retrieve(exchangeBuffer.First));
                exchangeBuffer.Remove(exchangeBuffer.First);
            }
        }
    }
}