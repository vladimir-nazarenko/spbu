namespace MyClasses.Exceptions
{
    using System;

    public class EmptyArrayException : Exception
    {
        public EmptyArrayException() : base()
        {
        }

        public EmptyArrayException(string message) : base(message)
        {
        }

        public EmptyArrayException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
