using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingTactics
{
    class Fleet
    {
        public void AddShip(IEnumerable<Square> squares)
        {
            ships.Add(new Ship(squares));
        }

        public IEnumerable<Ship> Ships
        {
            get { return ships; }
        }

        public HitResult IsHit(Square square)
        {
            foreach (var ship in Ships)
            {
                HitResult hit = ship.IsHit(square);
                if (hit != HitResult.Missed)
                    return hit;
            }
            return HitResult.Missed;
        }

        private List<Ship> ships = new List<Ship>();
    }
}
