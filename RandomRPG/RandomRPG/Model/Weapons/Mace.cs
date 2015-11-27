using System;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Mace : IWeapon
    {
        public string Name { get; set; }

        public int Durability
        {
            get { return 50; }
            set { }
        }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public int DamageOutput()
        {
            Random rand = new Random();
            double attackdmg = rand.Next(MinDamage, MaxDamage);
            return Convert.ToInt32(Math.Round(attackdmg));
        }

        public Mace(int minDamage = 2, int maxDamage = 4, string name = "Mace")
        {
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.Name = name + " Mace";
        }
    }
}