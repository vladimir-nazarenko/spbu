namespace Homework7
{
    using System;
    using Gtk;

    public class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();

            Application.Run();
        }
    }
}
