using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid_Game
{
    public partial class Form1 : Form
    {
        Button[,] btn = new Button[7, 6];
        Random r = new Random();
        int turn = 2;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Connect 4";
            this.BackColor = Color.LightGray;
            this.Width = 900;
            this.Height = 800;

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    btn[x, y] = new Button();
                    btn[x, y].SetBounds(100 + (100 * x), 75 + (100 * y), 100, 100);
                    btn[x, y].BackColor = Color.White;
                    btn[x, y].Text = "C"; // just for error checking, using the codes: Y for yellow peice, R for red peice and C for clear.
                    btn[x, y].MouseDown += (s, ev) => btnEvent_Click(s, ev, x, y);
                    Controls.Add(btn[x, y]);
                }
            }
        }

        void btnEvent_Click(object sender, MouseEventArgs e, int x, int y) 
        {
            var me = e as MouseEventArgs; 
            var pressedBtn = (Button)sender; // Assigning button value sent to a var

            if (e.Button == MouseButtons.Right && turn % 2 == 0) // extensive error checking (can be cleaned up in future)
            {
                if (pressedBtn.BackColor == Color.White)
                {
                    if (y <= 6 && y >= 0)
                    {
                        pressedBtn.BackColor = Color.Yellow;
                        pressedBtn.Text = "Y";
                        turn++; // incrementing turn every pass
                    }
                    else
                    {
                        Console.WriteLine("Invalid Move: Attemped game peice outside bounds of array");
                    }
                }  // these console messages can be introduced into message boxes in the future
                else
                {
                    Console.WriteLine("Invalid Move: Attempted game peice on already existing peice"); 
                }
            }
            if (e.Button == MouseButtons.Left && turn % 2 != 0) // testing for odd or even number using mod 
            {
                if (pressedBtn.BackColor == Color.White)
                {
                    if (y <= 6 && y >= 0)
                    {
                        pressedBtn.BackColor = Color.Red;
                        pressedBtn.Text = "R";
                        turn++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Move: Attemped game peice outside bounds of array");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Move: Attempted game peice on already existing peice");
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
