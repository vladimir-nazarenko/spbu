using System;
using System.Collections.Generic;

namespace PQueue
{
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        public PriorityQueue()
        {
            list = new List<ValueContainer<T>>();
            size = 0;
        }

        /// <summary>
        /// Enqueue the specified item and set its priority.
        /// </summary>
        /// <param name='item'>
        /// Item.
        /// </param>
        /// <param name='priority'>
        /// Priority.
        /// </param>
        public void Enqueue(T item, int priority)
        {
            var value = new ValueContainer<T>(item, priority);
            list.Add(value);
            size++;
        }

        /// <summary>
        /// Dequeue item with the highest priority
        /// or the last added item.
        /// </summary>
        public T Dequeue()
        {
            if (size == 0)
                throw new EmptyQueueException();
            int maxPriority = Int32.MinValue;
            ValueContainer<T> foreGround = default(ValueContainer<T>);
            foreach (var item in list)
            {
                if (item.Priority > maxPriority)
                    maxPriority = item.Priority;
            }

            foreach (var item in list)
            {
                if (item.Priority == maxPriority)
                {
                    foreGround = item;
                    break;
                }
            }
            size--;
            list.Remove(foreGround);
            return foreGround.Value;
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return size == 0;
        }

        private int size;
        private List<ValueContainer<T>> list;
    }
}

