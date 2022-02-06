using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Four : Piece
    {
        public Four() : base()
        {
            this.Color = System.Drawing.ColorTranslator.FromHtml("#EA7BA7");
            GetCoordFromOnePoint();
        }

        void GetCoordFromOnePoint()
        {
            int x = this.Coord[0].x;
            int y = this.Coord[0].y;
            this.Coord.Add(new Coordinate(x + 1, y + 1));
            this.Coord.Add(new Coordinate(x, y + 1));
            this.Coord.Add(new Coordinate(x + 1, y + 2));

        }
    }
}
