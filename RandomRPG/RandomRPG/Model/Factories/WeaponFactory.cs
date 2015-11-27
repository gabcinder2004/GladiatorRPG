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
                        { BodyPart.LeftHand, new Mace()},
                        { BodyPart.RightHand, new Mace()}
                    };

                default:
                    return new Dictionary<BodyPart, IWeapon>();
            }
        }
    }
}