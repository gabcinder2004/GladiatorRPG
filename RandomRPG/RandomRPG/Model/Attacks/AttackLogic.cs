using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public static class AttackLogic
    {

        public static int AttackActionHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, GladiatorTypes type, Attributes attributes)
        {
            switch (type.ToString())
            {
                case "Doctore":
                    return DoctoreAttackHandler(command, weaponSet, attributes);
                case "Slave":
                    return SlaveAttackHandler(command, weaponSet, attributes);
                case "Krixus":
                    return KrixusAttackHandler(command, weaponSet, attributes);
                default:
                    return 0;

            }
        }

        private static int DoctoreAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes)
        {
            //Filter Doctore abilities
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int SlaveAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes)
        {
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int KrixusAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes)
        {
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }
    }
}