using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public abstract class AttackAbilities : IAbilities
    {
        protected Dictionary<BodyPart, IWeapon> weaponSet;
        protected Attributes attributes;
        protected int attackTypeBonus;
        public abstract string AbilityName { get; set; }

        protected AttackAbilities(Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes)
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
            int strbonusModifier = Convert.ToInt32(Math.Round(attributes.Strength * .25));
            int totalDmg = strbonusModifier;
            foreach (var key in weaponSet)
            {
                int critIndicator = new Random().Next(0, 100);
                if (critIndicator <= attributes.CritChance)
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