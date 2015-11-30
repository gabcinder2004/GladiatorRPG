using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public static class ArmorMitigationLogic
    {

        public static int DefendActionHandler(Dictionary<BodyPart, IArmor> armorSet, GladiatorTypes type, Attributes attributes, string command = "none")
        {
            switch (type.ToString())
            {
                case "Doctore":
                    return DoctoreDefendHandler(command, armorSet, attributes);
                case "Slave":
                    return SlaveDefendHandler(command, armorSet, attributes);
                case "Krixus":
                    return KrixusDefendHandler(command, armorSet, attributes);
                case "Villager":

                    return VillagerDefendHandler(command, armorSet, attributes);
                default:
                    return 0;

            }
        }

        //Each guy has seperate dmg mitigation abilities, may need to incorporate levels access to thise as well
        private static int DoctoreDefendHandler(string command, Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
        {
            //Filter Doctore abilities
            switch (command.ToLower())
            {
                case "block":
                    return new Block(armorSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int SlaveDefendHandler(string command, Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
        {
            switch (command.ToLower())
            {
                case "block":
                    return new Block(armorSet, attributes).Execute();
                case "dodge":
                    return new Dodge(armorSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int KrixusDefendHandler(string command, Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
        {
            switch (command.ToLower())
            {
                case "block":
                    return new Block(armorSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int VillagerDefendHandler(string command, Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
        {
            switch (command.ToLower())
            {
                case "block":
                    return new Block(armorSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        public static int GetBaseDmgMitigation(Dictionary<BodyPart, IArmor> armorSet, GladiatorTypes type, Attributes attributes)
        {
            //perhaps have a bade dmg mitigation bonus to certain types
            int dmgMitigation = Convert.ToInt32(attributes.Strength*.1);
            foreach (var item in armorSet)
            {
                dmgMitigation += item.Value.ArmorValue;
            }

            return dmgMitigation;
        }
    }
}
