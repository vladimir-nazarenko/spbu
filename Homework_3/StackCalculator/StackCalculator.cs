using System;
using MyClasses.Data_structures;
using System.Collections.Generic;

namespace Homework_3
{
    public class StackCalculator
    {

        public static int Main(string[] args)
        {
            var calc = new StackCalculator(args[0], new LinkedStack<double>());
            try
            {
                Console.WriteLine(String.Format("{0} = {1}", args[0], calc.Calculate()));
            } catch
            {
                Console.WriteLine("Incorrect Expression");
            }
            return 0;
        }

        public StackCalculator(string expression, IStack<double> stackImplementation)
        {
            input = expression;
            stack = stackImplementation;
            operations = new Dictionary<string, Func<double, double, double>>
            {
                {"+", (x, y) => x + y},
                {"-", (x, y) => x - y},
                {"*", (x, y) => x * y},
                {"/", (x, y) => x / y}
            };
        }

        public double Calculate()
        {
            Parse();
            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];
                int number = 0;
                if (Int32.TryParse(token, out number))
                    stack.Push(number);
                else
                {
                    if (operations.ContainsKey(token))
                        stack.Push(operations[token](stack.Pop(), stack.Pop()));
                    else
                        throw new NotSupportedException(String.Format("Operation {0} has no implementation", token));
                }
            }
            double result = stack.Pop();
            if (stack.IsEmpty())
                return result;
            throw new Exception("Expression error");
        }

        private void Parse()
        {
            char[] separators = {' ', '\t', '\n'};
            tokens = input.Split(separators);
        }

        private string input;
        string[] tokens;
        private IStack<double> stack;
        private delegate double OperationDelegate(double x,double y);
        private Dictionary<string, Func<double, double, double>> operations;
    }
}

