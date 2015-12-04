using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Zones;

namespace RandomRPG.Utilities
{
    public static class Header
    {
        public static bool Logo = true;
        public static ZoneMap Map = null; 

        public static void PrintHeader()
        {
            if (Map == null)
            {
                Text.WriteLine(Resources.GladiatorLogo);
                return;
            }

            if (Program.GameState == GameState.Playing)
            {
                BuildMap(Map);
                PrintCharacterStatus();
                Console.SetCursorPosition(0, 10);
                return;
            } 

            //We're in battle mode
            PrintCharacterStatus();
        }

        public static void PrintCharacterStatus()
        {
            var gladiator = Player.Instance.CurrentGladiator;
            Text.WriteLine(ConsoleSide.Right, 1, gladiator.Name + " Stats:");
            for (int i = 0; i < gladiator.Attributes.Count; i++)
            {
                var attribute = gladiator.Attributes[i];
                Text.WriteLine(ConsoleSide.Right, i+2, $"{attribute.Type}: {attribute.Value}");
            }
            if (gladiator.Target != null)
            {
                var hp = gladiator.Target.Attributes.First(x => x.Type == AttributeType.HitPoints);
                var energy  = gladiator.Target.Attributes.First(x => x.Type == AttributeType.Energy);
                Text.WriteLine(ConsoleSide.Left, 1,gladiator.Target.Name + " Stats:");
                Text.WriteLine(ConsoleSide.Left, 2,gladiator.Target.Name + " " + hp.Type + ": " + hp.Value);
                Text.WriteLine(ConsoleSide.Left, 3,gladiator.Target.Name + " " + energy.Type + ": " + energy.Value);
            }
        }

        public static void BuildMap(ZoneMap map)
        {

            for (int x = 0; x < map.MapWidth; x++)
            {
                var result = string.Empty;

                for (int y = 0; y < map.MapHeight; y++)
                {
                    var tile = map.GetTile(x, y);
                    if (tile == null)
                    {
                        result += "[ ]";
                    }
                    else
                    {
                        if (tile.OccupyingUnit == Player.Instance.CurrentGladiator)
                        {
                            //change color here somehow
                            result += "[*]";
                        }
                        else
                        {
                            result += $"[{tile.OccupyingUnit.Name[0]}]";
                        }
                    }
                }

                Text.WriteLine(ConsoleSide.Left, x, result);
            }
        }
    }
}