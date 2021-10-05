using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class GameProcess
    {
        public Board Board { get; }

        public List<Wall> Walls { get; }

        public Player Player1 { get; }

        public Player Player2 { get; }

        private bool isPlayer1CurrentPlayer;

        public GameProcess()
        {
            Board = new Board();

            Walls = new List<Wall>();

            Player1 = new Player("PLAYER 1", new Vector2(Board.Size / 2, Board.Size - 1), 0);
            Player2 = new Player("PLAYER 2", new Vector2(Board.Size / 2, 0), Board.Size - 1);

            isPlayer1CurrentPlayer = true;
        }

        public Player GetCurrentPlayer()
        {
            return isPlayer1CurrentPlayer ? Player1 : Player2;
        }

        public Player GetAnotherPlayer()
        {
            return isPlayer1CurrentPlayer ? Player2 : Player1;
        }

        public Player ChangeCurrentPlayer()
        {
            isPlayer1CurrentPlayer = !isPlayer1CurrentPlayer;

            return GetCurrentPlayer();
        }

        public bool HavePlayersPath()
        {
            return Board.HasPathBetweenPointAndLine(Player1.Position, Player1.WinningPositionY)
               && Board.HasPathBetweenPointAndLine(Player2.Position, Player2.WinningPositionY);
        }

        public List<Vector2> GetCurrentPlayerAvailableMoves()
        {
            return GetPlayerAvailableMoves(GetCurrentPlayer(), GetAnotherPlayer());
        }

        public List<Vector2> GetAnotherPlayerAvailableMoves()
        {
            return GetPlayerAvailableMoves(GetAnotherPlayer(), GetCurrentPlayer());
        }

        private List<Vector2> GetPlayerAvailableMoves(Player currentPlayer, Player anotherPlayer)
        {
            List<Vector2> availableMoves = new List<Vector2>();

            foreach (var edge in Board.Graph.FindVertex(currentPlayer.Position).Edges)
            {
                Vector2 point = edge.ConnectedVertex.Point;

                if (!anotherPlayer.Position.Equals(point))
                {
                    availableMoves.Add(point);
                }
                else
                {
                    bool isJumpMoveAvailable = false;
                    Vector2 pointJumpMove = new Vector2(2 * anotherPlayer.Position.X - currentPlayer.Position.X,
                        2 * anotherPlayer.Position.Y - currentPlayer.Position.Y);
                    Vector2 pointDiagonal1Move = new Vector2(anotherPlayer.Position.X + (anotherPlayer.Position.Y - currentPlayer.Position.Y),
                        anotherPlayer.Position.Y + (anotherPlayer.Position.X - currentPlayer.Position.X));
                    Vector2 pointDiagonal2Move = new Vector2(anotherPlayer.Position.X - (anotherPlayer.Position.Y - currentPlayer.Position.Y),
                        anotherPlayer.Position.Y - (anotherPlayer.Position.X - currentPlayer.Position.X));

                    if (HasEdgeBetweenPoints(anotherPlayer.Position, pointJumpMove))
                    {
                        isJumpMoveAvailable = true;
                        availableMoves.Add(pointJumpMove);
                    }

                    if (!isJumpMoveAvailable)
                    {
                        if (HasEdgeBetweenPoints(anotherPlayer.Position, pointDiagonal1Move))
                        {
                            availableMoves.Add(pointDiagonal1Move);
                        }

                        if (HasEdgeBetweenPoints(anotherPlayer.Position, pointDiagonal2Move))
                        {
                            availableMoves.Add(pointDiagonal2Move);
                        }
                    }
                }
            }

            return availableMoves;
        }

        public bool MoveCurrentPlayer(Vector2 position)
        {
            Player currentPlayer = GetCurrentPlayer();

            List<Vector2> availableMoves = GetCurrentPlayerAvailableMoves();

            foreach (Vector2 availableMove in availableMoves)
            {
                if (availableMove.Equals(position))
                {
                    currentPlayer.Position = position;

                    return true;
                }
            }

            return false;
        }

        public bool AddCurrentPlayerWall(Vector2 leftTopPoint, bool isVertical)
        {
            Player currentPlayer = GetCurrentPlayer();

            Vector2 rightTopPoint = new Vector2();

            if (isVertical)
            {
                rightTopPoint.X = leftTopPoint.X;
                rightTopPoint.Y = leftTopPoint.Y - 1;
            }
            else
            {
                rightTopPoint.X = leftTopPoint.X + 1;
                rightTopPoint.Y = leftTopPoint.Y;
            }

            return AddCurrentPlayerWall(leftTopPoint, rightTopPoint);
        }

        public bool AddCurrentPlayerWall(Vector2 leftTopPoint, Vector2 rightTopPoint)
        {
            Player currentPlayer = GetCurrentPlayer();

            if (currentPlayer.WallCount <= 0)
            {
                return false;
            }

            Wall wall = null;
            Wall crossWall = null;

            if (leftTopPoint.Y.Equals(rightTopPoint.Y))
            {
                wall = new Wall(leftTopPoint, rightTopPoint, new Vector2(leftTopPoint.X, leftTopPoint.Y + 1), new Vector2(rightTopPoint.X, rightTopPoint.Y + 1), true);
                crossWall = new Wall(wall.LeftBottomPoint, wall.LeftTopPoint, wall.RightBottomPoint, wall.RightTopPoint, false);
            }
            else
            {
                wall = new Wall(leftTopPoint, rightTopPoint, new Vector2(leftTopPoint.X + 1, leftTopPoint.Y), new Vector2(rightTopPoint.X + 1, rightTopPoint.Y), false);
                crossWall = new Wall(wall.RightTopPoint, wall.RightBottomPoint, wall.LeftTopPoint, wall.LeftBottomPoint, true);
            }

            foreach (var w in Walls)
            {
                if (crossWall.Equals(w))
                {
                    return false;
                }
            }

            if (!HasEdgeBetweenPoints(wall.LeftTopPoint, wall.RightTopPoint) || !HasEdgeBetweenPoints(wall.LeftBottomPoint, wall.RightBottomPoint))
            {
                return false;
            }

            Board.Graph.RemoveEdge(wall.LeftTopPoint, wall.RightTopPoint);
            Board.Graph.RemoveEdge(wall.LeftBottomPoint, wall.RightBottomPoint);

            if (!HavePlayersPath())
            {
                Board.Graph.AddEdge(wall.LeftTopPoint, wall.RightTopPoint, 1);
                Board.Graph.AddEdge(wall.LeftBottomPoint, wall.RightBottomPoint, 1);

                return false;
            }

            Walls.Add(wall);

            currentPlayer.WallCount--;

            return true;
        }

        private bool HasEdgeBetweenPoints(Vector2 point1, Vector2 point2)
        {
            GraphVertex vertex1 = Board.Graph.FindVertex(point1);
            GraphVertex vertex2 = Board.Graph.FindVertex(point2);

            if (vertex1 == null || vertex2 == null)
            {
                return false;
            }

            foreach (var edge in vertex1.Edges)
            {
                if (point2.Equals(edge.ConnectedVertex.Point))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckCurrentPlayerWin()
        {
            return GetCurrentPlayer().Position.Y.Equals(GetCurrentPlayer().WinningPositionY);
        }
    }
}