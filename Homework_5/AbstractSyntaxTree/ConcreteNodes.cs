using System;

namespace Homework_5
{
    public class Operation : Node
    {
        public Operation(string sign, Func<double, double, double> performOperation)
        {
            this.Perform = performOperation;
            this.signature = sign;
        }

        public void Print()
        {
            Console.Write(String.Format("{0} ", signature));
        }

        public double Calculate()
        {
            return Perform(this._left.Calculate(), this._right.Calculate());
        }

        public Node AddOperand(Node operand)
        {
            if (_left == null)
                return _left = operand;
            else
                if (_right == null)
                return _right = operand;
            else
                throw new IndexOutOfRangeException();
        }

        Node _left;
        Node _right;
        string signature;
        private Func<double, double, double> Perform;
    }

    public class Operand : Node
    {
        public Operand(double value)
        {
            this._value = value;
        }

        public void Print()
        {
            Console.Write(String.Format("{0} ", _value));
        }

        public double Calculate()
        {
            return _value;
        }

        double _value;
    }
}