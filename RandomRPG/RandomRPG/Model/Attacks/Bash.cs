using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Bash : AttackAbilities
    {
      
        
        public Bash(Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes) : base(weaponSet, attributes)
        {
            this.attackTypeBonus = 5;
        }

        public override int Execute()
        {

            Console.WriteLine("You Bash!");
            int strbonusModifier = Convert.ToInt32(Math.Round(attributes.Strength*.25));
            int totalDmg = strbonusModifier + attackTypeBonus;
            foreach (var key in weaponSet)
            {
                int critIndicator = new Random().Next(0, 100);
                if (critIndicator <= 25)
                {
                    //crit
                    totalDmg += Convert.ToInt32(Math.Round(key.Value.DamageOutput()*1.5));
                }
                else
                    totalDmg += key.Value.DamageOutput();
            }

            //need to think about two handers
            if (weaponSet.Keys.Count > 1)
            {
                return Convert.ToInt32(totalDmg*.80);
            }
            else
            {
                return totalDmg;
            }
        }
    }
}