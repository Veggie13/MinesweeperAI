using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperAI
{
    class Minesweeper
    {
        private bool[,] _mines;
        private bool[,] _visible;
        private bool[,] _marked;
        private uint[,] _counts;
        public void Initialize(uint width, uint height, uint mines)
        {
            _mines = new bool[width, height];
            _visible = new bool[width, height];
            _marked = new bool[width, height];
            _counts = new uint[width, height];

            Random rand = new Random();
            for (uint i = 0; i < mines; i++)
            {
                uint col, row;
                do
                {
                    col = (uint)(width * rand.NextDouble());
                    row = (uint)(height * rand.NextDouble());
                } while (_mines[col, row]);

                _mines[col, row] = true;
                for (uint r = (row == 0) ? 0 : row - 1; r < height && r <= row + 1; r++)
                {
                    for (uint c = (col == 0) ? 0 : col - 1; c < width && c <= col + 1; c++)
                    {
                        _counts[c, r]++;
                    }
                }
            }
        }

        public uint Width
        {
            get { return (uint)((_mines == null) ? 0 : _mines.GetLength(0)); }
        }

        public uint Height
        {
            get { return (uint)((_mines == null) ? 0 : _mines.GetLength(1)); }
        }

        public int? this[uint col, uint row]
        {
            get
            {
                if (_visible[col, row])
                    return new int?((int)_counts[col, row]);
                if (_marked[col, row])
                    return -1;
                return null;
            }
        }

        public bool Satisfied(uint col, uint row)
        {
            uint open = 0, marked = 0;
            for (uint r = (row == 0) ? 0 : row - 1; r < Height && r <= row + 1; r++)
            {
                for (uint c = (col == 0) ? 0 : col - 1; c < Width && c <= col + 1; c++)
                {
                    if (r == row && c == col)
                        continue;
                    if (_marked[c, r])
                        marked++;
                    if (_visible[c, r])
                        open++;
                }
            }
            return (marked == _counts[col, row]) && (open == 8 - marked);
        }

        public bool Open(uint col, uint row)
        {
            _visible[col, row] = true;
            return _mines[col, row];
        }

        public void Mark(uint col, uint row)
        {
            _marked[col, row] = true;
        }
    }
}
