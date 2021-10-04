using System;
using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class GameView
    {
        private const int WIDTH = 46;

        private const int HEIGHT = 32;

        public static Vector2 WRONG_POSITION = new Vector2(-1, -1);

        public static void RenderMenu()
        {
            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.Clear();

            Console.WriteLine("***************** [QUORIDOR] ****************");
            Console.WriteLine();
            Console.WriteLine("                    [MENU]                   ");
            Console.WriteLine();
            Console.WriteLine("  0 - Info");
            Console.WriteLine();
            Console.WriteLine("  1 - Play with AI");
            Console.WriteLine("  2 - Play with another person");
            Console.WriteLine();
            Console.WriteLine("  Esc - Exit");
            Console.WriteLine(new string('\n', 16));
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" To continue, press the key on the keyboard");
            Console.WriteLine();
            Console.WriteLine("*********************************************");
        }

        public static void RenderInfo()
        {
            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.Clear();

            Console.WriteLine("***************** [QUORIDOR] ****************");
            Console.WriteLine();
            Console.WriteLine("                    [INFO]                   ");
            Console.WriteLine();
            Console.WriteLine("  Space - show available moves");
            Console.WriteLine("  Arrows - select position");
            Console.WriteLine("  Q - move player");
            Console.WriteLine("  W - set wall");
            Console.WriteLine("  Esc - Back / Exit");
            Console.WriteLine(new string('\n', 17));
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("                  By MaxClub                 ");
            Console.WriteLine();
            Console.WriteLine("*********************************************");
        }

        public static void RenderGameBoard(Board board, Player player1, Player player2, bool isPlayer1Move,
            List<Wall> walls, List<Vector2> availableMoves, string message)
        {
            const char PLAYER_1_CHAR = '1';
            const char PLAYER_2_CHAR = '2';
            const char WALL_CHAR = '█';
            const char AVAILABLE_MOVES_CHAR = '#';

            Vector2 cursorPosition = new Vector2(0, 0);

            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.Clear();

            Console.WriteLine("***************** [QUORIDOR] ****************");
            Console.WriteLine();
            Console.WriteLine("                    [GAME]                   ");
            Console.WriteLine($" {player2.Name}                          WALLS: {player2.WallCount}");
            Console.WriteLine();

            for (int x = 0; x < Board.Size; x++)
            {
                if (x == 0)
                {
                    Console.Write("  ");
                }
                Console.Write($" {x}");
            }
            Console.WriteLine();

            for (int y = 0; y < Board.Size; y++)
            {
                if (y == 0)
                {
                    for (int x = 0; x < Board.Size; x++)
                    {
                        if (x == 0)
                        {
                            Console.Write("  +");
                        }
                        Console.Write("-+");
                    }
                    Console.WriteLine();
                }

                for (int x = 0; x < Board.Size; x++)
                {
                    if (x == 0)
                    {
                        Console.Write($" {y}|");
                    }

                    if (player1.Position.X.Equals(x) && player1.Position.Y.Equals(y))
                    {
                        if (isPlayer1Move)
                        {
                            cursorPosition.X = Console.CursorLeft;
                            cursorPosition.Y = Console.CursorTop;
                        }

                        Console.Write(PLAYER_1_CHAR);
                    }
                    else if (player2.Position.X.Equals(x) && player2.Position.Y.Equals(y))
                    {
                        if (!isPlayer1Move)
                        {
                            cursorPosition.X = Console.CursorLeft;
                            cursorPosition.Y = Console.CursorTop;
                        }

                        Console.Write(PLAYER_2_CHAR);
                    }
                    else
                    {
                        if (ContainAvailableMovesPosition(availableMoves, new Vector2(x, y)))
                        {
                            Console.Write(AVAILABLE_MOVES_CHAR);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    if (FindWallByLeftTopPoint(walls, true, new Vector2(x, y)) != null
                        || FindWallByLeftBottomPoint(walls, true, new Vector2(x, y)) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("|");
                    }

                }
                Console.WriteLine();

                for (int x = 0; x < Board.Size; x++)
                {
                    if (x == 0)
                    {
                        Console.Write("  +");
                    }

                    if (FindWallByRightTopPoint(walls, false, new Vector2(x, y)) != null ||
                        FindWallByRightBottomPoint(walls, false, new Vector2(x, y)) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("-");
                    }

                    if (FindWallByLeftTopPoint(walls, true, new Vector2(x, y)) != null ||
                        FindWallByRightTopPoint(walls, false, new Vector2(x, y)) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("+");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine($" {player1.Name}                          WALLS: {player1.WallCount}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("*********************************************");

            Console.SetCursorPosition((int)cursorPosition.X, (int)cursorPosition.Y);
        }

        public static void MoveCursorUp()
        {
            Console.SetCursorPosition(Console.CursorLeft, Math.Max(7, Console.CursorTop - 1));
        }

        public static void MoveCursorDown()
        {
            Console.SetCursorPosition(Console.CursorLeft, Math.Min(Console.CursorTop + 1, 23));
        }

        public static void MoveCursorLeft()
        {
            Console.SetCursorPosition(Math.Max(3, Console.CursorLeft - 1), Console.CursorTop);
        }

        public static void MoveCursorRight()
        {
            Console.SetCursorPosition(Math.Min(Console.CursorLeft + 1, 19), Console.CursorTop);
        }

        public static Vector2 GetPlayerMovePosition()
        {
            Vector2 cursorPositionOnBoard = GetCursorPositionOnBoard();

            if (cursorPositionOnBoard.Equals(WRONG_POSITION))
            {
                return WRONG_POSITION;
            }

            if ((int)cursorPositionOnBoard.X % 2 == 0 && (int)cursorPositionOnBoard.Y % 2 == 0)
            {
                return new Vector2((int)cursorPositionOnBoard.X / 2, (int)cursorPositionOnBoard.Y / 2);
            }
            else
            {
                return WRONG_POSITION;
            }
        }

        public static Vector2[] GetWallPosition()
        {
            Vector2 cursorPositionOnBoard = GetCursorPositionOnBoard();

            if (cursorPositionOnBoard.Equals(WRONG_POSITION))
            {
                return new Vector2[] { WRONG_POSITION, WRONG_POSITION, };
            }

            if ((int)cursorPositionOnBoard.X % 2 != 0 && (int)cursorPositionOnBoard.Y % 2 == 0)
            {
                return new Vector2[] {
                    new Vector2 ((int)cursorPositionOnBoard.X / 2, (int)cursorPositionOnBoard.Y / 2),
                    new Vector2 (((int)cursorPositionOnBoard.X / 2) + 1, (int)cursorPositionOnBoard.Y / 2),
                };
            }
            else if ((int)cursorPositionOnBoard.X % 2 == 0 && (int)cursorPositionOnBoard.Y % 2 != 0)
            {
                return new Vector2[] {
                    new Vector2 ((int)cursorPositionOnBoard.X / 2, ((int)cursorPositionOnBoard.Y / 2) + 1),
                    new Vector2 ((int)cursorPositionOnBoard.X / 2, (int)cursorPositionOnBoard.Y / 2),
                };
            }
            else
            {
                return new Vector2[] { WRONG_POSITION, WRONG_POSITION, };
            }
        }

        public static Vector2 GetCursorPositionOnBoard()
        {
            int cursorLeft = Console.CursorLeft - 3;
            int cursorTop = Console.CursorTop - 7;

            if (cursorLeft < 0 || cursorLeft > 16 || cursorTop < 0 || cursorTop > 16)
            {
                return WRONG_POSITION;
            }

            return new Vector2(cursorLeft, cursorTop);
        }

        private static bool ContainAvailableMovesPosition(List<Vector2> availableMoves, Vector2 position)
        {
            if (availableMoves == null)
            {
                return false;
            }

            foreach (Vector2 availableMove in availableMoves)
            {
                if (availableMove.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }

        private static Wall FindWallByLeftTopPoint(List<Wall> walls, bool isVertical, Vector2 point)
        {
            foreach (Wall wall in walls)
            {
                if (wall.LeftTopPoint.Equals(point) && wall.IsVertical.Equals(isVertical))
                {
                    return wall;
                }
            }

            return null;
        }

        private static Wall FindWallByRightTopPoint(List<Wall> walls, bool isVertical, Vector2 point)
        {
            foreach (Wall wall in walls)
            {
                if (wall.RightTopPoint.Equals(point) && wall.IsVertical.Equals(isVertical))
                {
                    return wall;
                }
            }

            return null;
        }

        private static Wall FindWallByLeftBottomPoint(List<Wall> walls, bool isVertical, Vector2 point)
        {
            foreach (Wall wall in walls)
            {
                if (wall.LeftBottomPoint.Equals(point) && wall.IsVertical.Equals(isVertical))
                {
                    return wall;
                }
            }

            return null;
        }

        private static Wall FindWallByRightBottomPoint(List<Wall> walls, bool isVertical, Vector2 point)
        {
            foreach (Wall wall in walls)
            {
                if (wall.RightBottomPoint.Equals(point) && wall.IsVertical.Equals(isVertical))
                {
                    return wall;
                }
            }

            return null;
        }
    }
}
