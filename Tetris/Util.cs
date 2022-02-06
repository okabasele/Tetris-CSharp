using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Util
    {
        public static int LinesCleared { get; set; } = 0;
        public static List<Piece> Pieces = new List<Piece>();


        public static void GetEmptyArrayGrid()
        {
            for (int i = 0; i < Grid.SizeGrid; i++)
            {
                for (int j = 0; j < Grid.SizeGrid; j++)
                {
                    Grid.ArrayGrid[i, j] = 0;
                    Grid.ArrayGrid[i, 0] = 1;
                    Grid.ArrayGrid[i, Grid.SizeGrid - 1] = 1;
                    Grid.ArrayGrid[0, j] = 1;
                    Grid.ArrayGrid[Grid.SizeGrid - 1, j] = 1;
                }
            }
            PrintArrayGrid();
        }

        //afficher la map du jeu
        public static void PrintArrayGrid()
        {
            for (int i = 0; i < Grid.SizeGrid; i++)
            {
                for (int j = 0; j < Grid.SizeGrid; j++)
                {
                    Console.Write(Grid.ArrayGrid[j, i]);
                    if (j != Grid.SizeGrid - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
            }
        }

        public static void AddPieceToGrid(Piece piece)
        {
            foreach (Coordinate coord in piece.Coord)
            {
                Grid.ArrayGrid[coord.x, coord.y] = piece.Id;

            }

        }


        public static int FindRowToRemoveFromGrid()
        {
            int[] rowToCheck = new int[Grid.SizeGrid];
            int[] empty = new int[Grid.SizeGrid];
            int indexToReturn = -1;
            for (int i = 0; i < Grid.SizeGrid; i++)
            {
                rowToCheck = Enumerable.Range(1, 8)
                .Select(x => Grid.ArrayGrid[x, i])
                .ToArray();
                //Console.WriteLine("LIGNE A CHECKER : [{0}]", string.Join(", ", rowToCheck));

                if (rowToCheck.All(value => value == 9))
                {
                    indexToReturn = i;
                    // Console.WriteLine(" VOILA LA LIGNE A ENLEVER : " + indexToReturn);
                    return indexToReturn;

                }
            }
            // Console.WriteLine(" IL NY A PAS DE LIGNE A ENLEVER : " + indexToReturn);

            return indexToReturn;
        }

        public static void ClearLineFromGrid(int toFind)
        {
            int[] tmp = new int[Grid.SizeGrid];
            int[] empty = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 };



            //row = row-1
            for (int i = toFind; i > 0; i--)
            {
                tmp = Enumerable.Range(0, Grid.ArrayGrid.GetLength(1))
                    .Select(x => Grid.ArrayGrid[x, i - 1])
                    .ToArray();

                InsertToGridAtIndex(i, tmp);
                if (i == toFind)
                {
                    //delete coord from piece
                    RemoveCoordFromPieceAtIndex(i);
                }
                else
                {
                    UpdateCoordPiece(i);
                }

            }
            InsertToGridAtIndex(1, empty);

            LinesCleared++;

        }

        public static void InsertToGridAtIndex(int whereToInsert, int[] arrayToInsert)
        {


            for (int j = 0; j < arrayToInsert.Length; j++)
            {
                Grid.ArrayGrid[j, whereToInsert] = arrayToInsert[j];
            }

        }

        public static void RemoveCoordFromPieceAtIndex(int whereToRemove)
        {
            for (int j = 0; j < Grid.ArrayGrid.Length; j++)
            {
                foreach (Piece piece in Pieces)
                {
                    foreach (Coordinate coord in piece.Coord.Where(c => j == c.x && whereToRemove == c.y).ToList())
                    {
                        piece.Coord.Remove(coord);
                    }
                }
            }
        }

        public static void UpdateCoordPiece(int whereToUpdate)
        {
            for (int j = 0; j < Grid.ArrayGrid.Length; j++)
            {
                foreach (Piece piece in Pieces)
                {
                    foreach (Coordinate coord in piece.Coord.Where(c => j == c.x && whereToUpdate == c.y).ToList())
                    {
                        coord.y++;
                    }
                }
            }
        }


        public static Piece GenerateRandomPiece()
        {

            Random random = new Random();
            switch (random.Next(4))
            {
                case 0: return new Square();
                case 1: return new Four();
                case 2: return new T();
                case 3: return new L();
            }

            return new Line();

        }

    }
}
