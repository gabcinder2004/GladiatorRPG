using System.Collections.Generic;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class World
    {
        private static World _instance;

        public static World Instance => _instance ?? (_instance = new World());

        public List<Zone> Zones { get; set; }
        public List<IGladiator> Gladiators { get; set; }
    }
}