using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;
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

        public void RemoveUnit(int x, int y)
        {
            _map[x, y] = null;
        }

        public void MoveUnit(int x, int y, IUnit glad)
        {
            //Add Error handling for going off map buggy right now need to fix
            try
            {
                if (GetTile(x, y).OccupyingUnit != null)
                {
                    Player.Instance.CurrentGladiator.SetTargetGladiator((IGladiator)GetTile(x, y).OccupyingUnit);
                    Program.GameState = GameState.Battle;
                    return;
                }
            }
            catch (Exception)
            {
                if (x > MapWidth-1 || y > MapHeight-1 || x < 0 || y < 0)
                {
                    Text.ColorWriteLine("Cant move there!", ConsoleColor.DarkRed);
                    return;
                }
                _map[glad.CurrentTile.x, glad.CurrentTile.y] = null;
                glad.CurrentTile.y = y;
                glad.CurrentTile.x = x;
                try
                {
                    _map[x, y] = (Tile)glad.CurrentTile;
                }
                catch (Exception)
                {
                    Text.ColorWriteLine("Cant move there!", ConsoleColor.DarkRed);
                }
                
            }
        }

        public void MoveUnit(int direction, IUnit glad)
        {
            switch (direction)
            {
                case 1:
                    MoveUnit(glad.CurrentTile.x -1, glad.CurrentTile.y, glad);
                    break;
                case 2:
                    MoveUnit(glad.CurrentTile.x +1, glad.CurrentTile.y, glad);
                    break;
                case 3:
                    MoveUnit(glad.CurrentTile.x, glad.CurrentTile.y +1, glad);
                    break;
                case 4:
                    MoveUnit(glad.CurrentTile.x, glad.CurrentTile.y-1, glad);
                    break;
            }
        }
    }
}
