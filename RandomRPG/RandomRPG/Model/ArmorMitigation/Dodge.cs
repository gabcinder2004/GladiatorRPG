using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public class Dodge : ArmorMitigation
    {
        public override string AbilityName { get; set; }

        public Dodge(Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
            : base(armorSet, attributes) { }

        public override int Execute()
        {
            return 1;
        }
    }
}