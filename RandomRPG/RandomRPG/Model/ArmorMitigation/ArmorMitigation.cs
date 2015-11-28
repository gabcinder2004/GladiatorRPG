using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public abstract class ArmorMitigation
    {
        protected Dictionary<BodyPart, IArmor> armorSet;
        protected Attributes attributes;
        protected int BaseBonus;

        protected ArmorMitigation(Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
        {
            this.armorSet = armorSet;
            this.attributes = attributes;
        }

        public abstract int Execute();
    }
}