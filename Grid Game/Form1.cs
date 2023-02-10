/*
 *  
 * 
 * 
 * 
 * 
 */


// to-do: win checking, simple(ish) same system as validity checking however in a much more expanded fashion
// once the winner has been calculated, output diologue box with turns it took and who the winner is (possibly time too if I can implement it)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // dependencies
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Grid_Game
{
    public partial class Form1 : Form
    {
        Button[,] btn = new Button[7, 6]; // array of buttons
        Button play = new Button();
        Random r = new Random(); // new random reference (will potentially be used for "AI")

        int turn = 2; // global turn variable used to calculate which turn it is (can use this for win/lose checking, if turn == 44 then statemate)
        int playPause = 1; // used to check if music is playing or not

        public Form1()
        {
            InitializeComponent();
            this.Text = "Connect 4";
            this.BackColor = Color.LightGray;
            this.Width = 900;
            this.Height = 1000;
            play.SetBounds(700, 50, 100, 100);
            play.BackColor = Color.ForestGreen;
            play.Text = "Music!";
            play.Font = new Font("Arial", 18, FontStyle.Bold);
            play.Click += new EventHandler(play_Click);
            Controls.Add(play);
            createGrid();

        }

        void play_Click(object sender, EventArgs e)
        {
            playMusic();
        }

        private void playMusic()
        {
            SoundPlayer music = new SoundPlayer("C:/Users/naomi/source/repos/Grid-Game/Grid Game/Tell-me-you-know-8Bit.wav");
            // this file path will obviously throw and error any your system, just right click the .wav file within this folder and hit "copy as path", and replace the 
            // backslashes with forward slashes. this was the only way I could figure out to get it to work, every other file path would just throw and error
            if(playPause == 1)
            {
                music.Play();
                play.Text = "Stop!";
                play.BackColor = Color.OrangeRed;
                playPause++;
            }
            else
            {
                music.Stop();
                play.Text = "Play!";
                play.BackColor = Color.ForestGreen;
                playPause--;
            }
        }

        void btnEvent_Click(object sender, MouseEventArgs e, int x, int y) // event handler function
        {
            var me = e as MouseEventArgs; // var defining MouseEventArgs as mouseEvents won't hold information on both left and right mousebuttons
            var pressedBtn = (Button)sender; // defining button pressed data to reduce inputs from (((buton)sender)... 

            

            Console.Write("X = " + x); // debug 
            Console.Write("Y = " + y);
            

            if (checkBelow(x, y) == true && e.Button == MouseButtons.Right && turn % 2 == 0) // if checkbelow = true and pressedBtn = right and turn = even
            {
                if (pressedBtn.BackColor == Color.White) // if button = white
                {
                    pressedBtn.BackColor = Color.Yellow; // turn button yellow 
                    turn++; // iterate 
                    
                }
                else
                {
                    Console.WriteLine("Invalid Move"); // mainly debug
                }
            }
            if (checkBelow(x, y) == true && e.Button == MouseButtons.Left && turn % 2 != 0)
            {
                if (pressedBtn.BackColor == Color.White)
                {
                    pressedBtn.BackColor = Color.Red;
                    turn++;
                }
                else
                {
                    Console.WriteLine("Invalid Move");
                }
            }

            checkWin();


        }

        void createGrid() // create grid function, not really necessary but easier to read
        {
            for (int x = 0; x < 7; x++)
            { // nested for loop for building grid
                for (int y = 0; y < 6; y++)
                {
                    btn[x, y] = new Button(); 
                    btn[x, y].SetBounds(100 + (100 * x), 200 + (100 * y), 100, 100); 
                    btn[x, y].BackColor = Color.White;
                    int localX = x, localY = y; // provides current coordinates of the button pressed when it is clicked
                    btn[x, y].MouseDown += (s, ev) => btnEvent_Click(s, ev, localX, localY); // lambda expression to not need to specifiy data being passed
                    Controls.Add(btn[x, y]);
                }
            }
        }

        private bool checkBelow(int x, int y)
        {
            int i = y+1;
            if (y + 1 == 6 || btn[x,i].BackColor == Color.Yellow || btn[x, i].BackColor == Color.Red)
            { // effectively if space below occupied or bottom of grid, place square
                return true; // returns true to calling function where validity checking
            }
            else
            {
                return false; // returns false, due to turn not iterating is still the player's turn who attempted the misplaced piece 
            }
        }

        // need to check left 3 additional spaces, 3 additional spaces right
        // need to check above 3 additional spaces, 3 additional spaces below
        // need to check in each diagonal along with picking up if the piece places is in the middle of a winning line

        // call this checkwin function every time a piece is played, could only call if turn = 10, meaning each player has placed 4 pieces
        // however this feels redundant 

        private void checkWin()
        {
            // checking for horizontal win conditions
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x+1, y].BackColor == Color.Red && btn[x+2, y].BackColor == Color.Red && btn[x+3, y].BackColor == Color.Red)
                    {
                        Console.WriteLine("Red Wins Horizontally");
                    }

                    else if (btn[x, y].BackColor == Color.Yellow && btn[x + 1, y].BackColor == Color.Yellow && btn[x + 2, y].BackColor == Color.Yellow && btn[x + 3, y].BackColor == Color.Yellow)
                    {
                        Console.WriteLine("Yellow Wins Horizontally");
                    }

                 
                }
            }

            // checking for vertical win conditions
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (btn[x, y].BackColor == Color.Yellow && btn[x, y + 1].BackColor == Color.Yellow && btn[x, y + 2].BackColor == Color.Yellow && btn[x, y + 3].BackColor == Color.Yellow)
                    {
                        Console.WriteLine("Yellow Wins Vertically");
                    }

                    else if (btn[x, y].BackColor == Color.Red && btn[x, y + 1].BackColor == Color.Red && btn[x, y + 2].BackColor == Color.Red && btn[x, y + 3].BackColor == Color.Red)
                    {
                        Console.WriteLine("Red Wins Vertically");
                    }
                }
            }

            // checking for diagonal win condition (sloping up left to right)

            for (int x = 3; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x-1, y+1].BackColor == Color.Red && btn[x-2, y+2].BackColor == Color.Red && btn[x-3, y+3].BackColor == Color.Red)
                    {
                        Console.WriteLine("Red Wins Diagonally");
                    }

                    else if (btn[x, y].BackColor == Color.Yellow && btn[x - 1, y + 1].BackColor == Color.Yellow && btn[x - 2, y + 2].BackColor == Color.Yellow && btn[x - 3, y + 3].BackColor == Color.Yellow)
                    {
                        Console.WriteLine("Yellow Wins Diagonally");
                    }
                }
            }

            //checking for diagonal win conditions (sloping down left to right)

            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y < 6; y++)
                {
                    if (btn[x, y].BackColor == Color.Red && btn[x+1, y-1].BackColor == Color.Red && btn[x + 2, y - 2].BackColor == Color.Red && btn[x + 3, y - 3].BackColor == Color.Red)
                    {
                        Console.WriteLine("Red Wins Diagonally");
                    }

                    else if (btn[x, y].BackColor == Color.Yellow && btn[x+1, y-1].BackColor == Color.Yellow && btn[x + 2, y - 2].BackColor == Color.Yellow && btn[x + 3, y - 3].BackColor == Color.Yellow)
                    {
                        Console.WriteLine("Yellow Wins Diagonally");
                    }
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}