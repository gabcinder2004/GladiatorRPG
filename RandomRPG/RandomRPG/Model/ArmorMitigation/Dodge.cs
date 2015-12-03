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
        public Dodge(Dictionary<BodyPart, IArmor> armorSet, List<IAttribute> attributes)
            : base(armorSet, attributes) { }

        public Dodge()
        {
            this.AbilityName = "Dodge";
            this.AbilityType = "Defensive";
            this.EnergyCost = 50;
        }

        public override int Execute()
        {
            return 5;
        }
    }
}