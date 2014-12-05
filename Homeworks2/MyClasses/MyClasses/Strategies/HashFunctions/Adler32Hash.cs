namespace MyClasses.DataStructures
{
    public class Adler32Hash<T> : IHashFunction<T>
        {
            /// <summary>
            /// Calculates the hash of the <param name="value"/>.
            /// </summary>
            /// <returns>
            /// The 32-bit hash key.
            /// </returns>
            /// <param name='value'>
            /// Value for key calculating.
            /// </param>
            public ulong CalculateHash(T value)
            {
                string stringValue = value.ToString();
                char buf;
                ulong s1 = 1;
                ulong s2 = 0;
                ulong result = 0;
                for (int i = 0; i < stringValue.Length; i++)
                {
                    buf = stringValue[i];
                    s1 = (s1 + buf) % 65521;
                    s2 = (s2 + s1) % 65521;
                    result += (s2 << 16) + s1;
                }

                return result;
            }
        }
}