using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public class Dodge : ArmorMitigation
    {
        public override string AbilityName { get; set; }
        public override int EnergyCost { get; set; }

        public Dodge()
        {
            this.AbilityName = "Dodge";
            this.EnergyCost = 0;
        }

        public override int Execute(Dictionary<BodyPart, IArmor> armorSet, List<IAttribute> attributes)
        {
            return 5;
        }
    }
}