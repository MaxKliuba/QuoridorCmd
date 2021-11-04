using QuoridorCmd.Model;
using System;
using System.Collections.Generic;

namespace QuoridorCmd.View
{
    class GameView
    {
        private const int WIDTH = 46;

        private const int HEIGHT = 33;

        public static Position WRONG_POSITION = new Position(-1, -1);

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
            Console.WriteLine(new string('\n', 17));
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
            Console.WriteLine("  W - set vertical wall");
            Console.WriteLine("  E - set horizontal wall");
            Console.WriteLine("  Esc - Back / Exit");
            Console.WriteLine(new string('\n', 17));
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("               By MaxClub & Co               ");
            Console.WriteLine();
            Console.WriteLine("*********************************************");
        }

        public static void RenderGameBoard(Board board, Player player1, Player player2, Player currentPlayer,
            List<Wall> walls, List<Position> availableMoves, string message)
        {
            char[] CELL_NUMBERING_CHARS = { '_', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', };
            char[] WALL_NUMBERING_CHARS = { '_', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', };

            const char PLAYER_1_CHAR = '1';
            const char PLAYER_2_CHAR = '2';
            const char WALL_CHAR = '█';
            const char AVAILABLE_MOVES_CHAR = '#';

            Position cursorPosition = new Position(1, 1);

            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.Clear();

            Console.WriteLine("***************** [QUORIDOR] ****************");
            Console.WriteLine();
            Console.WriteLine("                    [GAME]                   ");
            Console.WriteLine($" {player2.Name}                          WALLS: {player2.WallCount}");
            Console.WriteLine();

            for (int x = 1; x <= Board.SIZE; x++)
            {
                if (x == 1)
                {
                    Console.Write("  ");
                }
                Console.Write($" {CELL_NUMBERING_CHARS[x]}");
            }
            Console.WriteLine();

            for (int y = 1; y <= Board.SIZE; y++)
            {
                if (y == 1) // верхня грань
                {
                    for (int x = 1; x <= Board.SIZE; x++)
                    {
                        if (x == 1)
                        {
                            Console.Write("  +");
                        }
                        Console.Write("-+");
                    }
                    Console.WriteLine();
                }

                for (int x = 1; x <= Board.SIZE; x++) // клітинки і перестінки між ними
                {
                    if (x == 1)
                    {
                        Console.Write($" {y}|");
                    }

                    if (player1.Position.Coordinate.X.Equals(x) && player1.Position.Coordinate.Y.Equals(y))
                    {
                        if (player1.Equals(currentPlayer))
                        {
                            cursorPosition.SetCoordinate(Console.CursorLeft, Console.CursorTop);
                        }

                        Console.Write(PLAYER_1_CHAR);
                    }
                    else if (player2.Position.Coordinate.X.Equals(x) && player2.Position.Coordinate.Y.Equals(y))
                    {
                        if (player2.Equals(currentPlayer))
                        {
                            cursorPosition.SetCoordinate(Console.CursorLeft, Console.CursorTop);
                        }

                        Console.Write(PLAYER_2_CHAR);
                    }
                    else
                    {
                        if (ContainAvailableMovesPosition(availableMoves, new Position(x, y)))
                        {
                            Console.Write(AVAILABLE_MOVES_CHAR);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    if (FindWallByLeftTopPosition(walls, new Position(x, y - 1), true) != null
                        || FindWallByLeftTopPosition(walls, new Position(x, y), true) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("|");
                    }

                }
                Console.WriteLine();

                for (int x = 1; x <= Board.SIZE; x++) // горизонтальний перестінок між рядами клітинок
                {
                    if (x == 1)
                    {
                        Console.Write("  +");
                    }

                    if (FindWallByLeftTopPosition(walls, new Position(x - 1, y), false) != null ||
                        FindWallByLeftTopPosition(walls, new Position(x, y), false) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("-");
                    }

                    if (FindWallByLeftTopPosition(walls, new Position(x, y), true) != null ||
                        FindWallByLeftTopPosition(walls, new Position(x, y), false) != null)
                    {
                        Console.Write(WALL_CHAR);
                    }
                    else
                    {
                        Console.Write("+");
                    }

                    if (x == Board.SIZE && y != Board.SIZE)
                    {
                        Console.Write($"{y}");
                    }
                }
                Console.WriteLine();
            }

            for (int x = 1; x < Board.SIZE; x++)
            {
                if (x == 1)
                {
                    Console.Write("   ");
                }
                Console.Write($" {WALL_NUMBERING_CHARS[x]}");
            }
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine($" {player1.Name}                          WALLS: {player1.WallCount}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("*********************************************");

            Console.SetCursorPosition((int)cursorPosition.Coordinate.X, (int)cursorPosition.Coordinate.Y);
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

        public static Position GetPlayerMovePosition()
        {
            Position cursorPositionOnBoard = GetCursorPositionOnBoard();

            if (cursorPositionOnBoard.Equals(WRONG_POSITION))
            {
                return WRONG_POSITION;
            }

            if ((int)cursorPositionOnBoard.Coordinate.X % 2 != 0 && (int)cursorPositionOnBoard.Coordinate.Y % 2 != 0)
            {
                return new Position((int)(cursorPositionOnBoard.Coordinate.X / 2) + 1, (int)(cursorPositionOnBoard.Coordinate.Y / 2) + 1);
            }
            else
            {
                return WRONG_POSITION;
            }
        }

        public static Position GetWallPosition()
        {
            Position cursorPositionOnBoard = GetCursorPositionOnBoard();

            if (cursorPositionOnBoard.Equals(WRONG_POSITION))
            {
                return WRONG_POSITION;
            }

            if ((int)cursorPositionOnBoard.Coordinate.X % 2 == 0 && (int)cursorPositionOnBoard.Coordinate.Y % 2 == 0)
            {
                return new Position((int)cursorPositionOnBoard.Coordinate.X / 2, (int)cursorPositionOnBoard.Coordinate.Y / 2);
            }
            else
            {
                return WRONG_POSITION;
            }
        }

        public static Position GetCursorPositionOnBoard()
        {
            int cursorLeft = Console.CursorLeft - 2;
            int cursorTop = Console.CursorTop - 6;

            if (cursorLeft < 1 || cursorLeft > 17 || cursorTop < 1 || cursorTop > 17)
            {
                return WRONG_POSITION;
            }

            return new Position(cursorLeft, cursorTop);
        }

        private static bool ContainAvailableMovesPosition(List<Position> availableMoves, Position position)
        {
            if (availableMoves == null)
            {
                return false;
            }

            foreach (Position availableMove in availableMoves)
            {
                if (availableMove.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }

        private static Wall FindWallByLeftTopPosition(List<Wall> walls, Position point, bool isVertical)
        {
            foreach (Wall wall in walls)
            {
                if (wall.LeftTopPosition.Equals(point) && wall.IsVertical.Equals(isVertical))
                {
                    return wall;
                }
            }

            return null;
        }
    }
}
