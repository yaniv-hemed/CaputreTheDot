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
    public class Flower
    {
        public enum DotState
        {
            Empty = 0, //gray
            Player = 1, //orange
            Computer = 2 //blue
        };
        
        BoardCordinate cord;
        DotState state;
        Flower[] flowers;
        /*
         *        5   0
         *       4 this 1
         *        3   2
         */

        public Flower(int x, int y)
        {
            flowers = new Flower[6];
            cord = new BoardCordinate();
            cord.BoardX = x;
            cord.BoardY = y;
        }
        public void SetFlowerState(DotState s)
        {
            if (this.state == DotState.Player && s == DotState.Computer)
            {
                throw new Exception("overrride the player dot");
                //Console.WriteLine("overrride the player dot");
            }
            this.state = s;
        }
        public bool IsEscapeable()
        {   
            //check for the edge of the board
            for (int i = 0; i < flowers.Length; i++)
            {   
                if (flowers[i] == null)
                {
                    return true;
                }
            }
            //check if we are surounded by player's capsules 
            for (int i = 0; i < flowers.Length; i++)
            {
                if (flowers[i].GetFlowerState() != DotState.Player)
                {
                    return true;
                }
            }
            return false;
        }
        public DotState GetFlowerState()
        {
            return this.state;
        }
        public bool IsNeighborOf(Flower f)
        {
            if ( f == null)
            {
                return false;
            }
            if (f != null && f == this)
            {
                //special case that prevents loops
                return false;
                //throw new Exception("the neighbor flower is me!!!!!");
            }
            for (int i=0;i<flowers.Length; i++)
            {
                if (this.flowers[i] != null && this.flowers[i] == f)
                {
                    return true;
                }
            }
            return false;
        }

        public BoardCordinate GetCordinate() { return this.cord; }
        public Flower GetTopRight()
        {
            return flowers[0];
        }
        public Flower GetRight()
        {
            return flowers[1];
        }
        public Flower GetBottomRight()
        {
            return flowers[2];
        }
        public Flower GetBottomLeft()
        {
            return flowers[3];
        }
        public Flower GetLeft()
        {
            return flowers[4];
        }
        public Flower GetTopLeft()
        {
            return flowers[5];
        }
        public void SetTopRight(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[0] = g;
        }
        public void SetRight(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[1] = g;
        }
        public void SetBottomRight(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[2] = g;
        }
        public void SetBottomLeft(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[3] = g;
        }
        public void SetLeft(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[4] = g;
        }
        public void SetTopLeft(Flower g)
        {
            if (g == this)
            {
                throw new Exception("cant connect flower to himself");
            }
            flowers[5] = g;
        }

        public bool IsOnTheEdge()
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                if (flowers[i] == null)
                {
                    return true;
                }                
            }
            return false;
        }

        public Flower[] GetNeighborsFlowers()
        {
            return this.flowers;
        }

        public List<Flower> GetAllNewNeighbors(Flower source)
        {
            List<Flower> l = new List<Flower>();
            for (int i = 0; i < this.flowers.Length; i++)
            {
                if (flowers[i] != null && flowers[i].state == DotState.Empty && !flowers[i].IsNeighborOf(source))
                {
                    l.Add(flowers[i]);
                }
            }
            return l;
        }

        

    }
}