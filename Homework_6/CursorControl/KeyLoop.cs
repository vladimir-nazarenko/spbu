using System;

namespace Homework_6
{
    public class KeyLoop
    {
        public EventHandler<EventArgs> LeftHandler = (sender, args) => {};
        public EventHandler<EventArgs> RightHandler = (sender, args) => {};

        public KeyLoop()
        {

        }

        public void Run()
        {

        
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        LeftHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.RightArrow:
                        RightHandler(this, EventArgs.Empty);
                        break;
                }
            }   
        }
    }
}

