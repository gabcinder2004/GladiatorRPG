using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Units
{
    public class Krixus : NpcGladiator
    {
        public Krixus(string name)
            : base(name, GladiatorTypes.Krixus)
        {         
        }

        public override int GetBaseAttackDmg(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes)
        {
            int dmg = Convert.ToInt32(attributes.First(x => x.Type == AttributeType.Strength).Value * .25);
            foreach (var weapon in weaponSet)
            {
                dmg += weapon.Value.DamageOutput();
            }

            return dmg;
        }

        public override int GetBaseDmgMitigation(Dictionary<BodyPart, IArmor> armorSet, List<IAttribute> attributes)
        {
            var strength = attributes.First(x => x.Type == AttributeType.Strength);
            int dmgMitigation = Convert.ToInt32(strength.Value * .2);
            foreach (var item in armorSet)
            {
                dmgMitigation += item.Value.ArmorValue;
            }

            return dmgMitigation;
        }
    }
}
