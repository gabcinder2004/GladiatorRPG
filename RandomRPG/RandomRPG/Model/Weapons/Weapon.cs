using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
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
            double attackdmg = rand.Next(MinDamage, MaxDamage);
            return Convert.ToInt32(Math.Round(attackdmg));
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