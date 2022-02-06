using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
namespace Tetris
{
    public partial class Form1 : Form
    {
        bool gameOn = false;
        Piece nextPiece = new Piece();
        private Timer aTimer;
        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            Util.GetEmptyArrayGrid();
            //panel2.BackColor = Color.FromArgb(25, Color.Black);
            button2.Visible = false;
            button3.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            panel3.Visible = false;

        }

        public void Launch()
        {
            gameOn = true;
            label2.Visible = true;
            label3.Visible = true;
            panel3.Visible = true;
            SetTimer();

        }

        void MakeTimerFaster()
        {

            if (Util.LinesCleared % 2 == 0)
            {
                aTimer.Interval -= 100;
            }

        }

        private void SetTimer()
        {
            // Create a timer
            aTimer = new Timer();
            aTimer.Interval = 1000;
            aTimer.Tick += OnTimedEvent;
            aTimer.Enabled = true;
        }
        private void OnTimedEvent(Object source, EventArgs e)
        {
            label2.Text = "Score : " + Util.LinesCleared;
            bool newPieceNeeded = true;
            Piece pieceAllowedToMove = new Piece();
            nextPiece = Util.GenerateRandomPiece();

            
            foreach (Piece piece in Util.Pieces)
            {
                if (piece.MouvementAllowed)
                {
                    newPieceNeeded = false;
                    pieceAllowedToMove = piece;
                }
            }

            if (newPieceNeeded)
            {
                //CHECK GRID
                int findLineToClear = Util.FindRowToRemoveFromGrid();
                if (findLineToClear > 0)
                {
                    Util.ClearLineFromGrid(findLineToClear);
                    label2.Text = "Score : " + Util.LinesCleared;
                    MakeTimerFaster();
                    panel1.Invalidate();

                }

                pieceAllowedToMove = nextPiece;
                CheckApparition(pieceAllowedToMove);
                Util.Pieces.Add(pieceAllowedToMove);
                if (!pieceAllowedToMove.MouvementAllowed)
                {
                    Util.Pieces.Remove(pieceAllowedToMove);
                }
                panel3.Invalidate();

            }

            if (!gameOn)
            {
                label1.Visible = true;
                aTimer.Enabled = false;
                button2.Visible = true;
                button3.Visible = true;
            }
            HandleMouv.HandleVerticalMouvement(pieceAllowedToMove);

            panel1.Invalidate();

        }
        void CheckApparition(Piece piece)
        {
            foreach (Coordinate coord in piece.Coord)
            {
                if (Grid.ArrayGrid[coord.x, coord.y] == 9)
                {
                    piece.MouvementAllowed = false;
                    gameOn = false;
                }
            }
        }
        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            Pen p = new Pen(Brushes.White, 2);
            Brush brush;
            Rectangle r;
            gr.ScaleTransform(Constants.SCALE, Constants.SCALE);
            for (int x = 0; x < Grid.SizeGrid; x++)
            {
                for (int y = 0; y < Grid.SizeGrid; y++)
                {
                    if (Grid.ArrayGrid[x, y] != 1)
                    {
                        r = new Rectangle(x * Constants.BLOCKSIZE, y * Constants.BLOCKSIZE, Constants.BLOCKSIZE, Constants.BLOCKSIZE);
                        gr.DrawRectangle(p, r);
                        foreach (Piece piece in Util.Pieces)
                        {
                            foreach (Coordinate coord in piece.Coord)
                            {
                                if (x == coord.x && y == coord.y)
                                {
                                    brush = new SolidBrush(piece.Color);
                                    gr.FillRectangle(brush, r);
                                }
                            }
                        }

                    }
                }
            }
        }

        //LES TOUCHES CLAVIERS
        private void HandlePieceMouvOnKeyDown(object sender, KeyEventArgs e)
        {
            if (gameOn)
            {
                foreach (Piece piece in Util.Pieces)
                {
                    if (piece.MouvementAllowed)
                    {
                        switch (e.KeyCode)
                        {

                            case Keys.Right: HandleMouv.HandleRightMouvement(piece); break;
                            case Keys.Left: HandleMouv.HandleLeftMouvement(piece); break;
                            case Keys.Up: HandleMouv.HandleRotateMouvement(piece); break;
                        }

                    }
                }

            }
        }

        // BUTTON START

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            Launch();
        }

        //BUTTON RESTART
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            Util.LinesCleared = 0;
            Util.Pieces.Clear();
            Util.GetEmptyArrayGrid();
            Launch();
        }

        //BUTTON EXIT
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //PAINT NEXT PIECE

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (gameOn)
            {
                Graphics gr = e.Graphics;
                Pen p = new Pen(Brushes.White, 2);
                Brush brush;
                Rectangle r;
                float scale = 1.5F;
                int blocksize = 5;
                gr.ScaleTransform(scale, scale);
                for (int x = 4; x < 8; x++)
                {
                    for (int y = 1; y < 4; y++)
                    {
                        r = new Rectangle(x * blocksize, y * blocksize, blocksize, blocksize);
                        foreach (Coordinate coord in nextPiece.Coord)
                        {
                            if (x == coord.x && y == coord.y)
                            {
                                brush = new SolidBrush(nextPiece.Color);
                                gr.FillRectangle(brush, r);
                            }
                        }

                    }
                }
            }
        }

    }
}
