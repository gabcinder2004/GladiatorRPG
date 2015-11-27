using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Player
    {
        public string Name { get; set; }

        public Player()
        {
            Gladiators = new List<IGladiator>();
        }

        public void SetTargetGladiator(IGladiator gladiator)
        {
            CurrentGladiator.Target = gladiator;
            Target = CurrentGladiator.Target;
            gladiator.Target = CurrentGladiator;
        }

        public IGladiator Target { get; set; }
        public IGladiator CurrentGladiator { get; set; }

        public List<IGladiator> Gladiators { get; set; }
    }
}