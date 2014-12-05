namespace Homework8
{
    using System;
    using System.Collections.Generic;
    using MyClasses;

    public class Set<T> : ICollection<T>
    {
        private MyClasses.DataStructures.LinkedList<T> storage;

        /// <summary>
        /// Gets the number of the elements in the current instance.
        /// </summary>
        public int Count
        {
            get
            {
                return storage.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Set()
        {
            storage = new MyClasses.DataStructures.LinkedList<T>();
        }

        public void Clear()
        {
            storage.Clear();
        }

        /// <summary>
        /// Copies instance to an array.
        /// </summary>
        /// <param name='array'>
        /// Array.
        /// </param>
        /// <param name='index'>
        /// Index where to start.
        /// </param>
        public void CopyTo(T[] array, int index)
        {
            this.storage.CopyTo(array, index);
        }

        public bool Remove(T item)
        {
            return storage.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static new bool Equals(object first, object second)
        {
            if (!(first is Set<T>) || !(second is Set<T>))
            {
                return object.Equals(first, second);
            }

            var firstSet = first as Set<T>;
            var secondSet = second as Set<T>;
            bool isEqual = true;
            if (firstSet.Count == secondSet.Count)
            {
                foreach (T item in firstSet)
                {
                    if (!secondSet.Contains(item))
                    {
                        isEqual = false;
                    }
                }

                return isEqual;
            }

            return false;
        }

        /// <summary>
        /// Unite the current instance with another.
        /// </summary>
        /// <param name='another'>
        /// Another instance.
        /// </param>
        public Set<T> Unite(Set<T> another)
        {
            var newSet = (Set<T>)this.MemberwiseClone();
            foreach (T item in another)
            {
                if (!newSet.Contains(item))
                {
                    newSet.Add(item);
                }
            }

            return newSet;
        }

        /// <summary>
        /// Intersect the current instance with another.
        /// </summary>
        /// <param name='another'>
        /// Another instance.
        /// </param>
        public Set<T> Intersect(Set<T> another)
        {
            var newSet = new Set<T>();
            foreach (T item in this)
            {
                if (another.Contains(item))
                {
                    newSet.Add(item);
                }
            }

            return newSet;
        }

        public bool Contains(T item)
        {
            return storage.Contains(item);
        }

        public void Add(T item)
        {
            if (!this.storage.Contains(item))
            {
                storage.Add(item);
            }
        }

        public override bool Equals(object another)
        {
            return Set<T>.Equals(this, another);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
