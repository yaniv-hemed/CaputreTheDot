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
    class PathNode
    {
        public PathNode Prev { get;  }
        public Flower CurrentFlower { get;  }
        public PathNode(PathNode prev, Flower CurrentFlower)
        {
            this.Prev = prev;
            this.CurrentFlower = CurrentFlower;
           //Console.WriteLine(this);
        }

        public bool Equals(PathNode other)
        {
            return this.CurrentFlower.GetCordinate() == other.CurrentFlower.GetCordinate();
        }

        public bool IsVisitedBefore()
        {
            PathNode p;

            p = Prev;
            //if (this != null && this.Prev == null)
            //{
            //    return false; // the root case (first hop)
            //}
            while (p.Prev != null && !p.Equals(this))
            {
                p = p.Prev;
            }
            if (p.Prev == null) // we reach the root
            {
                return false;
            }
            return true; // we visited here 
        }

        public FlowerMovment TransformRootToMovement()
        {
            PathNode oneBeforeRoot = this;
            while (oneBeforeRoot.Prev.Prev != null)
            {
                oneBeforeRoot = oneBeforeRoot.Prev;
            }
            FlowerMovment flm = new FlowerMovment
                (oneBeforeRoot.Prev.CurrentFlower,
                 oneBeforeRoot.CurrentFlower);
            Console.WriteLine(oneBeforeRoot);
            return flm;

        }

        public override string ToString()
        {
            if (Prev != null)
            {
                return this.CurrentFlower.GetCordinate() + " --> " + this.Prev.ToString();
            }
            else
            {
                return "{[ROOT]}";
            }
        }



    }
}