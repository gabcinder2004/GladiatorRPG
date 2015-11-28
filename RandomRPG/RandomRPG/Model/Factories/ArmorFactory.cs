using System.Collections.Generic;
using RandomRPG.Model.Armors;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Factories
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
                        {BodyPart.Chest, GetBaseLeatherArmor()}
                    };

                case "Slave":
                    return new Dictionary<BodyPart, IArmor>()
                    {
                        {BodyPart.Pants, GetBaseUnderWear()}
                    };

                default:
                    return new Dictionary<BodyPart, IArmor>();
            }
        }

        public static IArmor GetBaseLeatherArmor()
        {
            return new Armor(5, 50, ArmorTypes.LeatherArmor, 10, BodyPart.Chest);
        }

        public static IArmor GetBaseUnderWear()
        {
            return new Armor(0, 15, ArmorTypes.UnderWear, 5, BodyPart.Pants);
        }
    }
}