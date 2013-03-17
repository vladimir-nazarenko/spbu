using System;

namespace MyClasses.Data_structures
{
    public interface IHashFunction<T>
    {
        ulong CalculateHash(T value);
    }
}