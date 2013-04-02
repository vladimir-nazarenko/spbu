using System;

namespace PQueue
{
    public class EmptyQueueException : Exception
    {
        public EmptyQueueException()
            : base()
        {

        }

        public EmptyQueueException(string message)
            : base(message)
        {

        }

        public EmptyQueueException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

