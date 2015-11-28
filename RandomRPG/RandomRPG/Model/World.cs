using System.Collections.Generic;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Zones;

namespace RandomRPG.Model
{
    public class World
    {
        private static World _instance;

        public static World Instance => _instance ?? (_instance = new World());

        public List<IZone> Zones { get; set; }
        public List<IGladiator> Gladiators { get; set; }
    }
}