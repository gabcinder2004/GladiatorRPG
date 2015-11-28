using System;
using System.Collections.Generic;
using RandomRPG.Controllers;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Zones
{
    public interface IZone
    {
        Action<GameEvent> StateChanged { get; set; }
        List<IZone> ConnectedZones { get; set; } 
        ZoneMap Map { get; set; }
    }
}