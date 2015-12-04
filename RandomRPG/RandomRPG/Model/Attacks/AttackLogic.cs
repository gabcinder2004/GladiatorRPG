using System;
using System.Collections.Generic;
using System.Linq;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Attacks
{
    public static class AttackLogic
    {

        public static int AttackActionHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, GladiatorTypes type, List<IAttribute> attributes)
        {
            switch (type.ToString())
            {
                case "Doctore":
                    return DoctoreAttackHandler(command, weaponSet, attributes);
                case "Slave":
                    return SlaveAttackHandler(command, weaponSet, attributes);
                case "Krixus":
                    return KrixusAttackHandler(command, weaponSet, attributes);
                case "Villager":
                    return VillagerAttackHandler(command, weaponSet, attributes);
                default:
                    return 0;

            }
        }

        private static int DoctoreAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
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

        private static int SlaveAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int KrixusAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        private static int VillagerAttackHandler(string command, Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            switch (command.ToLower())
            {
                case "bash":
                    return new Bash(weaponSet, attributes).Execute();
                default:
                    return 0;

            }
        }

        public static int GetBaseAttackDmg(Dictionary<BodyPart, IWeapon> weaponSet, GladiatorTypes type,
            List<IAttribute> attributes)
        {
            int dmg = Convert.ToInt32(attributes.First(x => x.Type == AttributeType.Strength).Value * .25);
            foreach (var weapon in weaponSet)
            {
                dmg += weapon.Value.DamageOutput();
            }
            if (weaponSet.Count > 1)
                return Convert.ToInt32(dmg*.80);

            return dmg;
        }
    }
}