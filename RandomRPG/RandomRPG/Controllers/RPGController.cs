using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Utilities;

namespace RandomRPG.Controllers
{
    public class RpgController
    {
        public void MainMenu()
        {
            Text.Divider();

            var menuOptions = EnumUtil.GetValues<MainMenuOptions>().ToList();
            var menuString = string.Empty;

            for (var i = 0; i < menuOptions.Count(); i++)
            {
                menuString += $"{i+1}) {menuOptions[i]} {Environment.NewLine}";
            }

            var choice = Text.Prompt<int>(string.Format(Resources.MainMenu, menuString)) - 1;

            if (menuOptions.Count < choice) return;

            switch (menuOptions[choice])
            {
                case MainMenuOptions.StartNewGame:
                    Program.GameState = GameState.Start;
                    break;

                case MainMenuOptions.LoadGame:
                    Text.ColorWriteLine("Not yet implemented", ConsoleColor.Yellow);
                    Console.Read();
                    break;

                case MainMenuOptions.Quit:
                    Program.GameState = GameState.Quit;
                    break;
            }

            Text.Clear();
        }

        public void StartNewGame()
        {
            Text.WriteLine(Resources.CharacterCreation_Intro);
            Text.Divider();
            var name = Text.Prompt(Resources.CharacterCreation_Name);
            //Dont need slave class anymore, use GladType
            //Program.Player.CurrentGladiator = new Slave(name);
            Program.Player.Gladiators.Add(Program.Player.CurrentGladiator);
            Program.GameState = GameState.Playing;
        }

        public void QuitGame()
        {
            Text.Divider();
            Text.WriteLine("Are you sure you want to quit the game? (y/n)");
            Text.Divider();

            ConsoleKeyInfo quitKey = Console.ReadKey();

            if (quitKey.KeyChar == 'y')
            {
                Program.RunningGame = false;
            }
            else
            {
                Program.GameState = GameState.Menu;
                Text.Clear();
            }
        }
    }
}
