using System;
using MyClasses;

namespace Homework_1
{
    public static class Factorial
    {
        public static void Main(string[] args)
        {
            int inputNumber = Parsing.ParseOneIntegerFromConsoleArgs(args);
            if (Parsing.LastErrorMessage == "none")
                Console.WriteLine(Calculate(inputNumber));
            else
                Console.WriteLine(Parsing.LastErrorMessage);   
        }

        public static int Calculate(int number)
        {
            if (number < 0)
                return 0;
            if (number < 2)
                return 1;
            return number * Calculate(number - 1);
        }
    }
}