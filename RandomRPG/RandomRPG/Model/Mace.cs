using System;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{
    public class Mace : IWeapon
    {
        public string Name => "Mace";

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
    }
}