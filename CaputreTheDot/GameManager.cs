namespace CaputreTheDot
{
    public class GameManager : IMovment
    {
        private static GameManager gm;
        bool isPlayerTurn;
        int movesCounter;
        GameGraph graph;
        GameActivity activity;
        private GameManager()
        {
            isPlayerTurn = true;
            movesCounter = 0;
            graph = new GameGraph();
        }

        public static GameManager GetGameManager()
        {
            if (gm == null)
            {
                gm = new GameManager();
            }
            return gm;
        }

        public void ResetTurns()
        {
            isPlayerTurn = true;
            graph = new GameGraph();
        }

        public void SwitchTurn()
        {
            if (isPlayerTurn)
            {
                //switch turn first because RandomMove also switch turns.
                //otherwise we have endless recursion
                isPlayerTurn = !isPlayerTurn;
                movesCounter++;
                //ShortestPath.RandomMove();
                try
                {
                    ShortestPath.FindShortestPath();
                }
                catch
                {
                    ShortestPath.RandomMove();
                }
            }
            else
            {
                isPlayerTurn = !isPlayerTurn;
            }
        }

        public bool GetIsPlayerTurn()
        {
            return this.isPlayerTurn;
        }

        public int GetMovesConter()
        {
            return this.movesCounter;
        }

        public GameGraph GetGameGraph()
        {
            return this.graph;
        }

        public GameActivity GetGameActivity()
        {
            return this.activity;
        }

        public void SetGameActivity(GameActivity gameActivity)
        {
            this.activity = gameActivity;
        }

        public void PerformMovement(FlowerMovment fm)
        {
            System.Console.WriteLine("MOVMENT {0}", fm);
            if (fm.GetSource().IsNeighborOf(fm.GetDestenation()))
            {
                //activity.PerformMovement(fm);
                //graph.PerformMovement(fm);
                SetDotState(Flower.DotState.Computer, 
                    fm.GetDestenation().GetCordinate().BoardX, fm.GetDestenation().GetCordinate().BoardY);
                SetDotState(Flower.DotState.Empty,
                    fm.GetSource().GetCordinate().BoardX, fm.GetSource().GetCordinate().BoardY);
               
                SwitchTurn();
                //System.Console.WriteLine("me   cord {0}", fm.GetMe().GetCordinate());
                //System.Console.WriteLine("dest cord {0}",fm.GetDestenation().GetCordinate());
            }
            else
            {
                throw new System.Exception("the flower movment is not neighbors");
            }
        }

        public void DoPlayerVictory()
        {
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("============YOU WIN============");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("===============================");
        }

        public void DoComputerVictory()
        {
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("============YOU LOSE===========");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine("===============================");
        }

        public void SetDotState(Flower.DotState ds, int x, int y)
        {
            activity.SetDotState(ds, x, y);
            if (ds == Flower.DotState.Computer)
            {
                graph.SetComputerFlower(graph.GetFlowerByBoardCordinate(x, y));
            }
            graph.SetDotState(ds, x, y);
            
        }
    }
}