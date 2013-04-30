namespace Homework6
{
    using System;

    public class Mover
    {
        public Mover()
        {
            Console.SetCursorPosition(0, 0);
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            int left = Console.CursorLeft;
            if (Console.CursorLeft == 1)
            {
                if (Console.CursorTop > 0)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop - 1);
                }
            }
            else
            {
                Console.SetCursorPosition(left - 2, Console.CursorTop);
            }
        }

        public void MoveRight(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
        }
    }
}