using System;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.textBoxPlayerOneName.Text.Trim() != "" && this.textBoxPlayerTwoName.Text.Trim() != "")
            {
                GameBoard gb = new GameBoard(this.textBoxPlayerOneName.Text, this.textBoxPlayerTwoName.Text);
                gb.ShowDialog();
            }
        }
    }
}
