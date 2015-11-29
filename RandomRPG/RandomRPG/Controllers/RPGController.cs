using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            Player.Instance.CurrentGladiator.DisplayAbilityOptions();
            Player.Instance.CurrentGladiator.CurrentZone.Map.MoveUnit(8, 5, Player.Instance.CurrentGladiator);
            Header.BuildMap(Player.Instance.CurrentGladiator.CurrentZone.Map);
            Player.Instance.CurrentGladiator.CurrentZone.StateChanged(GameEvent.ZoneEnter);
            IGladiator glad = (IGladiator)Player.Instance.CurrentGladiator.CurrentZone.Map.GetTile(5,5).OccupyingUnit;
            Player.Instance.CurrentGladiator.SetTargetGladiator(glad);
            while (Player.Instance.CurrentGladiator.Target != null)
            {
                var name = Text.Prompt("Choose an Ability");
                Player.Instance.CurrentGladiator.Attack(Player.Instance.CurrentGladiator.AbilityList[(int.Parse(name))-1].ToString());
            }

            Text.Prompt(".................");
        }
    }

    public enum GameEvent
    {
        ZoneEnter,
        ZoneLeave
    }
}
