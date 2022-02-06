using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return String.Format("Coord (X = {0}, Y={1})",x,y);
        }

    }
}
