using System;
using System.Collections.Generic;
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
        List<IAttribute> Attributes { get; set; }
        ITile CurrentTile { get; set; }
        Reputation Reputation { get; set; }
                 
        Action<IGladiator> InteractionTriggered { get; set; }
    }
}