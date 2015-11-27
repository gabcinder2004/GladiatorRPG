using System.Collections.Generic;
using RandomRPG.Model.Armor;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public static class ArmorFactory
    {

        public static Dictionary<BodyPart, IArmor> GetBaseArmorInstances(GladiatorTypes gladType)
        {
            switch (gladType.ToString())
            {
                case "Doctore":
                    return new Dictionary<BodyPart, IArmor>()
                    {
                        {BodyPart.Chest, new LeatherArmor()}
                    };

                case "Slave":
                    return new Dictionary<BodyPart, IArmor>()
                    {
                        {BodyPart.Chest, new Underwear()}
                    };

                default:
                    return new Dictionary<BodyPart, IArmor>();
            }
        }
    }
}