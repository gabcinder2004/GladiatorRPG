using RandomRPG.Model.Enums;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// Base entity in the game for creatures/units. This can be any particular unit.
    /// </summary>
    public interface IUnit
    {
        string Name { get; set; }
        Attributes Attributes { get; set; }
        //T Type { get; set; }
        // ^ We gotta talk about this ^
        ITile CurrentTile { get; set; }
        GladiatorTypes Type { get; set; }
    }
}