using System;
using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class StupidAI
    {
        private GameProcess gameProcess;

        public Player Player { get; }

        private Dijkstra dijkstra;

        public StupidAI(GameProcess gameProcess, Player player)
        {
            this.gameProcess = gameProcess;
            Player = player;
            dijkstra = new Dijkstra(gameProcess.Board.Graph);
        }

        public void Move()
        {
            bool isWallAdded = false;

            Random random = new Random();

            List<Vector2> currentPlayerMinShortestPath = GetMinShortestPath(Player, gameProcess.GetCurrentPlayerAvailableMoves());
            List<Vector2> anotherPlayerMinShortestPath = GetMinShortestPath(gameProcess.GetAnotherPlayer(), gameProcess.GetAnotherPlayerAvailableMoves());

            if (currentPlayerMinShortestPath.Count > 1 && (anotherPlayerMinShortestPath.Count <= 2 || (random.Next(0, 4) == 0 && Player.WallCount > 0
                && currentPlayerMinShortestPath.Count > anotherPlayerMinShortestPath.Count)))
            {
                Vector2 anotherPlayerPosition = gameProcess.GetAnotherPlayer().Position;
                Vector2 minShortestPathNextPosition = anotherPlayerMinShortestPath[0];
                Vector2 wallLeftTopPoint = new Vector2();
                Vector2 wallRightTopPoint = new Vector2();

                for (int i = 0; i < 2; i++)
                {
                    if (anotherPlayerPosition.X.Equals(minShortestPathNextPosition.X))
                    {
                        if (anotherPlayerPosition.Y < minShortestPathNextPosition.Y)
                        {
                            wallLeftTopPoint.X = anotherPlayerPosition.X - i;
                            wallLeftTopPoint.Y = minShortestPathNextPosition.Y;
                            wallRightTopPoint.X = anotherPlayerPosition.X - i;
                            wallRightTopPoint.Y = anotherPlayerPosition.Y;
                        }
                        else
                        {
                            wallLeftTopPoint.X = anotherPlayerPosition.X - i;
                            wallLeftTopPoint.Y = anotherPlayerPosition.Y;
                            wallRightTopPoint.X = anotherPlayerPosition.X - i;
                            wallRightTopPoint.Y = minShortestPathNextPosition.Y;
                        }
                    }
                    else if (anotherPlayerPosition.Y.Equals(minShortestPathNextPosition.Y))
                    {
                        if (anotherPlayerPosition.X < minShortestPathNextPosition.X)
                        {
                            wallLeftTopPoint.X = anotherPlayerPosition.X;
                            wallLeftTopPoint.Y = anotherPlayerPosition.Y - i;
                            wallRightTopPoint.X = minShortestPathNextPosition.X;
                            wallRightTopPoint.Y = anotherPlayerPosition.Y - i;
                        }
                        else
                        {
                            wallLeftTopPoint.X = minShortestPathNextPosition.X;
                            wallLeftTopPoint.Y = anotherPlayerPosition.Y - i;
                            wallRightTopPoint.X = anotherPlayerPosition.X;
                            wallRightTopPoint.Y = anotherPlayerPosition.Y - i;
                        }
                    }

                    isWallAdded = gameProcess.AddCurrentPlayerWall(wallLeftTopPoint, wallRightTopPoint);

                    if (isWallAdded)
                    {
                        break;
                    }
                }
            }

            if (!isWallAdded)
            {
                gameProcess.MoveCurrentPlayer(currentPlayerMinShortestPath[0]);
            }
        }

        private List<Vector2> GetMinShortestPath(Player player, List<Vector2> availableMoves)
        {
            List<Vector2> minShortestPath = null;

            foreach (var availableMove in availableMoves)
            {
                for (int i = 0; i < Board.Size; i++)
                {
                    var shortestPath = dijkstra.FindShortestPath(
                        new Vector2(i, player.WinningPositionY),
                        new Vector2(availableMove.X, availableMove.Y));

                    if (shortestPath != null && ((minShortestPath != null && shortestPath.Count < minShortestPath.Count) || minShortestPath == null))
                        minShortestPath = shortestPath;
                }
            }

            return minShortestPath;
        }
    }
}
