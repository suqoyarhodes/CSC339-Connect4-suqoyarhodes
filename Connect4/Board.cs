using System;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    enum Player
    {
        EMPTY, RED, YELLOW
    }

    enum Result
    {
        None, RedWin, YellowWin, Draw 
    }

    class Board
    {
        private const int rows = 6;
        private const int cols = 7;
        private Player[,] board;

        public int PlayerRedAttemptsLeft;
        public int PlayerYellowAttemptsLeft;
        public Player turn;
        public Result result;

        public Board()
        {
            //constructor
            ResetBoard();
        }

        public void ResetBoard()
        {
            board = new Player[rows, cols];
            this.PlayerRedAttemptsLeft = 21;
            this.PlayerYellowAttemptsLeft = 21;
            turn = Player.RED;
            result = Result.None;

            //reset the game board to empty
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = Player.EMPTY;
                }
            }
            
        }

        public void DrawBoard(Graphics g)
        {
            int start = 50; //board top left corner offset

            g.FillRectangle(Brushes.Navy, start, start, 700, 600);

            //draw the board columns
            for(int i = 100;i<=600;i = i+100)
                g.DrawLine(Pens.White, start + i, start, start + i, start + 600);

            //draw the board rows
            for (int i = 100; i <= 500; i = i + 100)
                g.DrawLine(Pens.White, start, start + i, start + 700, start + i);


            for (int i= 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == Player.RED)
                    {
                        g.FillEllipse(Brushes.Red, (start + 5) + (j * 100), (start + 5) + (i * 100), 90, 90);
                    }
                    else if (board[i, j] == Player.YELLOW)
                    {
                        g.FillEllipse(Brushes.Yellow, (start + 5) + (j * 100), (start + 5) + (i * 100), 90, 90);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.White, (start + 5) + (j * 100), (start + 5) + (i * 100), 90, 90);
                    }
                }
            }
        }

        //To mark piece red and yellow
        public void NextTurn(int column) 
        {
            bool flag = false;
            for (int row = rows - 1; row >= 0; --row)
            {
                if(board[row, column] == Player.EMPTY)
                {
                    board[row, column] = this.turn;

                    if (turn == Player.RED)
                    {
                        turn = Player.YELLOW;
                        PlayerRedAttemptsLeft -= 1;
                    }
                    else
                    {
                        turn = Player.RED;
                        PlayerYellowAttemptsLeft -= 1;
                    }

                    CheckWin();

                    flag = true;
                    break;
                }
            }
            if(!flag) MessageBox.Show("Column Already full", "Overflow", MessageBoxButtons.OK);
        }

        private void CheckWin()
        {
            if (PlayerRedAttemptsLeft == 0 && PlayerYellowAttemptsLeft == 0)
            {
                result = Result.Draw;
            }
            else
            {
                //Checking Win of any player in columns
                for (int j = 0; j < cols; ++j)
                {
                    for (int i = rows - 1; i >= 3; --i)
                    {
                        if (board[i, j] == Player.EMPTY)
                            break;
                        else if (board[i, j] == Player.RED && board[i - 1, j] == Player.RED && board[i - 2, j] == Player.RED && board[i - 3, j] == Player.RED)
                        {
                            result = Result.RedWin;
                            return;
                        }
                        else if (board[i, j] == Player.YELLOW && board[i - 1, j] == Player.YELLOW && board[i - 2, j] == Player.YELLOW && board[i - 3, j] == Player.YELLOW)
                        {
                            result = Result.YellowWin;
                            return;
                        }
                    }
                }

                //Checking Win of any player in rows
                for (int i = rows - 1; i >= 0; --i)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        if (board[i, j] == Player.RED && board[i, j + 1] == Player.RED && board[i, j + 2] == Player.RED && board[i, j + 3] == Player.RED)
                        {
                            result = Result.RedWin;
                            return;
                        }
                        else if (board[i, j] == Player.YELLOW && board[i, j + 1] == Player.YELLOW && board[i, j + 2] == Player.YELLOW && board[i, j + 3] == Player.YELLOW)
                        {
                            result = Result.YellowWin;
                            return;
                        }
                    }
                }

                //Checking Win of any player diagonally from right
                for(int i = 3; i < rows; ++i)
                {
                    for(int j = 0; j < cols - 3; ++j)
                    {
                        if(board[i, j] == Player.YELLOW && board[i - 1, j + 1] == Player.YELLOW && board[i - 2, j + 2] == Player.YELLOW && board[i - 3, j + 3] == Player.YELLOW)
                        {
                            result = Result.YellowWin;
                            return;
                        }
                        else if(board[i, j] == Player.RED && board[i - 1, j + 1] == Player.RED && board[i - 2, j + 2] == Player.RED && board[i - 3, j + 3] == Player.RED)
                        {
                            result = Result.RedWin;
                            return;
                        }
                    }
                }

                //Checking Win of any player diagonally from opposite
                for (int i = 0; i < rows - 3; ++i)
                {
                    for (int j = 0; j < cols - 3; ++j)
                    {
                        if (board[i, j] == Player.YELLOW && board[i + 1, j + 1] == Player.YELLOW && board[i + 2, j + 2] == Player.YELLOW && board[i + 3, j + 3] == Player.YELLOW)
                        {
                            result = Result.YellowWin;
                            return;
                        }
                        else if (board[i, j] == Player.RED && board[i + 1, j + 1] == Player.RED && board[i + 2, j + 2] == Player.RED && board[i + 3, j + 3] == Player.RED)
                        {
                            result = Result.RedWin;
                            return;
                        }
                    }
                }
            }
        }
    }
}
