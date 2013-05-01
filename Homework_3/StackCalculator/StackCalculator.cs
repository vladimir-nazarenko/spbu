namespace Homework3
{
    using System;
    using System.Collections.Generic;
    using MyClasses.DataStructures;

    public class StackCalculator
    {
        private string input;

        private string[] tokens;

        private IStack<double> stack;

        private Dictionary<string, Func<double, double, double>> operations;

        public StackCalculator(string expression, IStack<double> stackImplementation)
        {
            this.input = expression;
            this.stack = stackImplementation;
            this.operations = new Dictionary<string, Func<double, double, double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => x / y }
            };
        }

        private delegate double OperationDelegate(double x, double y);

        public static int Main(string[] args)
        {
            var calc = new StackCalculator(args[0], new LinkedStack<double>());
            try
            {
                Console.WriteLine(string.Format("{0} = {1}", args[0], calc.Calculate()));
            } 
            catch
            {
                Console.WriteLine("Incorrect Expression");
            }

            return 0;
        }

        public double Calculate()
        {
            this.Parse();
            for (int i = 0; i < this.tokens.Length; i++)
            {
                string token = this.tokens[i];
                int number = 0;
                if (int.TryParse(token, out number))
                {
                    this.stack.Push(number);
                } 
                else
                {
                    if (this.operations.ContainsKey(token))
                    {
                        double x = this.stack.Pop();
                        double y = this.stack.Pop();
                        double res = this.operations[token](x, y);
                        this.stack.Push(res);
                    } 
                    else
                    {
                        throw new NotSupportedException(string.Format("Operation {0} has no implementation", token));
                    }
                }
            }
            // Баг сидит здесь!
            var r = 5.0;
            r = this.stack.Pop();
            if (this.stack.IsEmpty())
            {
                return r;
            }

            throw new Exception("Expression error");
        }

        private void Parse()
        {
            char[] separators = { ' ', '\t', '\n' };
            this.tokens = this.input.Split(separators);
        }      
    }
}
