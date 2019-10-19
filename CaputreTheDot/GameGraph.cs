using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CaputreTheDot
{
    public class GameGraph 
    {
        Flower[,] board;
        Flower computer;
        public GameGraph()
        {
            //board[x,y]
            board = new Flower[9, 9];
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    Flower f = new Flower(x,y);
                    board[x, y] = f;
                }
            }
        }

        public Flower GetComputerFlower()
        {
            return this.computer;
        }
        public void SetComputerFlower(Flower computer)
        {
            this.computer = computer;
        }

        public Flower GetFlowerByBoardCordinate(int x, int y)
        {
            return board[x, y];
        }

        public void ConnectBoardFlowers()
        {
            ConnectRight();
            ConnectLeft();
            ConnectTopRight();
            ConnectTopLeft();
            ConnectBottonRight();
            ConnectBottonLeft();
        }
        private void ConnectRight()
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0) - 1; x++)
                {
                    board[x, y].SetRight(board[x + 1, y]);
                }
            }
        }
        private void ConnectLeft()
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 1; x < board.GetLength(0); x++)
                {
                    board[x, y].SetLeft(board[x - 1, y]);
                }
            }
        }
        private void ConnectTopRight()
        {
            for (int y = 1; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0) - 1; x++)
                {
                    if (y % 2 == 1)
                    {
                        board[x, y].SetTopRight(board[x + 1, y - 1]);
                    }
                    else
                    {
                        board[x, y].SetTopRight(board[x, y - 1]);
                    }

                }
            }
        }
        private void ConnectTopLeft()
        {
            for (int y = 1; y < board.GetLength(1); y++)
            {
                for (int x = 1; x < board.GetLength(0); x++)
                {
                    if (y %2 == 1)
                    {
                        board[x, y].SetTopLeft(board[x, y - 1]);
                    }
                    else
                    {
                        board[x, y].SetTopLeft(board[x - 1, y - 1]);
                    }
                    
                }
            }
        }
        private void ConnectBottonRight()
        {
            for (int y = 0; y < board.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < board.GetLength(0) - 1; x++)
                {
                    if (y % 2 == 1)
                    {
                        board[x, y].SetBottomRight(board[x + 1, y + 1]);
                    }
                    else
                    {
                        board[x, y].SetBottomRight(board[x, y + 1]);
                    }
                }
            }
        }
        private void ConnectBottonLeft()
        {
            for (int y = 0; y < board.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < board.GetLength(0); x++)
                {
                    if (y % 2 == 1)
                    {
                        board[x, y].SetBottomLeft(board[x, y + 1]);
                    }
                    else
                    {
                        board[x, y].SetBottomLeft(board[x - 1, y + 1]);
                    }
                }
            }
        }

        //public void PerformMovement(FlowerMovment fm)
        //{
        //    board[fm.GetMe().GetCordinate().BoardX, fm.GetDestenation().GetCordinate().BoardY].SetFlowerState(Flower.DotState.Empty);
        //    board[fm.GetDestenation().GetCordinate().BoardX, fm.GetDestenation().GetCordinate().BoardY].SetFlowerState(Flower.DotState.Computer);
        //    computer = board[fm.GetDestenation().GetCordinate().BoardX, fm.GetDestenation().GetCordinate().BoardY];
        //}

        public void SetDotState(Flower.DotState ds, int x, int y)
        {
            this.board[x, y].SetFlowerState(ds);
        }

    }
}