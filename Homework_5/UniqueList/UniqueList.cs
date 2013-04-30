namespace Homework5
{
    using System;
    using MyClasses.DataStructures;

    public class UniqueList<T> : LinkedList<T> where T : IComparable
    {
        public UniqueList()
            : base()
        {
        }

        public override void InsertAfter(T value, ListElement<T> position)
        {
            if (this.Find(value) == null)
            {
                this.InsertAfter(value, position);
            }
            else
            {
                throw new RepeatingElementException();
            }
        }

        public override void InsertFirst(T value)
        {
            if (this.Find(value) == null)
            {
                this.InsertFirst(value);
            }
            else
            {
                throw new RepeatingElementException();
            }
        }
    }
}