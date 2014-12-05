namespace Homework5
{
    using System;
    using MyClasses.DataStructures;

    public class UniqueList<T> : MyClasses.DataStructures.LinkedList<T>
    {
        /// <summary>
        ///  Inserts value after given List Element. Throws exception if the <paramref name="value"/>
        /// was found in the list.
        /// </summary>
        /// <param name='value'>
        /// Value to be inserted.
        /// </param>
        /// <param name='position'>
        /// Position where to put value.
        /// </param>
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

        /// <summary>
        ///  Inserts value on the first position. Throws exception if the <paramref name="value"/>
        /// was found in the list.
        /// </summary>
        /// <param name='value'>
        ///  Value to be inserted. 
        /// </param>
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