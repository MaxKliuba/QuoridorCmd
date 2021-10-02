using System;
using System.Collections.Generic;
using System.Numerics;

namespace QuoridorConsole
{
    class StupidAI
    {
        GameProcess GameProcess { get; }

        public Player Player { get; }

        public StupidAI(GameProcess gameProcess, Player player)
        {
            GameProcess = gameProcess;
            Player = player;
        }

        public void Move()
        {
            Random random = new Random();

            if (random.Next(0, 2) == 0 && Player.WallCount > 0)
            {
                while (!GameProcess.AddWall(Player,
                    new Vector2(random.Next(0, GameProcess.Board.Size - 2), random.Next(0, GameProcess.Board.Size - 2)),
                    random.Next(0, 2) == 0)) ;
            }
            else
            {
                List<Vector2> availableMoves = GameProcess.GetPlayerAvailableMoves(Player);

                while (!GameProcess.MovePlayer(Player, availableMoves[random.Next(0, availableMoves.Count)])) ;
            }
        }
    }
}
