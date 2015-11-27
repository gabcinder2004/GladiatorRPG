using System.Collections.Generic;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class World
    {
        private static World Instance { get; set; }

        public static World Instantiate()
        {
            return Instance ?? (Instance = new World());
        }

        public List<Zone> Zones { get; set;}
        public List<IGladiator> Gladiators { get; set; } 
    }
}