namespace Homework6
{
    using System;

    /// <summary>
    /// Gets keys and attempts to handle them.
    /// </summary>
    public class KeyLoop
    {
        public event EventHandler<EventArgs> LeftHandler = (sender, args) => { };

        public event EventHandler<EventArgs> RightHandler = (sender, args) => { };

        public event EventHandler<EventArgs> UpHandler = (sender, args) => { };

        public event EventHandler<EventArgs> DownHandler = (sender, args) => { };

        /// <summary>
        /// Starts infinite loop for key input.
        /// </summary>
        public void Run()
        {        
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        this.LeftHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.RightArrow:
                        this.RightHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.UpArrow:
                        this.UpHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.DownArrow:
                        this.DownHandler(this, EventArgs.Empty);
                        break;
                    default:
                        Console.Write('\b');
                        break;
                }
            }   
        }
    }
}