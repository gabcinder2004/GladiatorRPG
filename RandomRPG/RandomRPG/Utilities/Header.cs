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


            var header = $"{BuildMap(Map)}{Environment.NewLine}{GetCharacterStatus()}";
            return header;
        }

        public static string GetCharacterStatus()
        {
            var gladiator = Player.Instance.CurrentGladiator;

            return $"{gladiator.Name}{Environment.NewLine}" +
                   $"Hit Points: {gladiator.Attributes.HitPoints} {Environment.NewLine}" +
                   $"Strength: {gladiator.Attributes.Strength} {Environment.NewLine}" +
                   $"Agility: {gladiator.Attributes.Agility} {Environment.NewLine}" +
                   $"Vitality: {gladiator.Attributes.Vitality} {Environment.NewLine}" +
                   $"Crit Chance: {gladiator.Attributes.CritChance}";

        }

        public static string BuildMap(ZoneMap map)
        {
            if (Program.GameState == GameState.Battle)
            {
                return string.Empty;
            }

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
                result += Environment.NewLine;
            }

            return result;
        }
    }
}