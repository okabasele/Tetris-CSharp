using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Line : Piece
    {
        public Line() : base()
        {
            this.Color = System.Drawing.ColorTranslator.FromHtml("#8FEA7B");
            GetCoordFromOnePoint();
        }

        void GetCoordFromOnePoint()
        {
            int x = this.Coord[0].x;
            int y = this.Coord[0].y;
            this.Coord.Add(new Coordinate(x + 1, y));
            this.Coord.Add(new Coordinate(x + 2, y));
            this.Coord.Add(new Coordinate(x + 3, y));
        }
    }
}
