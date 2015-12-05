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

        public override int Execute(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            return base.Execute(weaponSet, attributes) + this.attackTypeBonus;

        }

        public Bash()
        {
            this.AbilityName = "Bash";
            this.AbilityType = "Offensive";
            this.EnergyCost = 10;
            this.attackTypeBonus = 5;
        }

        public override string ToString()
        {
            return this.AbilityName;
        }
    }
}