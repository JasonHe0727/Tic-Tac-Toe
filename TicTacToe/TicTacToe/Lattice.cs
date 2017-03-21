using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TicTacToe
{
    public class Lattice
    {
        CellType[] cells;
        const int nRows = 3;
        const int nCols = 3;

        public CellType[] Cells { get { return this.cells; } }

        public Lattice()
        {
            cells = new CellType[nRows * nCols];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = CellType.Empty;
            }
        }

        public CellType this [int row, int col]
        {
            get
            {
                return cells[row * nCols + col];
            }
            set
            {
                cells[row * nCols + col] = value;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = CellType.Empty;
            }
        }

        public Situation Current
        {
            get
            {
                Situation result = RowCheck();
                if (result != Situation.Playing)
                {
                    return result;
                }
                result = ColumnCheck();
                if (result != Situation.Playing)
                {
                    return result;
                }
                result = HypotenuseCheck();
                if (result != Situation.Playing)
                {
                    return result;
                }
                if (IsEmpty)
                {
                    return Situation.Playing;
                }
                else if (IsFull)
                {
                    return Situation.Tied;
                }
                else
                {
                    return Situation.Playing;
                }
            }
        }

        bool IsEmpty
        {
            get
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] != CellType.Empty)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        bool IsFull
        {
            get
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == CellType.Empty)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        Situation RowCheck()
        {
            for (int i = 0; i < nRows; i++)
            {
                CellType[] rowCells = new CellType[nCols];
                for (int j = 0; j < nCols; j++)
                {
                    rowCells[j] = this[i, j];
                }
                if (AllEqual(rowCells, CellType.O))
                {
                    return Situation.O;
                }
                else if (AllEqual(rowCells, CellType.X))
                {
                    return Situation.X;
                }
            }
            return Situation.Playing;
        }

        Situation ColumnCheck()
        {
            for (int j = 0; j < nCols; j++)
            {
                CellType[] columnCells = new CellType[nCols];
                for (int i = 0; i < nRows; i++)
                {
                    columnCells[i] = this[i, j];
                }
                if (AllEqual(columnCells, CellType.O))
                {
                    return Situation.O;
                }
                else if (AllEqual(columnCells, CellType.X))
                {
                    return Situation.X;
                }
            }
            return Situation.Playing;
        }


        Situation HypotenuseCheck()
        {
            Situation result = LeftHypotenuseCheck();
            if (result != Situation.Playing)
            {
                return result;
            }
            result = RightHypotenuseCheck();
            return result;
        }

        Situation LeftHypotenuseCheck()
        {
            CellType[] hypotenuseCells = new CellType[nRows];
            for (int i = 0; i < nRows; i++)
            {
                hypotenuseCells[i] = this[i, i];
            }
            if (AllEqual(hypotenuseCells, CellType.O))
            {
                return Situation.O;
            }
            else if (AllEqual(hypotenuseCells, CellType.X))
            {
                return Situation.X;
            }
            return Situation.Playing;
        }

        Situation RightHypotenuseCheck()
        {
            CellType[] hypotenuseCells = new CellType[nRows];
            for (int i = 0; i < nRows; i++)
            {
                hypotenuseCells[i] = this[i, nCols - 1 - i];
            }
            if (AllEqual(hypotenuseCells, CellType.O))
            {
                return Situation.O;
            }
            else if (AllEqual(hypotenuseCells, CellType.X))
            {
                return Situation.X;
            }
            return Situation.Playing;
        }

        bool AllEqual(CellType[] cellArray, CellType type)
        {
            for (int i = 0; i < cellArray.Length; i++)
            {
                if (cellArray[i] != type)
                {
                    return false;
                }
            }
            return true;
        }

        public void Display()
        {
            Console.WriteLine("---------------");
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    char c = GetCellChar(this[i, j]);
                    /*if (c == ' ')
                    {
                        c = char.Parse((i * nCols + j + 1).ToString());
                    }*/
                    Console.Write("| {0} |", c);
                }
                Console.WriteLine("\n---------------");
            }
        }

        public void WriteData(StreamWriter writer, int position)
        {
            writer.Write(GetCellInt(cells[0]));
            for (int i = 1; i < cells.Length; i++)
            {
                writer.Write(',');
                writer.Write(GetCellInt(cells[i]));
            }
            writer.Write(',');
            writer.WriteLine(position);
        }

        int GetCellInt(CellType type)
        {
            if (type == CellType.O)
            {
                return -1;
            }
            else if (type == CellType.X)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        char GetCellChar(CellType type)
        {
            if (type == CellType.O)
            {
                return 'o';
            }
            else if (type == CellType.X)
            {
                return 'x';
            }
            else
            {
                return ' ';
            }
        }

        public void RandomSet(CellType type)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] == CellType.Empty)
                {
                    indices.Add(i);
                }
            }
            var rnd = new Random();
            int choice = indices[rnd.Next(indices.Count)];
            cells[choice] = type;
        }

    }

}

