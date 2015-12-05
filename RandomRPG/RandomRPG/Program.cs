using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
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
        public const int ConsoleHeight = 60;
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
                        if (Player.Instance.CurrentGladiator.CurrentZone == null)
                        {
                            IZone currentZone = ZoneFactory.GetZone(ZoneLevel.One);
                            Header.Map = currentZone.Map;
                            Player.Instance.CurrentGladiator.CurrentZone = currentZone;
                        }

                        Player.Instance.CurrentGladiator.CurrentZone.StateChanged(GameEvent.ZoneEnter);
                        Text.WriteLine("Welcome to the Arena Of Death!.\nNavigate through the arena to kill enemies.");
                        RpgController.Navigate();
                        break;

                    case GameState.Interacting:
                        InteractionController.Interact(Player.Instance.CurrentGladiator, Player.Instance.CurrentGladiator.Target);
                        break;

                    case GameState.Battle:
                        Text.ClearWithAbilities();
                        RpgController.Battle();
                        break;

                    case GameState.Quit:
                        RpgController.QuitGame();
                        break;

                    case GameState.GameOver:
                        var playAgain = Text.PromptCharacter("Would you like to play again?", new List<MenuOption>() {new MenuOption('1', "Yes"), new MenuOption('2', "No")});
                        if (playAgain == '1')
                        {
                            Zone1.Instance.Dispose();
                            GameState = GameState.Menu;
                            Text.Clear();
                            break;
                        }

                        Text.WriteLine("See you soon.");
                        Thread.Sleep(2000);
                        Environment.Exit(1);
                        break;
                }
            }
        }
    }
}
