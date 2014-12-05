namespace Homework7
{
    using System;
    using System.Collections.Generic;
    using Homework3;
    using MyClasses;
    using MyClasses.DataStructures;

    public static class Evaluator
    {
        public static double Evaluate(string expression)
        {
            string splitted = SplitBySpaces(expression);
            string postfix = GetPostfixNotion(splitted);
            var calculator = new StackCalculator(postfix, new LinkedStack<double>());
            return calculator.Calculate();
        }

        private static string GetPostfixNotion(string expression)
        {
            var stack = new Stack<string>();
            string result = string.Empty;
            string[] tokens = Parse(expression);
            for (int index = 0; index < tokens.Length; index++)
            {
                string current = tokens[index];     
                if (current == "(" || current == ")" || current == "+" ||
                    current == "-" || current == "*" || current == "/")
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(current);
                    } 
                    else
                    {
                        string temp = stack.Peek();
                        switch (current)
                        {
                            case "+":                   
                                if (temp == "*" || temp == "/")
                                {
                                    result += stack.Pop() + " ";
                                }

                                stack.Push("+");
                                break;
                            case "-":
                                if (temp == "*" || temp == "/")
                                {
                                    result += stack.Pop() + " ";
                                }

                                stack.Push("-");
                                break;
                            case "*":
                                stack.Push("*");
                                break;
                            case "/":
                                stack.Push("/");
                                break;
                            case "(":
                                stack.Push("(");
                                break;
                            case ")":
                                temp = stack.Pop();
                                while (temp != "(" && stack.Count != 0)
                                {
                                    result += temp + " ";
                                    temp = stack.Pop();
                                }

                                break;
                        }
                    }
                } 
                else
                {
                    double number = 0.0;
                    if (double.TryParse(current, out number))
                    {
                        result += current + " ";
                    }            
                }
            }

            while (stack.Count != 0)
            {
                result += stack.Pop() + ' ';
            }

            result = result.Trim();
            return result;
        }

        private static string[] Parse(string expression)
        {
            char[] separators = { ' ', '\t', '\n' };
            return expression.Split(separators);
        }

        private static string SplitBySpaces(string expression)
        {
            string splitted = string.Empty;
            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]) || expression[i] == '.')
                {
                    splitted += expression[i];
                }
                else
                {
                    splitted += " " + expression[i] + " ";
                }
            }

            return splitted;
        }
    }
}
