using System;

namespace RandomRPG.Model
{
    public class Mace : IWeapon
    {
        public string Name => "Mace";

        public int Durability => 50;

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public double DamageOutput()
        {
            Random rand = new Random();
            return rand.Next(MinDamage, MaxDamage);
        }
    }
}