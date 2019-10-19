using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using static CaputreTheDot.Flower;

namespace CaputreTheDot
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        TextView tvMovesCounter;
        CapsuleView[,] board;
        MainActivity.DifficultyLevel dl;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GameLayout);
            int difficulty = Intent.GetIntExtra("DIFFICULTY", 1);
            dl = (MainActivity.DifficultyLevel)difficulty;
            tvMovesCounter = FindViewById<TextView>(Resource.Id.tvMovesCounter);
            tvMovesCounter.Text = "Moves: 0";
            GameManager.GetGameManager().SetGameActivity(this);  
        }

        protected override void OnPause()
        {
            base.OnPause();
            Console.WriteLine("pause event and yaniv");
        }

        protected override void OnStart()
        {
            base.OnStart();
            DrawBoard();
            FillBoard();
            //Console.WriteLine("start event");
        }
        protected override void OnResume()
        {
            base.OnResume();
            //Console.WriteLine("resume event");
        }
        protected override void OnStop()
        {
            base.OnStop();
            Console.WriteLine("stop event");
        }

        private void FillBoard()
        {
           
            GameManager.GetGameManager().SetDotState(DotState.Computer, 4, 4);
            int dotCounter = 0;
            switch (dl)
            {
                case MainActivity.DifficultyLevel.Easy:
                    {
                        dotCounter = 30;
                        break;
                    }
                case MainActivity.DifficultyLevel.Medium:
                    {
                        dotCounter = 15;
                        break;
                    }
                case MainActivity.DifficultyLevel.Hard:
                    {
                        dotCounter = 10;
                        break;
                    }
                case MainActivity.DifficultyLevel.NotSelected:
                    {
                        throw new Exception("game started witout difficulty level");
                        //break;
                    }         
            }
            Random rnd = new Random();
            while (dotCounter > 0)
            {
                int x = rnd.Next(0, 9);
                int y = rnd.Next(0, 9);
                if (GameManager.GetGameManager().GetGameGraph().GetFlowerByBoardCordinate(
                    x,y).GetFlowerState() == DotState.Empty)
                {
                    GameManager.GetGameManager().GetGameGraph().GetFlowerByBoardCordinate(
                    x, y).SetFlowerState(DotState.Player);
                    this.board[x, y].SetCapsuleState(DotState.Player);
                    dotCounter--;
                }
            }
        }

        private void DrawBoard()
        {
            board = new CapsuleView[9, 9];
            GameManager.GetGameManager().GetGameGraph().ConnectBoardFlowers();
            RelativeLayout boardLayout = (RelativeLayout)FindViewById(Resource.Id.BoardLayout);
            //9 spaces and 9 circles(18 radius) + 3 radius for spacing the line,
            //spacing 2 radius for big, 1 for small
            DisplayMetrics displayMetrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetRealMetrics(displayMetrics);
            int screenWidth= displayMetrics.WidthPixels;
            int radius = (screenWidth - 9 * CapsuleView.SPACE) / 21;
            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    //async drawing
                    //Task.Run( () => {
                    //    DrawSingleCapsuleView(x, y, radius, boardLayout);
                    //});
                    //DrawSingleCapsuleView(x, y, radius, boardLayout);
                    CapsuleView cv = new CapsuleView(this, radius, x, y);
                    RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(
                      2 * cv.Radius,
                      2 * cv.Radius);
                    lp.LeftMargin = x * (2 * cv.Radius + CapsuleView.SPACE) + cv.Radius;
                    lp.TopMargin = y * (2 * cv.Radius) + cv.Radius;
                    if (y % 2 == 1)
                    {
                        lp.LeftMargin += cv.Radius;
                    }
                    lp.TopMargin += CapsuleView.OFFSET_TOP;
                    cv.LayoutParameters = lp;
                    boardLayout.AddView(cv);
                    cv.Click += CapsuleClicked;
                    board[x, y] = cv;
                }
            }
        }

        private void DrawSingleCapsuleView(int x, int y, int radius,RelativeLayout boardLayout)
        {
            CapsuleView cv = new CapsuleView(this, radius, x, y);
            RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(
              2 * cv.Radius,
              2 * cv.Radius);
            lp.LeftMargin = x * (2 * cv.Radius + CapsuleView.SPACE) + cv.Radius;
            lp.TopMargin = y * (2 * cv.Radius) + cv.Radius;
            if (y % 2 == 1)
            {
                lp.LeftMargin += cv.Radius;
            }
            lp.TopMargin += CapsuleView.OFFSET_TOP;
            cv.LayoutParameters = lp;
            boardLayout.AddView(cv);
            cv.Click += CapsuleClicked;
            board[x, y] = cv;
        }

        private void CapsuleClicked(object sender, EventArgs e)
        {
            CapsuleView cv = (CapsuleView)sender;
            if (cv.GetDotState() == DotState.Empty && GameManager.GetGameManager().GetIsPlayerTurn())
            {
                cv.SetCapsuleState(DotState.Player);
                GameManager.GetGameManager().GetGameGraph().GetFlowerByBoardCordinate(
                            cv.GetCordinate().BoardX, cv.GetCordinate().BoardY).SetFlowerState(DotState.Player);
                Task.Run(() =>
                {
                    GameManager.GetGameManager().SwitchTurn();
                    tvMovesCounter.Text = "Moves: " + GameManager.GetGameManager().GetMovesConter();
                });
                
            }
        }

        public void SetDotState(Flower.DotState ds, int x, int y)
        {
            this.board[x, y].SetCapsuleState(ds);
        }

      
    }
}   
