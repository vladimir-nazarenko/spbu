using System;

namespace PQueue
{
    /// <summary>
    /// Auxulliary class to store priority of a value.
    /// </summary>
    public class ValueContainer<T>
    {
        public ValueContainer(T value, int priority)
        {
            this.value = value;
            this.priority = priority;
        }

        public int Priority{ get {return priority; } }
        public T Value{ get { return value; }}

        private T value;
        private int priority;
    }
}

