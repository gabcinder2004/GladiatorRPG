using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Bash : AttackAbilities
    {
      
        //Energu cost associated
        public override string AbilityName { get; set; }

        public Bash(Dictionary<BodyPart, IWeapon> weaponSet, Attributes attributes) : base(weaponSet, attributes)
        {
            this.attackTypeBonus = 5;
            this.AbilityName = "Bash";
        }

        public override int Execute()
        {
            //Will change
            return base.Execute() + attackTypeBonus;
        }

        public Bash()
        {
            this.AbilityName = "Bash";
        }

        public override string ToString()
        {
            return this.AbilityName;
        }
    }
}