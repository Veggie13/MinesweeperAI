using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperAI
{
    class AI
    {
        public static void Strategy(Minesweeper game, out uint col, out uint row)
        {
        }

        static bool Truth(Minesweeper game, uint col, uint row, ref uint rcol, ref uint rrow, ref bool impossible)
        {
            impossible = false;
            uint unopened = 0, marked = 0;
            for (uint r = (row == 0) ? 0 : row - 1; r < game.Height && r <= row + 1; r++)
            {
                for (uint c = (col == 0) ? 0 : col - 1; c < game.Width && c <= col + 1; c++)
                {
                    if (r == row && c == col)
                        continue;
                    if (game[c, r] == null)
                        unopened++;
                    if (game[c, r] < 0)
                        marked++;
                }
            }

            uint remaining = (uint)game[col, row] - marked;
            if (unopened < remaining)
            {
                impossible = true;
                return true;
            }
            else if (unopened == remaining)
            {
                for (uint r = (row == 0) ? 0 : row - 1; r < game.Height && r <= row + 1; r++)
                {
                    for (uint c = (col == 0) ? 0 : col - 1; c < game.Width && c <= col + 1; c++)
                    {
                        if (r == row && c == col)
                            continue;
                        if (game[c, r] == null)
                            game.Mark(c, r);
                    }
                }
            }
            else if (unopened > 0 && remaining == 0)
            {
                for (uint r = (row == 0) ? 0 : row - 1; r < game.Height && r <= row + 1; r++)
                {
                    for (uint c = (col == 0) ? 0 : col - 1; c < game.Width && c <= col + 1; c++)
                    {
                        if (r == row && c == col)
                            continue;
                        if (game[c, r] == null)
                        {
                            rcol = c;
                            rrow = r;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool Contrapositive(Minesweeper game, uint col, uint row, ref uint rcol, ref uint rrow)
        {

        }
    }
}
