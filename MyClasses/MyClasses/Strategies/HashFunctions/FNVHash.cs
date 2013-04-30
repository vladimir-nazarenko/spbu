namespace MyClasses.DataStructures
{
    using System;

    public class FNVHash<T> : IHashFunction<T>
    {
        /// <summary>
        /// Calculates the hash of the <param name="value"/>.
        /// </summary>
        /// <returns>
        /// The hash key.
        /// </returns>
        /// <param name='value'>
        /// Value for key calculating.
        /// </param>
        public ulong CalculateHash(T value)
        {
            string stringValue = value.ToString();
            const ulong Prime = 2365347734339;
            ulong hval = 2166135261;
            for (int i = 0; i < stringValue.Length; i++)
            {
                hval *= Prime;
                hval ^= Convert.ToUInt64(stringValue.ToCharArray(i, 1)[0] + 128);
            }

            return hval;
        }
    }
}