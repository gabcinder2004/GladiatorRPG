using System;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Armors
{
    public class Armor : IArmor
    {
        public BodyPart EquipLocation { get; set; }

        public int ArmorValue { get; set; }

        public int Durability { get; set; }

        public string Name { get; set; }

        public ArmorTypes Type { get; set; }

        public Armor(int armorMin, int durability, ArmorTypes type, int armorMax, BodyPart equipLocation)
        {
            this.Name = type.ToString();
            this.Durability = durability;
            this.EquipLocation = BodyPart.Chest;
            this.ArmorValue = GetArmorValue(armorMin, armorMax);
            this.EquipLocation = equipLocation;
        }

        private int GetArmorValue(int armorMin, int armorMax)
        {
            double armorValue = new Random().Next(armorMin, armorMax);
            return (int)Math.Round(armorValue);
        }
    }
}