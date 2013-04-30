namespace Homework6
{
    using System;

    public class KeyLoop
    {
        public event EventHandler<EventArgs> LeftHandler = (sender, args) => { };

        public event EventHandler<EventArgs> RightHandler = (sender, args) => { };

        public void Run()
        {        
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        this.LeftHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.RightArrow:
                        this.RightHandler(this, EventArgs.Empty);
                        break;
                }
            }   
        }
    }
}