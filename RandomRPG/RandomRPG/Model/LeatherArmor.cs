using System;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public class LeatherArmor : IArmor
    {
        public BodyPart EquipLocation { get; set; }

        public int ArmorValue { get; set; }

        public int Durability { get; set; }

        public string Name { get; set; }

        public LeatherArmor(int armorMin = 5, int durability = 50, string name = "Leather Armor", int armorMax = 10)
        {
            this.Name = name;
            this.Durability = durability;
            this.EquipLocation = BodyPart.Chest;
            this.ArmorValue = GetArmorValue(armorMin, armorMax);
        }

        private int GetArmorValue(int armorMin, int armorMax)
        {
            double armorValue = new Random().Next(armorMin, armorMax);
            return (int)Math.Round(armorValue);
        }
    }
}