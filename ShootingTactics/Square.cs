using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ShootingTactics
{
    class Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public readonly int Row;
        public readonly int Column;
        public bool Hit = false;

        public bool Equals(Square other)
        {
            if (other == null)
                return false;
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }

        public static bool operator==(Square lhs, Square rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator!=(Square lhs, Square rhs)
        {
            return !(lhs == rhs);
        }
    }
}
