using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Controllers;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Factories;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Zones;
using RandomRPG.Utilities;

namespace RandomRPG
{
    class Program
    {
        #region Declarations

        public const int ConsoleWidth = 75;
        public const int ConsoleHeight = 30;
        public static bool RunningGame = true;

        public static GameState GameState = GameState.Menu;
        public static RpgController RpgController = new RpgController();
        #endregion

        private static void Main(string[] args)
        {
            Text.Clear();

            Console.WindowWidth = ConsoleWidth;
            Console.WindowHeight = ConsoleHeight;
            Console.BufferHeight = ConsoleHeight;
            Console.BufferWidth = ConsoleWidth;

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
                        Text.Clear();
                        if (Player.Instance.CurrentGladiator.CurrentZone == null)
                        {
                            IZone currentZone = ZoneFactory.GetZone(ZoneLevel.One);
                            Header.Map = currentZone.Map;
                            Player.Instance.CurrentGladiator.CurrentZone = currentZone;
                        }

                        Player.Instance.CurrentGladiator.CurrentZone.StateChanged(GameEvent.ZoneEnter);
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
