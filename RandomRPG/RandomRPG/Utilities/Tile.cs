using RandomRPG.Model.Interfaces;

namespace RandomRPG.Utilities
{
    public class Tile : ITile
    {
        public IUnit OccupyingUnit { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public interface ITile
    {
        IUnit OccupyingUnit { get; set; }
        int x { get; set; }
        int y { get; set; }
    }
}