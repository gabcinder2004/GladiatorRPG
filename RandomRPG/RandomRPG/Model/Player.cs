using System.Collections.Generic;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Player
    {
        public Player()
        {
            Gladiators = new List<IGladiator>();
        }

        public IGladiator CurrentGladiator { get; set; }

        public List<IGladiator> Gladiators { get; set; } 
    }
}