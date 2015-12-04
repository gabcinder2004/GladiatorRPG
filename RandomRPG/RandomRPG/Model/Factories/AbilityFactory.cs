using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.ArmorMitigation;
using RandomRPG.Model.Attacks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Factories
{
    class AbilityFactory
    {
        public static List<IAbilities> GetBaseAbilityList(GladiatorTypes gladType)
        {
            switch (gladType.ToString())
            {
                case "Doctore":
                    return new List<IAbilities>
                    {
                        {new Bash()}
                    };

                case "Slave":
                    return new List<IAbilities>
                    {
                        {new Bash()},
                        {new Dodge()}
                    };

                case "Krixus":
                    return new List<IAbilities>
                    {
                        {new Bash()}
                    };

                default:
                    return new List<IAbilities>
                    {
                        {new Bash()}
                    };
            }
        }
    }
}
