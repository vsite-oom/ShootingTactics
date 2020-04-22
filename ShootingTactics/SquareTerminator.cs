using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingTactics
{
    class SquareTerminator
    {
        public SquareTerminator(Grid grid)
        {
            this.grid = grid;
        }

        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            int left = shipSquares.First().Column;
            if (left > 0)
                --left;
            int top = shipSquares.First().Row;
            if (top > 0)
                --top;

            int right = shipSquares.Last().Column + 1;
            if (right < grid.Columns)
                ++right;
            int bottom = shipSquares.Last().Row + 1;
            if (bottom < grid.Rows)
                ++bottom;

            List<Square> toEliminate = new List<Square>();
            for (int r = top; r < bottom; ++r)
            {
                for (int c = left; c < right; ++c)
                    toEliminate.Add(new Square(r, c));
            }
            return toEliminate;
        }

        private readonly Grid grid;
    }
}
