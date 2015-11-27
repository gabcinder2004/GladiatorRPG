using System;

namespace RandomRPG.Model
{
    public interface IItems
    {
        string Name { get; }
        int Durability { get; set; }
        BodyPart EquipLocation { get; }
        double DmgOutput();

    }

    //Probably Make Abstract Classes here for Weapon/Armor, take some stuff out of IItems

    public class Mace : IItems
    {

        public string Name
        {
            get { return "Mace"; }
        }

        public int Durability
        {
            get { return 50; }
            set { }
        }

        public BodyPart EquipLocation
        {
            get
            {
                return BodyPart.MainHand;
            }
        }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public double DmgOutput()
        {
            Random rand = new Random();
            return rand.Next(MinDamage, MaxDamage);
        }
    }
}