using System.Collections.Generic;

namespace QuoridorCmd.Model
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

            Player1 = new Player("PLAYER 1", PlayerColor.White);
            Player2 = new Player("PLAYER 2", PlayerColor.Black);

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
            return Board.HasPathBetweenPositionAndLine(Player1.Position, Player1.WinningPositionY)
               && Board.HasPathBetweenPositionAndLine(Player2.Position, Player2.WinningPositionY);
        }

        public List<Position> GetCurrentPlayerAvailableMoves()
        {
            return GetPlayerAvailableMoves(GetCurrentPlayer(), GetAnotherPlayer());
        }

        public List<Position> GetAnotherPlayerAvailableMoves()
        {
            return GetPlayerAvailableMoves(GetAnotherPlayer(), GetCurrentPlayer());
        }

        private List<Position> GetPlayerAvailableMoves(Player currentPlayer, Player anotherPlayer)
        {
            List<Position> availableMoves = new List<Position>();

            foreach (var edge in Board.Graph.FindVertex(currentPlayer.Position).Edges)
            {
                Position position = edge.ConnectedVertex.Position;

                if (!anotherPlayer.Position.Equals(position))
                {
                    availableMoves.Add(position);
                }
                else
                {
                    bool isJumpMoveAvailable = false;
                    Position pointJumpMove = new Position(2 * anotherPlayer.Position.Coordinate.X - currentPlayer.Position.Coordinate.X,
                        2 * anotherPlayer.Position.Coordinate.Y - currentPlayer.Position.Coordinate.Y);
                    Position pointDiagonal1Move = new Position(
                        anotherPlayer.Position.Coordinate.X + (anotherPlayer.Position.Coordinate.Y - currentPlayer.Position.Coordinate.Y),
                        anotherPlayer.Position.Coordinate.Y + (anotherPlayer.Position.Coordinate.X - currentPlayer.Position.Coordinate.X)
                        );
                    Position pointDiagonal2Move = new Position(
                        anotherPlayer.Position.Coordinate.X - (anotherPlayer.Position.Coordinate.Y - currentPlayer.Position.Coordinate.Y),
                        anotherPlayer.Position.Coordinate.Y - (anotherPlayer.Position.Coordinate.X - currentPlayer.Position.Coordinate.X)
                        );

                    if (HasEdgeBetweenPositions(anotherPlayer.Position, pointJumpMove))
                    {
                        isJumpMoveAvailable = true;
                        availableMoves.Add(pointJumpMove);
                    }

                    if (!isJumpMoveAvailable)
                    {
                        if (HasEdgeBetweenPositions(anotherPlayer.Position, pointDiagonal1Move))
                        {
                            availableMoves.Add(pointDiagonal1Move);
                        }

                        if (HasEdgeBetweenPositions(anotherPlayer.Position, pointDiagonal2Move))
                        {
                            availableMoves.Add(pointDiagonal2Move);
                        }
                    }
                }
            }

            return availableMoves;
        }

        public bool MoveCurrentPlayer(Position position)
        {
            Player currentPlayer = GetCurrentPlayer();

            List<Position> availableMoves = GetCurrentPlayerAvailableMoves();

            foreach (Position availableMove in availableMoves)
            {
                if (availableMove.Equals(position))
                {
                    currentPlayer.Position = position;

                    return true;
                }
            }

            return false;
        }

        public bool AddCurrentPlayerWall(Position leftTopPosition, bool isVertical)
        {
            Player currentPlayer = GetCurrentPlayer();

            if (currentPlayer.WallCount <= 0)
            {
                return false;
            }

            Wall wall = new Wall(leftTopPosition, isVertical);
            Wall crossWall = new Wall(leftTopPosition, !isVertical);

            Position rightTopPosition = new Position(leftTopPosition.Coordinate.X + 1, leftTopPosition.Coordinate.Y);
            Position leftBottomPosition = new Position(leftTopPosition.Coordinate.X, leftTopPosition.Coordinate.Y + 1);
            Position rightBottomPosition = new Position(leftTopPosition.Coordinate.X + 1, leftTopPosition.Coordinate.Y + 1);

            foreach (var w in Walls)
            {
                if (crossWall.Equals(w) || wall.Equals(w))
                {
                    return false;
                }
            }

            if ((isVertical && (!HasEdgeBetweenPositions(wall.LeftTopPosition, rightTopPosition) || !HasEdgeBetweenPositions(leftBottomPosition, rightBottomPosition)))
                || (!isVertical && (!HasEdgeBetweenPositions(wall.LeftTopPosition, leftBottomPosition) || !HasEdgeBetweenPositions(rightTopPosition, rightBottomPosition))))
            {
                return false;
            }

            if (isVertical)
            {
                Board.Graph.RemoveEdge(leftTopPosition, rightTopPosition);
                Board.Graph.RemoveEdge(leftBottomPosition, rightBottomPosition);
            }
            else
            {
                Board.Graph.RemoveEdge(leftTopPosition, leftBottomPosition);
                Board.Graph.RemoveEdge(rightTopPosition, rightBottomPosition);
            }

            if (!HavePlayersPath())
            {
                if (isVertical)
                {
                    Board.Graph.AddEdge(leftTopPosition, rightTopPosition, 1);
                    Board.Graph.AddEdge(leftBottomPosition, rightBottomPosition, 1);
                }
                else
                {
                    Board.Graph.AddEdge(leftTopPosition, leftBottomPosition, 1);
                    Board.Graph.AddEdge(rightTopPosition, rightBottomPosition, 1);
                }

                return false;
            }

            Walls.Add(wall);

            currentPlayer.WallCount--;

            return true;
        }

        private bool HasEdgeBetweenPositions(Position position1, Position position2)
        {
            GraphVertex vertex1 = Board.Graph.FindVertex(position1);
            GraphVertex vertex2 = Board.Graph.FindVertex(position2);

            if (vertex1 == null || vertex2 == null)
            {
                return false;
            }

            foreach (var edge in vertex1.Edges)
            {
                if (position2.Equals(edge.ConnectedVertex.Position))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckCurrentPlayerWin()
        {
            return GetCurrentPlayer().Position.Coordinate.Y.Equals(GetCurrentPlayer().WinningPositionY);
        }
    }
}