using System;

namespace MyClasses
{
    public class StringHashTable
    {
        public StringHashTable()
        {
        }

        Hash<string> hashFunction;
        UInt32 size;
    }

    interface Hash<T>
    {
        UInt64 GetHashCode(T value);
    }

    public class FNVHash : Hash<string>
    {
        //some troubles with access modifiers
        UInt64 Hash<string>.GetHashCode(string value)
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

