using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static CaputreTheDot.Flower;

namespace CaputreTheDot
{
    public class CapsuleView : View
    {
        public int Radius;
        public const int SPACE = 15;
        public const int OFFSET_X = 0;
        public const int OFFSET_TOP = 150;
        //int boardX;
        //int boardY;
        DotState capsuleState;
        BoardCordinate cord;
        public CapsuleView(Context context, int radius, int x, int y) : base(context)
        {
            this.Radius = radius;
            this.capsuleState = DotState.Empty;

            this.cord = new BoardCordinate();
            cord.BoardX = x;
            cord.BoardY = y;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            Paint paintCircle = new Paint();
            switch (this.capsuleState)
            {
                case DotState.Empty:
                    {
                        paintCircle.Color = Color.Gray;  
                        break;
                    }
                case DotState.Computer:
                    {
                        paintCircle.Color = Color.Blue;
                        break;
                    }
                case DotState.Player:
                    {
                        paintCircle.Color = Color.Orange;
                        break;
                    }
            }
            canvas.DrawCircle(Radius, Radius, Radius, paintCircle);
        }

        public void SetCapsuleState(DotState ds)
        {
            if (this.capsuleState == DotState.Player && ds == DotState.Computer)
            {
                throw new Exception("overrride the player dot");
                //Console.WriteLine("overrride the player dot");
            }
            this.capsuleState = ds;
            //calls the onDraw() again
            this.Invalidate();
        }

        public DotState GetDotState()
        {
            return this.capsuleState;
        }

        public BoardCordinate GetCordinate()
        {
            return this.cord;
        }




    }
}