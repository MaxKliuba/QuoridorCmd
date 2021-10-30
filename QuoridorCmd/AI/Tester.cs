using QuoridorCmd.Model;
using System;

namespace QuoridorCmd.AI
{
    class Tester
    {
        public static void Start()
        {
            GameProcess gameProcess = new GameProcess();
            StupidAI stupidAI = null;

            string inputPlayerColor = Console.ReadLine();

            if (inputPlayerColor.Equals("black"))
            {
                stupidAI = new StupidAI(gameProcess, gameProcess.Player2);
            }
            else
            {
                stupidAI = new StupidAI(gameProcess, gameProcess.Player1);
            }

            while (true)
            {
                if (gameProcess.GetCurrentPlayer().Equals(stupidAI.Player))
                {
                    Console.WriteLine(stupidAI.Move());
                    if (gameProcess.CheckCurrentPlayerWin())
                    {
                        break;
                    }
                    gameProcess.ChangeCurrentPlayer();
                }

                string inputCommand = Console.ReadLine();

                if (inputCommand.Contains("wall"))
                {
                    Wall wall = new Wall(inputCommand.Substring(5));
                    gameProcess.AddCurrentPlayerWall(wall);
                }
                else
                {
                    Position position = new Position(inputCommand.Substring(5));
                    gameProcess.MoveCurrentPlayer(position);
                }

                if (gameProcess.CheckCurrentPlayerWin())
                {
                    break;
                }
                gameProcess.ChangeCurrentPlayer();
            }
        }
    }
}
