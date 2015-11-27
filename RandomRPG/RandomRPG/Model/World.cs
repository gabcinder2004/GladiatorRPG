using System.Collections.Generic;

namespace RandomRPG.Model
{
    public class World
    {
        private static World worldInstance { get; set; }

        public static World Instantiate()
        {
            if (worldInstance == null)
            {
                worldInstance = new World();
            }

            return worldInstance;
        }

        public List<Zone> Zones { get; set;}
        public List<IGladiator> Gladiators { get; set; } 
    }
}