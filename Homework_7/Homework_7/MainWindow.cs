using System;
using Gtk;
using Homework7;

public partial class MainWindow : Gtk.Window
{   
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        this.Build();
        this.TraverseWidgets(this);
    }
    
    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    /// <summary>
    /// Checks if widget is button, if so - bounds it with the handler.
    /// </summary>
    /// <param name='w'>
    /// Widget to bound.
    /// </param>
    private void BoundHandlers(Widget w)
    {
        if (w is Gtk.Button)
        {
            (w as Button).Clicked += new System.EventHandler(this.ButtonPressedHandler);
        }
    }

    private void ButtonPressedHandler(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string label = btn.Label;
        switch (label)
        {
            case "Clear":
                this.inputBox.Buffer.Text = string.Empty;
                break;
            case "=":
                double result = Evaluator.Evaluate(this.inputBox.Buffer.Text);
                this.inputBox.Buffer.Text = result.ToString();
                break;
            default:
                this.inputBox.Buffer.Text += label;
                break;
        }
    }

    /// <summary>
    /// Traverses all the the widgets on the form and
    /// calls <see href = "BoundHandlers"/> for every widget.
    /// </summary>
    /// <param name='parent'>
    /// Parent widget to start traverse.
    /// </param>
    private void TraverseWidgets(Widget parent)
    {
        if (!(parent is Container))
        {
            return;
        }

        foreach (Widget w in (parent as Container).AllChildren)
        {
            this.TraverseWidgets(w);
            this.BoundHandlers(w);
        }
    }
}
