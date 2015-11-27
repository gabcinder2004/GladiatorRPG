using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{    
    public class Doctore : IGladiator
    {

        public Attributes Attributes { get; set; }
        public IWeapon LeftHand { get; set; }
        public IWeapon RightHand { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public string Name { get; set; }
        
        public void BattleCry()
        {
            Console.WriteLine("From the Darkness!");
        }

        public int Attack(string command)
        {
            return 5;
        }

        public Doctore(Attributes attributes, IWeapon rightHand, IWeapon leftHand, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, string name)
        {
            this.Attributes = attributes;
            this.RightHand = rightHand;
            this.LeftHand = leftHand;
            this.Armor = armor;
            this.Inventory = inventory;
            this.Name = name;
        }

        public Doctore()
        {
            this.Attributes = new Attributes()
            {
                Strength = 25,
                Agility = 50,
                CritChance = 25,
                Vitality = 100,
                Energy = 75,
                HitPoints = 200
            };

            this.RightHand = new Mace();
            // this.LeftHand = <SOME THING??>

            this.Armor = new Dictionary<BodyPart, IArmor>()
            {
                {BodyPart.Chest, new LeatherArmor()}
            };

            this.Inventory = new List<IItems>();
            this.Name = "Doctore";
        }

        public override string ToString()
        {
            //Will fix
            IArmor headPiece;
            string head;
            Armor.TryGetValue(BodyPart.Head, out headPiece);
            head = headPiece != null ? headPiece.Name : "None";

            return String.Format("Gladiator Type: {0}\nChest: {1}\nHead: {2}", Name, Armor[BodyPart.Chest].Name, head );
        }
    }
}