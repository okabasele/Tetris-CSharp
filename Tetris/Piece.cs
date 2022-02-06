using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class Piece
    {

        public List<Coordinate> Coord { get; set; } = new List<Coordinate>();
        public int Id { get; set; } = 2;
        public Color Color { get; set; }
        public bool MouvementAllowed { get; set; } = true;
        public bool Spawned { get; set; } = true;
        public bool CanRotate { get; set; } = true;
        public bool CanMoveRight { get; set; } = true;
        public bool CanMoveLeft { get; set; } = true;

        public Piece()
        {
            this.Coord.Add(new Coordinate(4, 1));
        }

        public List<Coordinate> GetCopyCoordinate()
        {
            List<Coordinate> coord = new List<Coordinate>
            {
                new Coordinate(Coord[0].x, Coord[0].y),
                new Coordinate(Coord[1].x, Coord[1].y),
                new Coordinate(Coord[2].x, Coord[2].y),
                new Coordinate(Coord[3].x, Coord[3].y)
            };
            return coord;
        }

        public void RotateTemporary()
        {


            Coordinate pivot = new Coordinate(Coord[1].x, Coord[1].y);
            Coordinate tmp = new Coordinate(0, 0);
            foreach (Coordinate coord in Coord)
            {
                tmp.x = (coord.y + pivot.x - pivot.y);
                tmp.y = (pivot.x + pivot.y - coord.x - 1);
                coord.x = tmp.x;
                coord.y = tmp.y;
            }


        }
        public void Rotate()
        {


            Coordinate pivot = new Coordinate(Coord[1].x, Coord[1].y);
            Coordinate tmp = new Coordinate(0, 0);
            foreach (Coordinate coord in Coord)
            {
                Grid.ArrayGrid[coord.x, coord.y] = 0;
                tmp.x = (coord.y + pivot.x - pivot.y);
                tmp.y = (pivot.x + pivot.y - coord.x - 1);
                coord.x = tmp.x;
                coord.y = tmp.y;
            }


        }

    }
}
