using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;
using RandomRPG.Model.Zones;
using RandomRPG.Utilities;

namespace RandomRPG.Controllers
{
    public class RpgController
    {
        public void MainMenu()
        {
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
            Player.Instance.CurrentGladiator = new Slave(name);
            Player.Instance.Gladiators.Add(Player.Instance.CurrentGladiator);
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

        public void Battle()
        {
            //messing around with our API
            int moveX = Player.Instance.CurrentGladiator.Target.CurrentTile.x;
            int moveY = Player.Instance.CurrentGladiator.Target.CurrentTile.y;
            //Figure out good to way display HP so it contantly refreshes
            //Text.ColorWriteLine(Player.Instance.CurrentGladiator.Name + ": " + Player.Instance.CurrentGladiator.Attributes.HitPoints + "(HP)", ConsoleColor.Green);
            ((NpcGladiator) Player.Instance.CurrentGladiator.Target).Attack();
            if (Player.Instance.CurrentGladiator.IsAlive)
            {
                Player.Instance.CurrentGladiator.RegenerateEnergy();
                int ability = Text.Prompt<int>("Choose an Ability");
                Player.Instance.CurrentGladiator.Attack(ability-1);
            }
            //figure out good way to show attacks here
            Thread.Sleep(2000);
            if (Player.Instance.CurrentGladiator.Target == null && Player.Instance.CurrentGladiator.IsAlive)
            {
                Player.Instance.CurrentGladiator.CurrentZone.Map.RemoveUnit(moveX, moveY);
                Player.Instance.CurrentGladiator.CurrentZone.Map.MoveUnit(moveX, moveY, Player.Instance.CurrentGladiator);
                Program.GameState = GameState.Playing;
            }
            if (Player.Instance.CurrentGladiator.Target == null && !Player.Instance.CurrentGladiator.IsAlive)
            {
                Text.ColorWriteLine("Game Over!!!!", ConsoleColor.DarkRed);
                Thread.Sleep(2000);
                Player.Instance.CurrentGladiator.CurrentZone = null;
                Player.Instance.CurrentGladiator.CurrentTile = null;
                Header.Map = null;
                Text.Clear();
                Program.GameState = GameState.Menu;
            }
        }

        public void Navigate()
        {
            var options = new List<MenuOption>()
            {
                new MenuOption('w', "Move up"),
                new MenuOption('s', "Move down"),
                new MenuOption('d', "Move right"),
                new MenuOption('a', "Move left")
            };

            var direction = Text.PromptCharacter("Where would you like to go?", options);
            Player.Instance.CurrentGladiator.CurrentZone.Map.MoveUnit(direction, Player.Instance.CurrentGladiator);
        }
    }

    public enum GameEvent
    {
        ZoneEnter,
        ZoneLeave
    }
}
