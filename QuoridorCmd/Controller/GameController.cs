using QuoridorCmd.AI;
using QuoridorCmd.Model;
using QuoridorCmd.View;
using System;
using System.Collections.Generic;

namespace QuoridorCmd.Controller
{
    class GameController
    {
        public static void Start()
        {
            GameView.RenderMenu();

            while (true)
            {
                var keyMenu = Console.ReadKey(true);

                if (keyMenu.Key.Equals(ConsoleKey.D1) || keyMenu.Key.Equals(ConsoleKey.D2))
                {
                    GameProcess gameProcess = new GameProcess();
                    StupidAI stupidAI = null;
                    List<Position> availableMoves = null;
                    string message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name);
                    bool gameOver = false;

                    if (keyMenu.Key.Equals(ConsoleKey.D1))
                    {
                        stupidAI = new StupidAI(gameProcess, gameProcess.Player2);
                    }

                    GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2,
                        gameProcess.GetCurrentPlayer(), gameProcess.Walls, availableMoves, message);

                    while (true)
                    {
                        var keyGame = Console.ReadKey(true);

                        if (keyGame.Key.Equals(ConsoleKey.Escape))
                        {
                            break;
                        }

                        if (!gameOver)
                        {
                            availableMoves = null;
                            message = null;

                            if (keyGame.Key.Equals(ConsoleKey.UpArrow))
                            {
                                GameView.MoveCursorUp();

                                continue;
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.DownArrow))
                            {
                                GameView.MoveCursorDown();

                                continue;
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.LeftArrow))
                            {
                                GameView.MoveCursorLeft();

                                continue;
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.RightArrow))
                            {
                                GameView.MoveCursorRight();

                                continue;
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.Spacebar))
                            {
                                availableMoves = gameProcess.GetCurrentPlayerAvailableMoves();
                                message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name);
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.Q))
                            {
                                Position playerMovePosition = GameView.GetPlayerMovePosition();
                                bool isPlayerMove = false;

                                if (!playerMovePosition.Equals(GameView.WRONG_POSITION))
                                {
                                    isPlayerMove = gameProcess.MoveCurrentPlayer(playerMovePosition);
                                }

                                if (!isPlayerMove)
                                {
                                    message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name) + GetWrongPlayerPositionMessage();
                                }
                                else
                                {
                                    gameOver = gameProcess.CheckCurrentPlayerWin();

                                    if (gameOver)
                                    {
                                        message = GetWinMessage(gameProcess.GetCurrentPlayer().Name);
                                    }
                                    else
                                    {
                                        gameProcess.ChangeCurrentPlayer();
                                        message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name);
                                    }
                                }
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.W) || keyGame.Key.Equals(ConsoleKey.E))
                            {
                                Position wallPosition = GameView.GetWallPosition();
                                bool isWallAdded = false;

                                if (!wallPosition.Equals(GameView.WRONG_POSITION))
                                {
                                    isWallAdded = gameProcess.AddCurrentPlayerWall(wallPosition, keyGame.Key.Equals(ConsoleKey.W));
                                }

                                if (!isWallAdded)
                                {
                                    if (gameProcess.GetCurrentPlayer().WallCount > 0)
                                    {
                                        message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name) + GetWrongWallPositionMessage();
                                    }
                                    else
                                    {
                                        message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name) + GetWallsAreOverMessage();
                                    }
                                }
                                else
                                {
                                    gameOver = gameProcess.CheckCurrentPlayerWin();

                                    if (gameOver)
                                    {
                                        message = GetWinMessage(gameProcess.GetCurrentPlayer().Name);
                                    }
                                    else
                                    {
                                        gameProcess.ChangeCurrentPlayer();
                                        message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name);
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }

                            GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2,
                                            gameProcess.GetCurrentPlayer(), gameProcess.Walls, availableMoves, message);
                        }

                        if (!gameOver && stupidAI != null && gameProcess.GetCurrentPlayer().Equals(stupidAI.Player))
                        {
                            stupidAI.Move();

                            gameOver = gameProcess.CheckCurrentPlayerWin();

                            if (gameOver)
                            {
                                message = GetWinMessage(gameProcess.GetCurrentPlayer().Name);
                            }
                            else
                            {
                                gameProcess.ChangeCurrentPlayer();
                                message = GetCurrentPlayerMessage(gameProcess.GetCurrentPlayer().Name);
                            }

                            GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2,
                                    gameProcess.GetCurrentPlayer(), gameProcess.Walls, null, message);
                        }
                    }

                    GameView.RenderMenu();
                }
                else if (keyMenu.Key.Equals(ConsoleKey.D0))
                {
                    GameView.RenderInfo();

                    while (true)
                    {
                        var keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key.Equals(ConsoleKey.Escape))
                        {
                            break;
                        }
                    }

                    GameView.RenderMenu();
                }
                else if (keyMenu.Key.Equals(ConsoleKey.Escape))
                {
                    Console.Clear();

                    break;
                }
            }
        }

        private static string GetCurrentPlayerMessage(string playerName)
        {
            return $"> CURRENT PLAYER: {playerName}\n";
        }

        private static string GetWrongPlayerPositionMessage()
        {
            return $"> WRONG PLAYER POSITION";
        }

        private static string GetWrongWallPositionMessage()
        {
            return $"> WRONG WALL POSITION";
        }

        private static string GetWallsAreOverMessage()
        {
            return "> THE WALLS ARE OVER";
        }

        private static string GetWinMessage(string playerName)
        {
            return $"> {playerName} WINS!!!\n";
        }
    }
}
