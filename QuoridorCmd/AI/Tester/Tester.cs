using QuoridorCmd.Model;
using System;

namespace QuoridorCmd.AI.Tester
{
    class Tester
    {
        public static void Start()
        {
            GameProcess gameProcess = new GameProcess();
            StupidAI stupidAI = null;

            Command inputPlayerColorCommand = Command.ParseString(Console.ReadLine());

            if (inputPlayerColorCommand.Action.Equals(CommandAction.BLACK))
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

                bool isCommandCorrect = false;

                while (!isCommandCorrect)
                {
                    Command inputCommand = Command.ParseString(Console.ReadLine());

                    if (inputCommand.Action.Equals(CommandAction.WALL))
                    {
                        Wall wall = new Wall(inputCommand.Value);
                        isCommandCorrect = gameProcess.AddCurrentPlayerWall(wall);
                    }
                    else
                    {
                        Position position = new Position(inputCommand.Value);
                        isCommandCorrect = gameProcess.MoveCurrentPlayer(position);
                    }
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
