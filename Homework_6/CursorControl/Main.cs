namespace Homework6
{
    using System;

    public class MainClass
    {
        public static void Main(string[] args)
        {
            var loop = new KeyLoop();
            var mover = new Mover();
            loop.LeftHandler += mover.MoveLeft;
            loop.RightHandler += mover.MoveRight;
            loop.Run();
        }
    }
}
