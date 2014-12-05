using System;
using System.Timers;
using Cairo;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private Timer timer;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        this.Build();
        DrawingArea darea = new DrawingArea();
        darea.ExposeEvent += this.OnExpose;
        this.timer = new Timer(1000);
        this.timer.Start();
        this.timer.Elapsed += (sender, e) => { darea.QueueDraw(); };
        this.Add(darea);

        this.ShowAll();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    private void OnExpose(object sender, ExposeEventArgs args)
    {
        DrawingArea area = (DrawingArea)sender;
        Cairo.Context cr = Gdk.CairoHelper.Create(area.GdkWindow);

        // Draw frame
        cr.LineWidth = 10;
        cr.SetSourceRGB(0, 0, 0);
        int width = Allocation.Width;
        int height = Allocation.Height;
        cr.Translate(width / 2, height / 2);           
        cr.Arc(0, 0, ((width < height ? width : height) / 2) - 10, 0, 2 * Math.PI);
        cr.Stroke();
        cr.Arc(0, 0, ((width < height ? width : height) / 2) - 15, 0, 2 * Math.PI);
        cr.SetSourceRGB(1, 1, 1);
        cr.Fill();
        cr.Stroke();

        // Draw numbers
        cr.SetSourceRGB(0, 0, 0);
        for (int i = 1; i <= 12; i++)
        {
            cr.Rotate(Math.PI / 6);
            cr.MoveTo(0, -((width < height ? width : height) / 2) + 30);
            cr.ShowText(i.ToString());
        }

        // Draw arrows
        double angleOfSecond = (double)DateTime.Now.Second / (double)30 * Math.PI;
        double angleOfMinute = ((double)DateTime.Now.Minute / (double)30 * Math.PI) + (angleOfSecond / 60);
        double angleOfHour = ((double)DateTime.Now.Hour / (double)6 * Math.PI) + (angleOfMinute / 60);
        double sizeMultyplier = Math.Sqrt((height * height) + (width * width));
        double hourMultyplier = 0.1 * sizeMultyplier;
        double minuteMultyplier = 0.2 * sizeMultyplier;
        double secondMultyplier = 0.2 * sizeMultyplier;

        cr.Rotate(-Math.PI / 2); 

        cr.MoveTo(0, 0);
        cr.LineWidth = 3;
        cr.LineTo(minuteMultyplier * Math.Cos(angleOfMinute), minuteMultyplier * Math.Sin(angleOfMinute));
        cr.Stroke();

        cr.MoveTo(0, 0);
        cr.LineWidth = 2;
        cr.LineTo(secondMultyplier * Math.Cos(angleOfSecond), secondMultyplier * Math.Sin(angleOfSecond));
        cr.Stroke();

        cr.MoveTo(0, 0);
        cr.LineWidth = 5;
        cr.LineTo(hourMultyplier * Math.Cos(angleOfHour), hourMultyplier * Math.Sin(angleOfHour));
        cr.Stroke();

        ((IDisposable)cr.Target).Dispose();                                      
        ((IDisposable)cr).Dispose();
    }
}
