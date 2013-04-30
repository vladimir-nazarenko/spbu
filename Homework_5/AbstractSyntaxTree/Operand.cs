namespace Homework5
{
    using System;

    public class Operand : INode
    {
        private double value;

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