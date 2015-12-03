using System;
using System.Collections.Generic;
using System.Linq;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public abstract class AttackAbilities : IAbilities
    {
        protected Dictionary<BodyPart, IWeapon> weaponSet;
        protected List<IAttribute> attributes;
        protected int attackTypeBonus;
        public abstract string AbilityName { get; set; }
        public abstract string AbilityType { get; set; }
        public abstract int EnergyCost { get; set; }
        protected AttackAbilities(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            this.weaponSet = weaponSet;
            this.attributes = attributes;
        }

        protected AttackAbilities()
        {
        }

        public virtual int Execute()
        {
            //Will change
            int strbonusModifier = Convert.ToInt32(Math.Round(attributes.First(x => x.Type == AttributeType.Strength).Value * .25));
            int totalDmg = strbonusModifier;
            foreach (var key in weaponSet)
            {
                int critIndicator = new Random().Next(0, 101);
                if (critIndicator <= attributes.First(x => x.Type == AttributeType.CritChance).Value)
                {
                    //crit
                    totalDmg += Convert.ToInt32(Math.Round(key.Value.DamageOutput() * 1.5));
                }
                else
                    totalDmg += key.Value.DamageOutput();
            }

            //need to think about two handers
            if (weaponSet.Keys.Count > 1)
            {
                return Convert.ToInt32(totalDmg * .80);
            }
            else
            {
                return totalDmg;
            }
        }
    }
}