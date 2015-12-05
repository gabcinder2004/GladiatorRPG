using System;
using System.Threading;
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
            Tile tile = new Tile { OccupyingUnit = unit, x = x, y = y };
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
            if (IsOutOfBounds(x, y))
            {
                Text.WriteLine("");
                Text.ColorWriteLine("Cant move there!", ConsoleColor.DarkRed);
                Thread.Sleep(2000);
                return;
            }

            var unitOnTile = GetTile(x, y);

            if (unitOnTile == null)
            {
                _map[glad.CurrentTile.x, glad.CurrentTile.y] = null;
                glad.CurrentTile.y = y;
                glad.CurrentTile.x = x;
                _map[x, y] = (Tile)glad.CurrentTile;
                return;
            }
            
            IUnit target = GetTile(x, y).OccupyingUnit;
            (glad as IGladiator).Target = target;
            Program.GameState = GameState.Interacting;
        }

        private bool IsOutOfBounds(int x, int y)
        {
            return (x > MapWidth - 1 || y > MapHeight - 1 || x < 0 || y < 0);
        }

        public void MoveUnit(char direction, IUnit glad)
        {
            switch (direction)
            {
                case 'w':
                    MoveUnit(glad.CurrentTile.x - 1, glad.CurrentTile.y, glad);
                    break;
                case 's':
                    MoveUnit(glad.CurrentTile.x + 1, glad.CurrentTile.y, glad);
                    break;
                case 'd':
                    MoveUnit(glad.CurrentTile.x, glad.CurrentTile.y + 1, glad);
                    break;
                case 'a':
                    MoveUnit(glad.CurrentTile.x, glad.CurrentTile.y - 1, glad);
                    break;
            }
        }
    }
}
