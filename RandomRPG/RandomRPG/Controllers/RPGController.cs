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
            Player.Instance.CurrentGladiator = new Gladiator(name, GladiatorTypes.Slave);
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
            Player.Instance.CurrentGladiator.Target.NpcAttack();
            if (Player.Instance.CurrentGladiator.IsAlive)
            {
                var name = Text.Prompt<int>("Choose an Ability");
                Player.Instance.CurrentGladiator.Attack(Player.Instance.CurrentGladiator.AbilityList[name - 1].ToString());
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
            // Player.Instance.CurrentGladiator.CurrentZone.StateChanged(GameEvent.ZoneEnter);
            var directionKeys = new Dictionary<int, string>
            {
                {1, "w"},
                {2, "s"},
                {3, "a"},
                {4, "d"}
            };
            var directionsList = EnumUtil.GetValues<Directions>().ToList();

            for (int i = 0; i < directionsList.Count; i++)
            {
                Text.ColorWriteLine(directionKeys[i + 1] + ") " + directionsList[i] + "\n", ConsoleColor.Magenta);
            }

            var direction = Text.Prompt<string>("Where would you like to go?");
            //move direction -1
            //Think about extension method here
            //if spot taken engage battle
            //Zone1.Instance.Map.MoveUnit();
            Player.Instance.CurrentGladiator.CurrentZone.Map.MoveUnit(direction, Player.Instance.CurrentGladiator);
        }
    }

    public enum GameEvent
    {
        ZoneEnter,
        ZoneLeave
    }
}
