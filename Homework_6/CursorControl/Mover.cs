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
            this.RemoveLast();
            if (Console.CursorLeft > 0)
            {
                Console.CursorLeft--;
            }
            else
            {
                if (Console.CursorTop > 0)
                {
                    Console.CursorTop--;
                    Console.CursorLeft = Console.BufferWidth - 1;
                }
            }
        }

        public void MoveRight(object sender, EventArgs e)
        {
            this.RemoveLast();
            if (Console.CursorLeft == Console.BufferWidth - 1)
            {
                this.RemoveLast();
            }
            else
            {
                Console.CursorLeft++;
            }
        }

        public void MoveUp(object sender, EventArgs e)
        {
            this.RemoveLast();
            if (Console.CursorTop > 0)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            }
        }

        public void MoveDown(object sender, EventArgs e)
        {
            this.RemoveLast();
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
        }

        public void RemoveLast()
        {
            Console.Write('\b');
        }
    }
}