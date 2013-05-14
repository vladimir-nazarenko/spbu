namespace Homework5
{
    using System;

    public class Operand : INode
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Homework5.Operand"/> class.
        /// </summary>
        /// <param name='value'>
        /// Value that should be stored.
        /// </param>
        public Operand(double value)
        {
            this.value = value;
        }

        public void Print()
        {
            Console.Write(string.Format("{0} ", this.value));
        }

        public double Calculate()
        {
            return this.value;
        }
    }
}