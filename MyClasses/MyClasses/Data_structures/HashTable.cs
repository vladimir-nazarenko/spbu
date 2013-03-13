using System;
using System.Collections.Generic;

namespace MyClasses.Data_structures
{
    public class StringHashTable<T> : IEnumerable<T> where T : IComparable
    {
        public StringHashTable(ulong size, Hash<string> hashF)
        {
            this.table = new LinkedList<T>[size];
            this.hashFunction = hashF;
            this.length = size;
        }

        void Insert(T value)
        {
            ulong index = hashFunction.GetHashCode(value.ToString()) % length;
            table[index].InsertFirst(value);
        }

        bool Exists(T value)
        {
            ulong index = hashFunction.GetHashCode(value.ToString()) % length;
            return (table[index].Find(value) != null);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (ulong i = 0; i < this.length; i++)
            {
                foreach (T item in table[i])
                {
                    yield return item;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        Hash<string> hashFunction;
        ulong length;
        LinkedList<T>[] table;
    }

    public abstract class Hash<T>
    {
        public abstract UInt64 GetHashCode(T value);
    }

    public class FNVHash : Hash<string>
    {
        //some troubles with access modifiers
        public override UInt64 GetHashCode(string value)
        {
            const UInt64 prime = 2365347734339;
            UInt64 hval = 2166135261;
            for (int i = 0; i < value.Length; i++)
            {
                hval *= prime;
                hval ^= Convert.ToUInt64((value.ToCharArray(i, 1)[0] + 128));
            }
            return hval;
        }
    }
}

