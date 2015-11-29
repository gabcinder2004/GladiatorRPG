using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public static class WeaponFactory
    {

        public static Dictionary<BodyPart, IWeapon> GetBaseWeaponInstances(GladiatorTypes gladType)
        {
            switch (gladType.ToString())
            {
                case "Doctore":
                    return new Dictionary<BodyPart, IWeapon>()
                    {
                        { BodyPart.LeftHand, GetBaseMaceInstance()},
                        { BodyPart.RightHand, GetBaseMaceInstance()}
                    };

                case "Slave":
                    return new Dictionary<BodyPart, IWeapon>()
                    {
                        { BodyPart.LeftHand, GetBaseMaceInstance()}
                    };

                case "Krixus":
                    return new Dictionary<BodyPart, IWeapon>()
                    {
                        { BodyPart.LeftHand, GetBaseMaceInstance()},
                        { BodyPart.RightHand, GetBaseMaceInstance()}
                    };

                default:
                    return new Dictionary<BodyPart, IWeapon>();
            }
        }

        //Add Other Weapons
        public static IWeapon GetBaseMaceInstance()
        {
            return new Weapon(2, 8, WeaponTypes.Mace, 50, new List<BodyPart> { BodyPart.LeftHand, BodyPart.RightHand});
        }
    }
}