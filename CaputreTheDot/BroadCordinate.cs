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
    public class BoardCordinate
    {
        public BoardCordinate()
        {
            BoardX = 0;
            BoardY = 0;
        }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        public override string ToString()
        {
            return String.Format("[{0}, {1}]", BoardX, BoardY);
        }

    }
}