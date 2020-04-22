using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingTactics
{
    enum HitResult
    {
        Missed,
        Hit,
        Sunk
    }
    class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public HitResult IsHit(Square square)
        {
            if (!Squares.Contains(square))
                return HitResult.Missed;
            var sq = Squares.First(s => s == square);
            sq.Hit = true;
            bool sunk = Squares.Count(s => s.Hit == true) == Squares.Count();
            return sunk ? HitResult.Sunk : HitResult.Hit;
        }

        public readonly IEnumerable<Square> Squares;
    }
}
