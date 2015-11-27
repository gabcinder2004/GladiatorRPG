using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public abstract class AttackAbilities
    {
        protected Dictionary<BodyPart, IWeapon> weaponSet;
        protected Attributes attributes;
        public abstract int Execute();
        protected int attackTypeBonus;

        protected AttackAbilities(Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes)
        {
            this.weaponSet = weaponSet;
            this.attributes = attributes;
        }
    }
}