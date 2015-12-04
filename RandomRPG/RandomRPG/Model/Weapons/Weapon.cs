using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Weapons
{
    //Going to do same with this as gladiator, also think about adding attributes to items.
    public class Weapon : IWeapon
    {
        public string Name { get; set; }

        public WeaponTypes Type { get; set; }

        public List<BodyPart> EquipLocations { get; set; }

        public int Durability { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int DamageOutput()
        {
            Random rand = new Random();
            int attackdmg = rand.Next(MinDamage, MaxDamage+1);
            return attackdmg;
        }

        public Weapon(int minDamage, int maxDamage, WeaponTypes type, int durability, List<BodyPart> equipLocations )
        {
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.Name = type.ToString();
            this.Type = type;
            this.Durability = durability;
            this.EquipLocations = equipLocations;
        }

    }
}