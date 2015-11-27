using System;
using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{    
    public class Doctore : IGladiator
    {
        // There is no difference between having this property reference a field than having this just be a property with a getter/setter like this. Even resharper was like WTF
        /*
            public Attributes Attributes
            {
                get { return _attributes; }
                set { _attributes = value; }
            }

            public Attributes Attributes {get;set;}
        */

        public Attributes Attributes { get; set; }
        public IWeapon LeftHand { get; set; }
        public IWeapon RightHand { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }

        public List<IItems> Inventory { get; set; }

        public string Name { get; set; }

        // I think we need to consider putting HP in attributes
        public double HitPoints { get; set; }

        // I think we need to consider putting Energy in attributes
        public int Energy { get; set; }

        public void BattleCry()
        {
            Console.WriteLine("From the Darkness!");
        }

        public double SpecialAttack()
        {
            if (new Random().Next(0, 100) < Attributes.CritChance)
                return RightHand.DamageOutput() * 2;
            return RightHand.DamageOutput();
        }

        //Dependency injection for testing, I assume
        public Doctore(Attributes attributes, IWeapon rightHand, IWeapon leftHand, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, string name, double hitPoints, int energy)
        {
            this.Attributes = attributes;
            this.RightHand = rightHand;
            this.LeftHand = leftHand;
            this.Armor = armor;
            this.Inventory = inventory;
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Energy = energy;
        }

        public Doctore()
        {
            this.Attributes = new Attributes()
            {
                Strength = 25,
                Agility = 50,
                CritChance = 25,
                Vitality = 100
            };

            this.RightHand = new Mace();
            // this.LeftHand = <SOME THING??>

            this.Armor = new Dictionary<BodyPart, IArmor>();
            this.Inventory = new List<IItems>();
            this.Name = "Doctore";
            this.HitPoints = 100;
            this.Energy = 100;
        }
    }
}