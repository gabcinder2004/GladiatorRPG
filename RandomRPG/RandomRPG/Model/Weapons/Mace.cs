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

        public double DamageOutput()
        {
            Random rand = new Random();
            return rand.Next(MinDamage, MaxDamage);
        }

        public Mace(int minDamage = 2, int maxDamage = 4, string name = "Mace")
        {
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.Name = name + " Mace";
        }
    }
}