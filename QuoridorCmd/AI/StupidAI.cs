using QuoridorCmd.Model;
using System;
using System.Collections.Generic;

namespace QuoridorCmd.AI
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

            List<Position> currentPlayerMinShortestPath = GetMinShortestPath(Player, gameProcess.GetCurrentPlayerAvailableMoves());
            List<Position> anotherPlayerMinShortestPath = GetMinShortestPath(gameProcess.GetAnotherPlayer(), gameProcess.GetAnotherPlayerAvailableMoves());

            if (currentPlayerMinShortestPath.Count > 1 && (anotherPlayerMinShortestPath.Count <= 2 || (random.Next(0, 4) == 0 && Player.WallCount > 0
                && currentPlayerMinShortestPath.Count > anotherPlayerMinShortestPath.Count)))
            {
                Position anotherPlayerPosition = gameProcess.GetAnotherPlayer().Position;
                Position minShortestPathNextPosition = anotherPlayerMinShortestPath[0];
                Position wallLeftTopPosition = new Position(0, 0);
                bool isWallVertical = true;

                for (int i = 0; i < 2; i++)
                {
                    // хід гравця по вертикалі - горизонтальна стіна
                    if (anotherPlayerPosition.Coordinate.X.Equals(minShortestPathNextPosition.Coordinate.X))
                    {
                        if (anotherPlayerPosition.Coordinate.Y < minShortestPathNextPosition.Coordinate.Y) // вниз
                        {
                            wallLeftTopPosition.SetCoordinate(anotherPlayerPosition.Coordinate.X - i, anotherPlayerPosition.Coordinate.Y);
                        }
                        else // вгору
                        {
                            wallLeftTopPosition.SetCoordinate(anotherPlayerPosition.Coordinate.X - i, minShortestPathNextPosition.Coordinate.Y);
                        }

                        isWallVertical = false;
                    }
                    // хід гравця по горизонталі - вертикальна стіна
                    else if (anotherPlayerPosition.Coordinate.Y.Equals(minShortestPathNextPosition.Coordinate.Y))
                    {
                        if (anotherPlayerPosition.Coordinate.X < minShortestPathNextPosition.Coordinate.X) // праворуч
                        {
                            wallLeftTopPosition.SetCoordinate(anotherPlayerPosition.Coordinate.X, anotherPlayerPosition.Coordinate.Y - i);
                        }
                        else // ліворуч
                        {
                            wallLeftTopPosition.SetCoordinate(minShortestPathNextPosition.Coordinate.X, minShortestPathNextPosition.Coordinate.Y - i);
                        }

                        isWallVertical = true;
                    }

                    isWallAdded = gameProcess.AddCurrentPlayerWall(wallLeftTopPosition, isWallVertical);

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

        private List<Position> GetMinShortestPath(Player player, List<Position> availableMoves)
        {
            List<Position> minShortestPath = null;

            foreach (var availableMove in availableMoves)
            {
                for (int i = 1; i <= Board.SIZE; i++)
                {
                    var shortestPath = dijkstra.FindShortestPath(new Position(i, player.WinningPositionY), availableMove);

                    if (shortestPath != null && ((minShortestPath != null && shortestPath.Count < minShortestPath.Count) || minShortestPath == null))
                        minShortestPath = shortestPath;
                }
            }

            return minShortestPath;
        }
    }
}
