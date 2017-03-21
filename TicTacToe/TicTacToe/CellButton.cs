using System;
using Gtk;

namespace TicTacToe
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CellButton : Gtk.Button
    {
        Image circle;
        Image crossing;
        Image empty;

        public int Id{ get; private set; }

        public CellButton(int Id)
            : base()
        {
            this.Build();
            this.Id = Id;
            this.circle = new Image("./figures/circle.png");
            this.empty = new Image("./figures/empty.png");
            this.crossing = new Image("./figures/crossing.png");
            this.Image = this.empty;
        }

        public void DrawCircle()
        {
            this.Image = this.circle;
        }

        public void DrawCrossing()
        {
            this.Image = this.crossing;
        }

        public void Reset()
        {
            this.Image = this.empty;
        }

    }
}

