namespace Homework5
{
    using System;
    using System.Collections.Generic;

    public class AbstractSyntaxTree
    {
        private string[] tokens;
        private Dictionary<string, Func<double, double, double>> operations;
        private int counter;

        public AbstractSyntaxTree(string expression)
        {
            this.operations = new Dictionary<string, Func<double, double, double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => x / y }
            };
            this.tokens = expression.Split(new char[] { ' ' });
            this.counter = 0;
        }

        public void Build(ref INode current)
        {
            string token = this.tokens[this.counter++];
            if (token == ")")
            {
                return;
            }

            if (this.operations.ContainsKey(token))
            {
                if (current != null)
                {
                    var currentOperation = current as Operation;
                    INode newCurrent = currentOperation.AddOperand(new Operation(token, this.operations[token]));
                    this.Build(ref newCurrent);
                } 
                else
                {
                    current = new Operation(token, this.operations[token]);
                }
            } 
            else
            {
                double value;
                if (double.TryParse(token, out value))
                {
                    if (current != null)
                    {
                        (current as Operation).AddOperand(new Operand(value));
                    } 
                    else
                    {
                        current = new Operand(value);
                    }
                }
            }

            this.Build(ref current);
        }

        public double Calculate(INode tree)
        {
            return tree.Calculate();
        }

        public void PrintTree(INode tree)
        {
            tree.Print();
        }
    }
}