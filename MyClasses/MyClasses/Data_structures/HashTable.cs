using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class HashTable<T> : IEnumerable<T> where T : IComparable
    {
        public HashTable(ulong size, Hash<string> hashF)
        {
            this.table = new LinkedList<T>[size];
            this.hashFunction = hashF;
            this.length = size;
        }

        /// <summary>
        /// Stores specified value.
        /// </summary>
        public void Insert(T value)
        {
            ulong index = hashFunction.CalculateHash(value.ToString()) % length;
            if (table[index] == null) 
                table[index] = new LinkedList<T>();
            table[index].InsertFirst(value);
        }

        /// <summary>
        /// Check if there is a value with key
        /// specified if <param name='value'/>.
        /// </summary>
        public bool Exists(T value)
        {
            ulong index = hashFunction.CalculateHash(value.ToString()) % length;
            if (table[index] == null) 
                return false;
            return (table[index].Find(value) != null);
        }

        /// <summary>
        /// Remove the specified key.
        /// </summary>
        public void Remove(T value)
        {
            ulong index = hashFunction.CalculateHash(value.ToString()) % length;
            if (table[index] == null) 
                return;
            ListElement<T> position = table[index].Find(value);
            if (position != null)
                table[index].Remove(position);
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
                if (table[i] == null) 
                    continue;
                foreach (T item in table[i])
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
            return GetEnumerator();
        }

        private Hash<string> hashFunction;
        private ulong length;
        private LinkedList<T>[] table;
    }

    public interface Hash<T>
    {
        ulong CalculateHash(T value);
    }

    public class FNVHash : Hash<string>
    {
        public ulong CalculateHash(string value)
        {
            const ulong prime = 2365347734339;
            ulong hval = 2166135261;
            for (int i = 0; i < value.Length; i++)
            {
                hval *= prime;
                hval ^= Convert.ToUInt64((value.ToCharArray(i, 1)[0] + 128));
            }
            return hval;
        }
    }
}