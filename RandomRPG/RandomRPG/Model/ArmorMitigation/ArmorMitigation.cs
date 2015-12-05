using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public abstract class ArmorMitigation : IAbilities
    {
        protected Dictionary<BodyPart, IArmor> armorSet;
        protected List<IAttribute> attributes;
        protected int BaseBonus;
        public abstract int EnergyCost { get; set; }

        protected ArmorMitigation()
        {
        }

        public abstract int Execute(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes);


        public abstract string AbilityName { get; set; }
        public abstract string AbilityType { get; set; }
    }
}