namespace MyClasses.Exceptions
{
    using System;

    public class NullOperandException : Exception
    {
        public NullOperandException() : base()
        {
        }

        public NullOperandException(string message) : base(message)
        {
        }

        public NullOperandException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
