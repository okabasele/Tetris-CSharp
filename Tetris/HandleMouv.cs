using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class HandleMouv
    {
        public static void HandleVerticalMouvement(Piece piece)
        {
            if (piece.Spawned && piece.MouvementAllowed)
            {
                piece.Spawned = false;
                Util.AddPieceToGrid(piece);

            }
            else
            {
                CheckVerticalMouvement(piece);

                if (piece.MouvementAllowed)
                {
                    foreach (Coordinate coord in piece.Coord)
                    {
                        Grid.ArrayGrid[coord.x, coord.y] = 0;
                        coord.y++;
                    }

                }
                Util.AddPieceToGrid(piece);

            }

            Util.PrintArrayGrid();
        }

        public static void HandleRightMouvement(Piece piece)
        {
            if (piece.Spawned && piece.MouvementAllowed)
            {
                piece.Spawned = false;
                Util.AddPieceToGrid(piece);

            }
            else
            {
                CheckVerticalMouvement(piece);
                CheckRightMouvement(piece);

                if (piece.CanMoveRight && piece.MouvementAllowed)
                {
                    foreach (Coordinate coord in piece.Coord)
                    {
                        Grid.ArrayGrid[coord.x, coord.y] = 0;
                        coord.x++;
                    }

                }
                Util.AddPieceToGrid(piece);

            }

            Util.PrintArrayGrid();
        }
        public static void HandleLeftMouvement(Piece piece)
        {
            if (piece.Spawned && piece.MouvementAllowed)
            {
                piece.Spawned = false;
                Util.AddPieceToGrid(piece);

            }
            else
            {
                CheckVerticalMouvement(piece);
                CheckLeftMouvement(piece);

                if (piece.CanMoveLeft && piece.MouvementAllowed)
                {
                    foreach (Coordinate coord in piece.Coord)
                    {
                        Grid.ArrayGrid[coord.x, coord.y] = 0;
                        coord.x--;
                    }

                }

                Util.AddPieceToGrid(piece);

            }
            Util.PrintArrayGrid();
        }

        public static void HandleRotateMouvement(Piece piece)
        {
            if (piece.Spawned && piece.MouvementAllowed)
            {
                piece.Spawned = false;
                Util.AddPieceToGrid(piece);
            }
            else
            {

                CheckVerticalMouvement(piece);
                CheckRotateMouvement(piece);

                if (piece.CanRotate && piece.MouvementAllowed)
                {
                    piece.Rotate();

                }

                Util.AddPieceToGrid(piece);
            }
            Util.PrintArrayGrid();
        }


        //CHECK MOUVEMENTS

        public static void CheckVerticalMouvement(Piece piece)
        {
            foreach (Coordinate coord in piece.Coord)
            {
                if (Grid.ArrayGrid[coord.x, coord.y + 1] == 1 ||
                        Grid.ArrayGrid[coord.x, coord.y + 1] == 9)
                {
                    piece.MouvementAllowed = false;
                    piece.Id = 9;
                    break;
                }
            }

        }
        public static void CheckRotateMouvement(Piece piece)
        {

            if (System.ComponentModel.TypeDescriptor.GetClassName(piece).Contains("Square"))
            {
                return;
            }

            bool isAllowed = true;
            Piece tmp = new Piece
            {
                Coord = piece.GetCopyCoordinate(),
            };

            tmp.RotateTemporary();

            foreach (Coordinate coord in tmp.Coord)
            {
                if (coord.x < 1 || coord.y < 1 || coord.x > Grid.SizeGrid || coord.y > Grid.SizeGrid)
                {
                    piece.CanRotate = false;
                    isAllowed = false;
                    break;

                }
                else if (Grid.ArrayGrid[coord.x, coord.y] == 1 ||
                        Grid.ArrayGrid[coord.x, coord.y] == 9)
                {
                    piece.CanRotate = false;
                    isAllowed = false;
                    break;

                }
            }
            if (isAllowed)
            {
                piece.CanRotate = true;

            }
        }

        public static void CheckRightMouvement(Piece piece)
        {
            bool isAllowed = true;
            foreach (Coordinate coord in piece.Coord)
            {
                if (Grid.ArrayGrid[coord.x + 1, coord.y] == 1 ||
                        Grid.ArrayGrid[coord.x + 1, coord.y] == 9)
                {
                    piece.CanMoveRight = false;
                    isAllowed = false;
                    break;
                }
            }
            if (isAllowed)
            {
                piece.CanMoveRight = true;

            }

        }

        public static void CheckLeftMouvement(Piece piece)
        {
            bool isAllowed = true;
            foreach (Coordinate coord in piece.Coord)
            {
                if (Grid.ArrayGrid[coord.x - 1, coord.y] == 1 ||
                        Grid.ArrayGrid[coord.x - 1, coord.y] == 9)
                {
                    piece.CanMoveLeft = false;
                    isAllowed = false;
                    break;
                }
            }
            if (isAllowed)
            {
                piece.CanMoveLeft = true;

            }

        }

    }
}
