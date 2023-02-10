using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid_Game
{
    public partial class MenuScreen : Form
    {
        public MenuScreen()
        {
            InitializeComponent();
            this.Text = "Connect 4";
            this.BackColor = Color.LightGray;
            this.Width = 900;
            this.Height = 1000;

        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            game.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            DialogResult rules;
            rules = MessageBox.Show("Player 1 uses right click and player 2 uses left click.\nThe goal is to get 4 of your own tile in a row before the other player. This can be achieved horizontaly, verticaly or diagonaly.\n", "Rules", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
