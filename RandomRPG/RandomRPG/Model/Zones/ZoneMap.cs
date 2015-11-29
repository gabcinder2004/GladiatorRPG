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
            Tile tile = new Tile {OccupyingUnit = unit, x = x, y = y};
            _map[x, y] = tile;
            unit.CurrentTile = tile;
            //Give unit Reference to their tile.
        }

        public Tile GetTile(int x, int y)
        {
            return _map[x, y];
        }

        public void MoveUnit(int x, int y, IUnit glad)
        {
            _map[glad.CurrentTile.x, glad.CurrentTile.y] = null;
            glad.CurrentTile.y = y;
            glad.CurrentTile.x = x;
            _map[x, y] = (Tile)glad.CurrentTile;
        }
    }
}
