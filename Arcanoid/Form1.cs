using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Arcanoid
{
    struct Pos
    {
        public int x;
        public int y;

        public bool life;
    }

    public partial class Form1 : Form
    {

        private List<Pos> cube = new List<Pos>();

        private int szX = 50, szY = 40;

        Graphics g;

        private int xPos, yPos;
        private int xMove, yMove;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(Width, Height);
            g = Graphics.FromImage(pictureBox1.Image);

            xPos = Width / 2;
            yPos = Height - 50;

            int t = 12;
            int tt = 7;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cube.Add(new Pos() { x = 15 + t + i * szX, y = 20 + tt + j * szY, life = true });
                    tt += 5;
                }
                tt = 7;
                t += 12;
            }

            xMove = 2; // rand.Next(-3,4);
            yMove = 2; // rand.Next(-3, 4);

            foreach (var item in cube)
            {
                g.DrawRectangle(new Pen(Color.Green), item.x, item.y, szX, szY);
            }



            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            xPos += xMove;
            yPos += yMove;

            foreach (var item in cube)
            {
                if (item.life == true)
                    g.DrawRectangle(new Pen(Color.Green), item.x, item.y, szX, szY);
            }
            g.FillRectangle(Brushes.Blue, xPos, yPos, 5, 5);

            ProvBreak();

            ProvStuch();

            pictureBox1.Invalidate();
        }

        private void ProvBreak()
        {
            for (int i = 0; i < cube.Count; i++)
            {
                if (cube[i].life == true)
                {
                    if (cube[i].x < xPos + 5 && cube[i].x + szX > xPos
                        && cube[i].y < yPos + 5 && cube[i].y + szY > yPos)
                    {
                        if (yPos > cube[i].y - 3 + szY || yPos < cube[i].y + 3)
                        {
                            if (szY / 2 > yPos)
                            {
                                yMove = yMove * -1;
                                cube[i] = new Pos() { x = -500, y = -500, life = false };
                            }
                            else if (szY / 2 < yPos)
                            {
                                yMove = yMove * -1;
                                cube[i] = new Pos() { x = -500, y = -500, life = false };
                            }
                        }
                        else if (xPos >= cube[i].x - 3 + szX || xPos <= cube[i].x + 3)
                        {
                            if (szX / 2 > xPos)
                            {
                                xMove = xMove * -1;
                                cube[i] = new Pos() { x = -500, y = -500, life = false };
                            }
                            else if (szX / 2 < xPos)
                            {

                                xMove = xMove * -1;
                                cube[i] = new Pos() { x = -500, y = -500, life = false };
                            }
                        }

                    }
                }
            }

        }

        private void ProvStuch()
        {
            if (xPos < 0)
            {
                xMove = Math.Abs(xMove);
            }
            else if (xPos + 5 > Width)
            {
                xMove = Math.Abs(xMove) * -1;
            }

            if (yPos < 0)
            {
                yMove = Math.Abs(yMove);
            }
            else if (yPos + 5 > Height)
            {
                yMove = Math.Abs(yMove) * -1;
            }
        }
    }
}