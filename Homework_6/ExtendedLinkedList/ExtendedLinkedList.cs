using System;
using MyClasses.Data_structures;

namespace Homework_6
{
    public class ExtendedLinkedList<T> : LinkedList<T>
    {
        public ExtendedLinkedList()
            : base()
        {
        }

        public ExtendedLinkedList(LinkedList<T> list)
        {
            foreach(T item in list)
                InsertFirst(item);
        }

        /// <summary>
        /// Apply function sent in <param name="performAction"> to every
        /// element in the list.
        /// </summary>
        public LinkedList<T> Map(Action<T> performAction)
        {
            LinkedList<T> returnList = new LinkedList<T>();
                foreach (T item in this)
                    returnList.InsertFirst(performAction(item));
            return returnList;
        }

        /// <summary>
        /// Accumulates all values of the given list to the acc variable. 
        /// </summary>
        /// <param name='acc'>
        /// Acc.
        /// </param>
        /// <param name='action'>
        /// Function for accumulating values.
        /// </param>
        public T Fold(T acc, Func<T, T, T> action)
        {
            ListElement<T> seek = First;
            while (seek != null)
            {
                acc = action(acc, seek.Item);
                seek = seek.next;
            }
            return acc;
        }

        /// <summary>
        /// Retrun sublist where every element satisfies some
        /// criteria.
        /// </summary>
        /// <param name='check'>
        /// Function for criteria checking.
        /// </param>
        public LinkedList<T> Filter(Func<T, bool> check)
        {
            LinkedList<T> matched = new LinkedList<T>();
            foreach(T item in this)
                if (check(item))
                    matched.InsertFirst(item);
            return matched;
        }
    }
}

