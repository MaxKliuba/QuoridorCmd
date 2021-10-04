using System;
using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class StupidAI
    {
        private GameProcess gameProcess;

        public Player Player { get; }

        public StupidAI(GameProcess gameProcess, Player player)
        {
            this.gameProcess = gameProcess;
            Player = player;
        }

        public void Move()
        {
            Random random = new Random();

            if (random.Next(0, 2) == 0 && Player.WallCount > 0)
            {
                while (!gameProcess.AddWall(Player,
                    new Vector2(random.Next(0, Board.Size - 2), random.Next(0, Board.Size - 2)),
                    random.Next(0, 2) == 0)) ;
            }
            else
            {
                var dijkstra = new Dijkstra(gameProcess.Board.Graph);
                var availableMoves = gameProcess.GetPlayerAvailableMoves(Player);
                List<Vector2> minShortestPath = null;

                foreach (var availableMove in availableMoves) 
                {
                    for (int i = 0; i < Board.Size; i++)
                    {
                        var shortestPath = dijkstra.FindShortestPath(
                            new Vector2(i, Player.WinningPositionY),
                            new Vector2(availableMove.X, availableMove.Y));

                        if (shortestPath != null && ((minShortestPath != null && shortestPath.Count < minShortestPath.Count) || minShortestPath == null))
                            minShortestPath = shortestPath;
                    }
                }

                gameProcess.MovePlayer(Player, minShortestPath[0]);
            }
        }
    }
}
