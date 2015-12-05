using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;

namespace RandomRPG.Model.Factories
{
    public class VillagerFactory
    {
        public static Villager GetVillager()
        {
            return new Villager(NameGenerator.GenerateRandom(), 
                AttributeFactory.GetInstance("Villager"), 
                Reputation.Friendly, 
                new List<string> { "Hi, how's it going?", "You're ugly." });
        }
    }
}