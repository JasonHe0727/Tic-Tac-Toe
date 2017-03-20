using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
        Initialize();
        ShowAll();
    }

    void Initialize()
    {
        Button[] cells = new Button[3 * 3];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Button();
            /*DrawingArea area = new DrawingArea();
            area.SizeAllocate(new Gdk.Rectangle(0, 0, 100, 100));
            cells[i].Image = area;
            area.ExposeEvent += OnExpose;*/
            cells[i].Clicked += DrawCircle;
            this.table.Attach(cells[i], (uint)i % 3, (uint)i % 3 + 1, (uint)i / 3, (uint)i / 3 + 1);
        }

    }

    //    static readonly Image Circle = new Image("circle.png");
    //    static readonly Image Cross = new Image("cross.png");

    void DrawCircle(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        using (Cairo.Context cr = Gdk.CairoHelper.Create(button.GdkWindow))
        {
            cr.LineWidth = 9;
            cr.SetSourceRGB(0.7, 0.2, 0.0);

            int width, height;
            width = button.Allocation.Width;
            height = button.Allocation.Height;
            cr.Translate(width / 2, height / 2);
            cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();

            cr.SetSourceRGB(0.3, 0.4, 0.6);
            cr.Fill();
            cr.GetTarget().Dispose();
        }
    }

    void OnExpose(object sender, ExposeEventArgs args)
    {
        //DrawingArea area = (DrawingArea)sender;
        Button area = (Button)sender;
        Cairo.Context cr = Gdk.CairoHelper.Create(area.GdkWindow);

        cr.LineWidth = 9;
        cr.SetSourceRGB(0.7, 0.2, 0.0);

        int width, height;
        width = area.Allocation.Width;
        height = area.Allocation.Height;
        cr.Translate(width / 2, height / 2);
        cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2 * Math.PI);
        cr.StrokePreserve();

        cr.SetSourceRGB(0.3, 0.4, 0.6);
        cr.Fill();
        cr.GetTarget().Dispose();
        ((IDisposable)cr).Dispose();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
