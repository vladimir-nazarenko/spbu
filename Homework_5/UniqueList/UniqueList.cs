namespace Homework5
{
    using System;
    using MyClasses.DataStructures;

    public class UniqueList<T> : MyClasses.DataStructures.LinkedList<T>
    {
        public override void InsertAfter(T value, ListElement<T> position)
        {
            if (this.Find(value) == null)
            {
                base.InsertAfter(value, position);
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
                base.InsertFirst(value);
            }
            else
            {
                throw new RepeatingElementException();
            }
        }
    }
}