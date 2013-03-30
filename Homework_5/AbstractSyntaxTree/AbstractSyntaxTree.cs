using System;
using System.Collections.Generic;

namespace Homework_5
{
    class AbstractSyntaxTree
    {
        public static void Main(string[] args)
        {
            AbstractSyntaxTree tree = new AbstractSyntaxTree("( * 7 ( + 2 3 ) )");
            Node head = null;
            tree.Build(ref head);
        }

        public AbstractSyntaxTree(string expression)
        {
            operations = new Dictionary<string, Func<double, double, double>>
            {
                {"+", (x, y) => x + y},
                {"-", (x, y) => x - y},
                {"*", (x, y) => x * y},
                {"/", (x, y) => x / y}
            };
            tokens = expression.Split(new char[] {' '});
            counter = 0;
        }

        public void Build(ref Node current)
        {
            string token = tokens[counter++];
            if (token == ")")
                return;
            if (operations.ContainsKey(token))
            {
                if (current != null)
                {
                    var currentOperation = current as Operation;
                    Node newCurrent = currentOperation.AddOperand(new Operation(token, operations[token]));
                    Build(ref newCurrent);
                } else
                {
                    current = new Operation(token, operations[token]);
                }
            } else
            {
                double value;
                if (Double.TryParse(token, out value))
                {
                    if (current != null)
                    {
                        (current as Operation).AddOperand(new Operand(value));
                    } else
                        current = new Operand(value);
                }
            }
            Build(ref current);
        }

        public double Calculate(Node tree)
        {
            return tree.Calculate();
        }

        public void PrintTree(Node tree)
        {
            tree.Print();
        }

        private string[] tokens;
        private Dictionary<string, Func<double, double, double>> operations;
        private int counter;
    }
}