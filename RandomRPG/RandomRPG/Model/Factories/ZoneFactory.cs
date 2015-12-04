using System;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Zones;

namespace RandomRPG.Model.Factories
{
    public static class ZoneFactory
    {
        public static IZone GetZone(ZoneLevel level)
        {
            switch (level)
            {
                case ZoneLevel.One:
                    return Zone1.Instance;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
