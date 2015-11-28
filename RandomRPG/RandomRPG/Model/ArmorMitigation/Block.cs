using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.ArmorMitigation
{
    public class Block : ArmorMitigation
    {

        public Block(Dictionary<BodyPart, IArmor> armorSet, Attributes attributes)
            : base(armorSet, attributes)
        {
        }

        public override int Execute()
        {
            int totalMitigation = 0;
            int strengthBonus = Convert.ToInt32(attributes.Strength * .05);
            int agileBonus = Convert.ToInt32(attributes.Agility * .05);
            IArmor shield;
            //Assuming shields go in righthand if present
            armorSet.TryGetValue(BodyPart.RightHand, out shield);

            if (shield != null)
            {
                totalMitigation += shield.ArmorValue;
            }

            totalMitigation += strengthBonus + agileBonus;

            return totalMitigation;
        }
    }
}