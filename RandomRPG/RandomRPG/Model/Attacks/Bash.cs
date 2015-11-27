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
            //Will change
            Console.WriteLine("You Bash!");
            return base.Execute() + attackTypeBonus;
        }
    }
}