namespace MyClasses.DataStructures
{
    using System;

    public interface IHashFunction<T>
    {
        ulong CalculateHash(T value);
    }
}