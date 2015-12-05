using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public abstract class ArmorMitigation : IDefensiveAbilities
    {
        protected Dictionary<BodyPart, IArmor> armorSet;
        protected List<IAttribute> attributes;
        protected int BaseBonus;
        public abstract int EnergyCost { get; set; }

        protected ArmorMitigation()
        {
        }

        public abstract int Execute(Dictionary<BodyPart, IArmor> armorSet, List<IAttribute> attributes);


        public abstract string AbilityName { get; set; }
    }
}