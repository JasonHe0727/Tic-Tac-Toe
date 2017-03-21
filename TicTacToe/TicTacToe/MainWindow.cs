using System;
using Gtk;
using TicTacToe;

public partial class MainWindow: Gtk.Window
{
    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
        Initialize();
        ShowAll();
    }

    const int nRows = 3;
    const int nCols = 3;
    CellButton[] cells;
    Lattice lattice;

    void Initialize()
    {
        this.cells = new CellButton[nRows * nCols];
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new CellButton(i);
            cells[i].Clicked += CellButton_Clicked;
            this.table.Attach(cells[i], (uint)i % 3, (uint)i % 3 + 1, (uint)i / 3, (uint)i / 3 + 1);
        }
        button_start.Clicked += (object sender, EventArgs e) =>
        {
            this.lattice.Reset();
            ShowLattice();
        };
        this.lattice = new Lattice();
        this.Resizable = false;
    }

    void CellButton_Clicked(object sender, EventArgs e)
    {
        CellButton button = (CellButton)sender;
        int number = button.Id;
        if (lattice[number / 3, number % 3] != CellType.Empty)
        {
            return;
        }
        else
        {
            lattice[number / 3, number % 3] = CellType.X;
            var current = lattice.Current;
            if (current == Situation.Playing)
            {
                lattice.RandomSet(CellType.O);
            }
            current = lattice.Current;
            if (current != Situation.Playing)
            {
                ShowLattice();
                using (var dialog = new MessageDialog(
                                        this, 
                                        DialogFlags.DestroyWithParent, 
                                        MessageType.Info, 
                                        ButtonsType.Ok, 
                                        "result = {0}", current))
                {
                    dialog.Run();
                    dialog.Hide();
                }
            }
            else
            {
                ShowLattice();
            }
        }
    }

    void ShowLattice()
    {
        for (int i = 0; i < lattice.Cells.Length; i++)
        {
            if (lattice.Cells[i] == CellType.X)
            {
                this.cells[i].DrawCrossing();
            }
            else if (lattice.Cells[i] == CellType.O)
            {
                this.cells[i].DrawCircle();
            }
            else
            {
                this.cells[i].Reset();
            }
        }
    }

    void Play()
    {
        lattice.Reset();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}
