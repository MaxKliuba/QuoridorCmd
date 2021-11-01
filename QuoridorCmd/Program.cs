using QuoridorCmd.Controller;
using QuoridorCmd.AI.Tester;
using System;

namespace QuoridorCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("--ui"))
                {
                    GameController.Start();
                }
                else
                {
                    Console.WriteLine($"Option '{args[0]}' is unknown.");
                }
            }
            else
            {
                Tester.Start();
            }
        }
    }
}
