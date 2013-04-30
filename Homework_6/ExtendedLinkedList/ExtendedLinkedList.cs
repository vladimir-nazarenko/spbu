namespace Homework_6
{
    using System;
    using MyClasses.Data_structures;

    /// <summary>
    /// Linked list with extra methods.
    /// </summary>
    /// <typeparam name = "T">Type of encapsulated value</typeparam>
    public class ExtendedLinkedList<T> : LinkedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Homework_6.ExtendedLinkedList{T}"/> class.
        /// </summary>
        public ExtendedLinkedList()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Homework_6.ExtendedLinkedList{T}"/> class.
        /// </summary>
        /// <param name='list'>
        /// Linked list which will be copied into the instance.
        /// </param>
        public ExtendedLinkedList(LinkedList<T> list)
        {
            foreach (T item in list)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Apply function sent in <param name="performAction"/> to every element in the list.
        /// </summary>
        /// <param name="performAction">
        /// Function delegate which will be implyed to every element.
        /// </param>
        /// <returns>Instance of linked list with elements after action.</returns>
        public LinkedList<T> Map(Func<T, T> performAction)
        {
            LinkedList<T> returnList = new LinkedList<T>();
            foreach (T item in this)
            {
                returnList.Add(performAction(item));
            }

            return returnList;
        }

        /// <summary>
        /// Accumulates all values of the given list to the acc variable. 
        /// </summary>
        /// <param name='acc'>
        /// Accumulating value.
        /// </param>
        /// <param name='action'>
        /// Function for accumulating values.
        /// </param>
        /// <returns>Accumulated value.</returns>
        public T Fold(T acc, Func<T, T, T> action)
        {
            ListElement<T> seek = First;
            while (seek != null)
            {
                acc = action(acc, seek.Item);
                seek = seek.Next;
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
        /// <returns>Sublist with the values satisfy <param name="check"/></returns>
        public LinkedList<T> Filter(Func<T, bool> check)
        {
            LinkedList<T> matched = new LinkedList<T>();
            foreach (T item in this)
            {
                if (check(item))
                {
                    matched.InsertFirst(item);
                }
            }

            return matched;
        }
    }
}