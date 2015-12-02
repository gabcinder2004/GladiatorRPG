using System;
using System.Collections.Generic;
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

        public static string Get()
        {
            if (Map == null)
                return Resources.GladiatorLogo;


            var header = $"{BuildMap(Map)}{Environment.NewLine}";
            return header;
        }

        public static string GetCharacterStatus()
        {
            var gladiator = Player.Instance.CurrentGladiator;
            //Add alignment with format modifiers http://www.csharp-examples.net/align-string-with-spaces/, also add colors
            return $"{gladiator.Name}&" +
                   $"Hit Points: {gladiator.Attributes.HitPoints}&" +
                   $"Energy: {gladiator.Attributes.Energy}&" +
                   $"Strength: {gladiator.Attributes.Strength}&" +
                   $"Agility: {gladiator.Attributes.Agility}&" +
                   $"Vitality: {gladiator.Attributes.Vitality}&" +
                   $"Crit Chance: {gladiator.Attributes.CritChance}&" +
                   $"Kills: {gladiator.Kills}&";

        }

        public static string BuildMap(ZoneMap map)
        {
            if (Program.GameState == GameState.Battle)
            {
                return string.Empty;
            }
            int counter = 0;
            var array = GetCharacterStatus().Split('&');
            var result = string.Empty;

            for (int x = 0; x < map.MapWidth; x++)
            {
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
                if (counter < array.Length-1)
                {
                    result += "                              " + array[counter];
                    counter ++;
                }
                result += Environment.NewLine;
            }

            return result;
        }
    }
}