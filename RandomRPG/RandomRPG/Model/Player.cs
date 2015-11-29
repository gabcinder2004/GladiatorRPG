using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Player
    {
        private static Player _instance;
        public static Player Instance => _instance ?? (_instance = new Player());

        public string Name { get; set; }

        private Player()
        {
            Gladiators = new List<IGladiator>();
        }

        public IGladiator Target { get; set; }
        public IGladiator CurrentGladiator { get; set; }

        public List<IGladiator> Gladiators { get; set; }
    }
}