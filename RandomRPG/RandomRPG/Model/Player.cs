using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;

namespace RandomRPG.Model
{
    public class Player
    {
        private static Player _instance;
        public static Player Instance => _instance ?? (_instance = new Player());

        public string Name { get; set; }

        private Player()
        {
            Gladiators = new List<Gladiator>();
        }

        public Gladiator CurrentGladiator { get; set; }

        public List<Gladiator> Gladiators { get; set; }
    }
}