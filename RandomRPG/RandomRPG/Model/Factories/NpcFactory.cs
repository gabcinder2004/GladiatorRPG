using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Units;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Factories
{
    public static class NpcFactory
    {
        public static NpcGladiator GetRandomNpcGladiatorInstance()
        {
            var npcList = EnumUtil.GetValues<NpcGladiatorTypes>().ToList();
            var rand = new Random().Next(0, npcList.Count());
            switch (npcList[rand].ToString())
            {
                case "Krixus":
                    return new Krixus("Krixus");
                case "Doctore":
                    return new Doctore("Doctore");
                default:
                    return new Doctore("Doctore");
            }
        }
        public static Villager GetVillager()
        {
            return new Villager(NameGenerator.GenerateRandom(),
                AttributeFactory.GetInstance("Villager"),
                Reputation.Friendly,
                new List<string> { "Hi, how's it going?", "You're ugly." });
        }
    }
}
