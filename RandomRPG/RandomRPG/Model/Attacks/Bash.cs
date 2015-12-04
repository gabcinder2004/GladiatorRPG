using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Attacks
{
    public class Bash : AttackAbilities
    {
      
        //Energu cost associated
        public override string AbilityName { get; set; }
        public override string AbilityType { get; set; }
        public override int EnergyCost { get; set; }
        public Bash(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes) : base(weaponSet, attributes)
        {
            this.attackTypeBonus = 5;
            this.AbilityName = "Bash";
            this.EnergyCost = 10;
        }

        public override int Execute()
        {
            return base.Execute() + this.attackTypeBonus;

        }

        public Bash()
        {
            this.AbilityName = "Bash";
            this.AbilityType = "Offensive";
        }

        public override string ToString()
        {
            return this.AbilityName;
        }
    }
}