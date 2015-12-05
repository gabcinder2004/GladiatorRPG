using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public class Dodge : ArmorMitigation
    {
        public override string AbilityName { get; set; }
        public override string AbilityType { get; set; }
        public override int EnergyCost { get; set; }

        public Dodge()
        {
            this.AbilityName = "Dodge";
            this.AbilityType = "Defensive";
            this.EnergyCost = 0;
        }

        public override int Execute(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            return 5;
        }
    }
}