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
    class ShortestPath
    {
        public static void RandomMove()
        {
            Flower computer = GameManager.GetGameManager().GetGameGraph().GetComputerFlower();
            FlowerMovment fm = null;
            Random rnd = new Random();
            bool nextDotFound = false;
            while (!nextDotFound)
            {
                switch (rnd.Next(0, 6))
                {
                    case 0:
                        {
                            fm = new FlowerMovment(computer, computer.GetTopLeft());
                            break;
                        }
                    case 1:
                        {
                            fm = new FlowerMovment(computer, computer.GetTopRight());
                            break;
                        }
                    case 2:
                        {
                            fm = new FlowerMovment(computer, computer.GetRight());
                            break;
                        }
                    case 3:
                        {
                            fm = new FlowerMovment(computer, computer.GetBottomRight());
                            break;
                        }
                    case 4:
                        {
                            fm = new FlowerMovment(computer, computer.GetBottomLeft());
                            break;
                        }
                    case 5:
                        {
                            fm = new FlowerMovment(computer, computer.GetLeft());
                            break;
                        }
                }
                if (fm.GetDestenation() == null)
                {
                    GameManager.GetGameManager().DoComputerVictory();
                    break; // prevent next checks
                }
                if (!fm.GetSource().IsEscapeable())
                {
                    GameManager.GetGameManager().DoPlayerVictory();
                    break;
                }
                if (fm.GetDestenation().GetFlowerState() == Flower.DotState.Empty)
                {
                    nextDotFound = true;
                    break;
                }
            }
            if ( nextDotFound)
            {
                GameManager.GetGameManager().PerformMovement(fm);
            } 
        }

        public static void FindShortestPath()
        {
            GameGraph g = GameManager.GetGameManager().GetGameGraph();
            Flower computer = g.GetComputerFlower();
            Queue<PathNode> q = new Queue<PathNode>();
            PathNode rootPathNode = new PathNode(null, computer);
            if (computer.IsOnTheEdge())
            {
                Console.WriteLine("we are on the edge, computer win");
                GameManager.GetGameManager().DoComputerVictory();
                return;
            }
            for (int i = 0; i < computer.GetNeighborsFlowers().Length; i++)
            {
                Flower neighbor = computer.GetNeighborsFlowers()[i];
                if (neighbor.GetFlowerState() == Flower.DotState.Empty)
                {
                    PathNode tmpPn = new PathNode(rootPathNode,neighbor );
                    Console.WriteLine(tmpPn);
                    q.Enqueue(tmpPn); 
                }
            }
            while (q.Count > 0)
            {
                PathNode head = q.Dequeue();
                Console.WriteLine(head);
                if (head.CurrentFlower.IsOnTheEdge())
                {
                    Console.WriteLine("found a way out board");
                    Console.WriteLine(head);
                    Console.WriteLine("the desierd movment");
                    GameManager.GetGameManager().PerformMovement(head.TransformRootToMovement());
                    return;
                }
                else
                {
                    List<Flower> nextFlowers = head.CurrentFlower.GetAllNewNeighbors(head.Prev.CurrentFlower);
                    for (int i = 0; i < nextFlowers.Count; i++)
                    {
                        PathNode pathNode = new PathNode(head, nextFlowers[i]);
                        if (!pathNode.IsVisitedBefore())
                        {
                            q.Enqueue(pathNode);
                        }
                    }
                }
            }
            throw new Exception("not path found");
            //Console.WriteLine("no path");
        }
    }
}