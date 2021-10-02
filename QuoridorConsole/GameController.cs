using System;
using System.Numerics;

namespace QuoridorConsole
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
                    Player currentPlayer = gameProcess.Player1;
                    StupidAI stupidAI = null;
                    bool gameOver = false;

                    if (keyMenu.Key.Equals(ConsoleKey.D1))
                    {
                        stupidAI = new StupidAI(gameProcess, gameProcess.Player2);
                    }

                    GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                        gameProcess.Walls, null, $"> CURRENT PLAYER: {currentPlayer.Name}\n");

                    while (true)
                    {
                        if (!gameOver && stupidAI != null && currentPlayer.Equals(stupidAI.Player))
                        {
                            stupidAI.Move();

                            gameOver = gameProcess.CheckPlayerWin(currentPlayer);
                            if (gameOver)
                            {
                                GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                    gameProcess.Walls, null, $"> {currentPlayer.Name} WINS!!!\n");

                                continue;
                            }

                            if (currentPlayer.Equals(gameProcess.Player1))
                            {
                                currentPlayer = gameProcess.Player2;
                            }
                            else
                            {
                                currentPlayer = gameProcess.Player1;
                            }

                            GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                gameProcess.Walls, null, $"> CURRENT PLAYER: {currentPlayer.Name}\n");
                        }

                        var keyGame = Console.ReadKey(true);

                        if (keyGame.Key.Equals(ConsoleKey.Escape))
                        {
                            break;
                        }

                        if (!gameOver)
                        {
                            if (keyGame.Key.Equals(ConsoleKey.UpArrow))
                            {
                                GameView.MoveCursorUp();
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.DownArrow))
                            {
                                GameView.MoveCursorDown();
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.LeftArrow))
                            {
                                GameView.MoveCursorLeft();
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.RightArrow))
                            {
                                GameView.MoveCursorRight();
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.Spacebar))
                            {
                                GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                    gameProcess.Walls, gameProcess.GetPlayerAvailableMoves(currentPlayer), $"> CURRENT PLAYER: {currentPlayer.Name}\n");
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.Q))
                            {
                                Vector2 playerMovePosition = GameView.GetPlayerMovePosition();
                                bool isPlayerMove = false;
                                string message = null;

                                if (!playerMovePosition.Equals(GameView.WRONG_POSITION))
                                {
                                    isPlayerMove = gameProcess.MovePlayer(currentPlayer, playerMovePosition);
                                }

                                if (!isPlayerMove)
                                {
                                    message = $"> CURRENT PLAYER: {currentPlayer.Name}\n" +
                                            $"> WRONG PLAYER POSITION";
                                }
                                else
                                {
                                    gameOver = gameProcess.CheckPlayerWin(currentPlayer);
                                    if (gameOver)
                                    {
                                        GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                            gameProcess.Walls, null, $"> {currentPlayer.Name} WINS!!!\n");

                                        continue;
                                    }

                                    if (currentPlayer.Equals(gameProcess.Player1))
                                    {
                                        currentPlayer = gameProcess.Player2;
                                    }
                                    else
                                    {
                                        currentPlayer = gameProcess.Player1;
                                    }

                                    message = $"> CURRENT PLAYER: {currentPlayer.Name}\n";
                                }

                                GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                    gameProcess.Walls, null, message);
                            }
                            else if (keyGame.Key.Equals(ConsoleKey.W))
                            {
                                Vector2[] wallPosition = GameView.GetWallPosition();
                                bool isWallAdded = false;
                                string message = null;

                                if (!wallPosition[0].Equals(GameView.WRONG_POSITION) && !wallPosition[1].Equals(GameView.WRONG_POSITION))
                                {
                                    isWallAdded = gameProcess.AddWall(currentPlayer, wallPosition[0], wallPosition[1]);
                                }

                                if (!isWallAdded)
                                {
                                    if (currentPlayer.WallCount > 0)
                                    {
                                        message = $"> CURRENT PLAYER: {currentPlayer.Name}\n" +
                                            $"> WRONG WALL POSITION";
                                    }
                                    else
                                    {
                                        message = $"> CURRENT PLAYER: {currentPlayer.Name}\n" +
                                            "> THE WALLS ARE OVER";
                                    }
                                }
                                else
                                {
                                    gameOver = gameProcess.CheckPlayerWin(currentPlayer);
                                    if (gameOver)
                                    {
                                        GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                            gameProcess.Walls, null, $"> {currentPlayer.Name} WINS!!!\n");

                                        continue;
                                    }

                                    if (currentPlayer.Equals(gameProcess.Player1))
                                    {
                                        currentPlayer = gameProcess.Player2;
                                    }
                                    else
                                    {
                                        currentPlayer = gameProcess.Player1;
                                    }

                                    message = $"> CURRENT PLAYER: {currentPlayer.Name}\n";
                                }

                                GameView.RenderGameBoard(gameProcess.Board, gameProcess.Player1, gameProcess.Player2, currentPlayer.Equals(gameProcess.Player1),
                                    gameProcess.Walls, null, message);
                            }
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
    }
}
