namespace Homework6
{
    using System;

    /// <summary>
    /// Describes methods of arrow keys handling.
    /// </summary>
    public class Mover
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Homework6.Mover"/> class.
        /// </summary>
        public Mover()
        {
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// Moves console pointer to the left by one.
        /// </summary>
        public void MoveLeft(object sender, EventArgs e)
        {            
            if (Console.CursorLeft > 0)
            {
                Console.CursorLeft--;
            }
        }

         /// <summary>
        /// Moves console pointer to the right by one.
        /// </summary>
        public void MoveRight(object sender, EventArgs e)
        {
            if (Console.CursorLeft < Console.BufferWidth - 1)
            Console.CursorLeft++;
        }

        /// <summary>
        /// Moves console pointer to the top by one.
        /// </summary>
        public void MoveUp(object sender, EventArgs e)
        {
            if (Console.CursorTop > 0)
            {
                Console.CursorTop--;
            }
        }

        /// <summary>
        /// Moves console pointer to the bottom by one.
        /// </summary>
        public void MoveDown(object sender, EventArgs e)
        {
            if (Console.CursorTop < Console.BufferHeight -1)
            {
                Console.CursorTop++;
            }
        }
    }
}