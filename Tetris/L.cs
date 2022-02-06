using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class L : Piece
    {
        public L() : base()
        {
            this.Color = System.Drawing.ColorTranslator.FromHtml("#FAB554");

            GetCoordFromOnePoint();
        }

        void GetCoordFromOnePoint()
        {
            int x = this.Coord[0].x;
            int y = this.Coord[0].y;
            this.Coord.Add(new Coordinate(x, y + 1));
            this.Coord.Add(new Coordinate(x, y + 2));
            this.Coord.Add(new Coordinate(x + 1, y + 2));


        }
    }
}
