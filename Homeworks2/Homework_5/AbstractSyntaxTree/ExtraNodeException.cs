namespace Homework5
{
    using System;

    public class ExtraNodeException : Exception
    {
        public ExtraNodeException() : base()
        {
        }

        public ExtraNodeException(string message) : base(message)
        {
        }

        public ExtraNodeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}