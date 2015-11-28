using RandomRPG.Model.Interfaces;

namespace RandomRPG.Utilities
{
    public class Tile : ITile
    {
        public IUnit OccupyingUnit { get; set; }
    }

    public interface ITile
    {
        IUnit OccupyingUnit { get; set; }
    }
}