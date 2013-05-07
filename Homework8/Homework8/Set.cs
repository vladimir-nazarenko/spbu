namespace Homework8
{
    using System;
    using System.Collections.Generic;
    using Homework5;

    public class Set<T> : UniqueList<T>
    {
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

        public override void Add(T item)
        {
            try
            {
                base.Add(item);
            } 
            catch
            {
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
