// Name: (Su'Qoya Rhodes)
// CSC339 - Spring 2021
// Assignment 4

using System;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameBoard : Form
    {
        private Board board;
        private string playerOneName;
        private string playerTwoName;

        public GameBoard(string p1Name, string p2Name)
        {
            InitializeComponent();

            this.playerOneName = p1Name;
            this.playerTwoName = p2Name;

            //adds the event handler for when the screen is painted
            this.Paint += new PaintEventHandler(pic_board_Paint);
        }

        private void pic_board_Paint(object sender, PaintEventArgs e)
        {
            //gets called whenever the screen is painted
            board.DrawBoard(e.Graphics);
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {
            //gets called when the main form is loaded
            board = new Board();
            this.ChangeStatus();
        }

        //Quit Game
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        //Start a new game
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StartNewGame();
        }

        //Run on Column Button Click, which place a piece and change player turn
        private void ChangeTurn(int Column)
        {
            this.Paint -= new PaintEventHandler(pic_board_Paint);
            this.board.NextTurn(Column);
            this.ChangeStatus();
            this.Paint += new PaintEventHandler(pic_board_Paint);
            this.Refresh();

            if(this.board.result == Result.Draw)
            {
                MessageBox.Show("Match Draw! \nPress ok will start a new game", "Result", MessageBoxButtons.OK);
                this.StartNewGame();
            }
            if(this.board.result == Result.RedWin)
            {
                MessageBox.Show(playerOneName + " wins! \nPress ok will start a new game", "Result", MessageBoxButtons.OK);
                this.StartNewGame();
            }
            if(this.board.result == Result.YellowWin)
            {
                MessageBox.Show(playerTwoName + " wins! \nPress ok will start a new game", "Result", MessageBoxButtons.OK);
                this.StartNewGame();
            }
        }

        //Start New Game
        private void StartNewGame()
        {
            this.board.ResetBoard();
            this.Refresh();
            this.ChangeStatus();
        }

        //Change Game Status
        private void ChangeStatus()
        {
            if (this.board.turn == Player.RED)
                this.lblStatus.Text = "Status: " + playerOneName + "'s Turn, " + this.board.PlayerRedAttemptsLeft + " Pieces Left";
            else
                this.lblStatus.Text = "Status: " + playerTwoName + "'s Turn, " + this.board.PlayerYellowAttemptsLeft + " Pieces Left";
        }

        //Column Buttons Click Events
        private void btn_col1_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(0);
        }

        private void btn_col2_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(1);
        }

        private void btn_col3_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(2);
        }

        private void btn_col4_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(3);
        }

        private void btn_col5_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(4);
        }

        private void btn_col6_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(5);
        }

        private void btn_col7_Click(object sender, EventArgs e)
        {
            this.ChangeTurn(6);
        }
    }
}
