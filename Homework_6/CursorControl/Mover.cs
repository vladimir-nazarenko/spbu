using System;

namespace Homework_6
{
    public class Mover
    {
        public Mover()
        {
        }

        public void MoveLeft(object sender, EventArgs e)
        {
            int posX = Console.CursorLeft--;
            if (posX > 1)
            {
                Console.SetCursorPosition(posX - 2, Console.CursorTop);
            }
        }

        public void MoveRight(object sender, EventArgs e)
        {
            int posX = Console.CursorLeft--;
            if (posX < Console.WindowWidth - 1)
            {
                Console.SetCursorPosition(posX, Console.CursorTop);
            }
        }
    }
}

