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
    public class FlowerMovment
    {
        Flower source;
        Flower destenation;
        public FlowerMovment(Flower source, Flower destenation)
        {
            this.source = source;
            this.destenation = destenation;
        }
        public Flower GetSource() { return source; }
        public Flower GetDestenation() { return destenation; }
        public override string ToString()
        {
            return source.GetCordinate() + " --> " + destenation.GetCordinate();
        }
    }
}