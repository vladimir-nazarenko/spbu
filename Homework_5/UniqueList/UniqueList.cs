using System;
using MyClasses.Data_structures;

namespace Homework_5
{
    public class UniqueList<T> : LinkedList<T> where T : IComparable
    {
        public UniqueList()
            :base()
        {

        }

        public override void InsertAfter(T value, ListElement<T> position)
        {
            if (Find(value) == null)
                base.InsertAfter(value, position);
            else
                throw new RepeatingElementException();
        }

        public override void InsertFirst(T value)
        {
            if (Find(value) == null)
                base.InsertFirst(value);
            else
                throw new RepeatingElementException();
        }
    }
}