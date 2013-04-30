namespace Homework5
{
    using System;

    public class Operation : INode
    {
        private INode left;
        private INode right;
        private string signature;
        private Func<double, double, double> perform;

        public Operation(string sign, Func<double, double, double> performOperation)
        {
            this.perform = performOperation;
            this.signature = sign;
        }

        public void Print()
        {
            Console.Write("({0} ", this.signature);
            this.left.Print();
            this.right.Print();
            Console.Write(")");
        }

        public double Calculate()
        {
            return this.perform(this.left.Calculate(), this.right.Calculate());
        }

        public INode AddOperand(INode operand)
        {
            if (this.left == null)
            {
                return this.left = operand;
            }
            else
            {
                if (this.right == null)
                {
                    return this.right = operand;
                } 
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}