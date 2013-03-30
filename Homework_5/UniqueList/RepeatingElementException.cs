using System;


namespace Homework_5
{
    public class RepeatingElementException : Exception
    {
        public RepeatingElementException()
            :base()
        {
        }

        public RepeatingElementException(string message)
            :base(message)
        {
        }

        public RepeatingElementException(string message, Exception inner)
            :base(message, inner)
        {
        }
    }
}

