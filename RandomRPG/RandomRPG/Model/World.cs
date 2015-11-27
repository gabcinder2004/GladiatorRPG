using System.Collections.Generic;

namespace RandomRPG.Model
{
    public class World
    {
        public List<Zone> Zones { get; set;}
        public List<IGladiator> Gladiators { get; set; } 
    }
}