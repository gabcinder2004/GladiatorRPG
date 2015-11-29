using System;
using System.Collections.Generic;
using RandomRPG.Model;
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
            return Map == null ? Resources.GladiatorLogo : BuildMap(Map);
        }

        public static string BuildMap(ZoneMap map)
        {
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