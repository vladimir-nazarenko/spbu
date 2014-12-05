namespace MyClasses.DataStructures
{
    using System;

    /// <summary>
    /// List element notion.
    /// </summary>
    /// <typeparam name = "T">Type of encapsulated value</typeparam>
    public class ListElement<T>
    {
        /// <summary>
        /// The stored item.
        /// </summary>
        private T item;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.ListElement{T}" /> class.
        /// </summary>
        /// <param name='value'>
        /// Value to store.
        /// </param>
        public ListElement(T value)
        {
            this.item = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyClasses.DataStructures.ListElement{T}" /> class.
        /// </summary>
        /// <param name='value'>
        /// Value to store.
        /// </param>
        /// <param name='nextNode'>
        /// Next node reference.
        /// </param>
        public ListElement(T value, ListElement<T> nextNode)
        {
            this.item = value;
            this.Next = nextNode;
        }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// Reference to next element.
        /// </value>
        public ListElement<T> Next { get; set; }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// Encapsulated value.
        /// </value>
        public T Item
        {
            get
            {
                return this.item; 
            }
        }
    }
}