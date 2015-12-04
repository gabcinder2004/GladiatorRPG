using System;
using System.Collections.Generic;
using RandomRPG.Controllers;
using RandomRPG.Model.Zones;

namespace RandomRPG.Model.Interfaces
{
    public interface IZone
    {
        Action<GameEvent> StateChanged { get; set; }
        List<IZone> ConnectedZones { get; set; } 
        ZoneMap Map { get; set; }
    }
}