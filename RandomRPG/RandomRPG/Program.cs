using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Controllers;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG
{
    class Program
    {
        #region Declarations

        private const int ConsoleWidth = 73;
        private const int ConsoleHeight = 30;

        public static GameState GameState = GameState.Menu;
        public static RpgController RpgController = new RpgController();
        public static Player Player = new Player();
        public static bool RunningGame = true;

        #endregion

        private static void Main(string[] args)
        {
            Console.WriteLine(Resources.Version);
            Console.WindowWidth = ConsoleWidth;
            Console.WindowHeight = ConsoleHeight;
            Console.BufferHeight = ConsoleHeight;
            Console.BufferWidth = ConsoleWidth;

            Player player = new Player();
            IGladiator mob = new Gladiator(GladiatorTypes.Slave);
            player.CurrentGladiator = new Gladiator(GladiatorTypes.Doctore);
            player.SetTargetGladiator(mob);
            //Must set target to auto attack. Need to think about dmg output etc...
            while (player.Target?.Target != null)

            {
                player.CurrentGladiator.Attack("bash");
                Console.WriteLine(player.Target.Attributes.HitPoints);
                mob.Attack("bash");
                Console.WriteLine(player.CurrentGladiator.Attributes.HitPoints);
            }

            Console.WriteLine("\n" + player.CurrentGladiator);


            //int value = glad.Attack("bash");

            while (RunningGame)
            {
                switch (GameState)
                {
                    case GameState.Menu:
                        RpgController.MainMenu();
                        break;

                    case GameState.Start:
                        RpgController.StartNewGame();
                        break;

                    case GameState.Playing:
                        // TO BE WORKED ON NEXT
                        Text.Clear();
                        Console.WriteLine("You've created your gladiator. Next steps will come soon.");
                        GameState = GameState.Menu;
                        break;

                    case GameState.Battle:
                        break;

                    case GameState.Quit:
                        RpgController.QuitGame();
                        break;

                    case GameState.GameOver:
                        break;
                }
            }
        }
    }
}
