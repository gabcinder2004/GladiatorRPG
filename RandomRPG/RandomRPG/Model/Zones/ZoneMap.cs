using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Zones
{
    public class ZoneMap
    {
        private readonly Tile[,] _map;

        public ZoneMap(int width, int height)
        {
            MapWidth = width;
            MapHeight = height;
            _map = new Tile[width, height];
        }

        public int MapWidth { get; set; }
        public int MapHeight { get; set; }

        public void SetTile(int x, int y, IUnit unit) 
        {
            _map[x, y] = new Tile {OccupyingUnit = unit};
        }

        public Tile GetTile(int x, int y)
        {
            return _map[x, y];
        }
    }
}
