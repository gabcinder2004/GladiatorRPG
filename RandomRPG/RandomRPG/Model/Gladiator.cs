using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RandomRPG.Model
{
    public interface IGladiator
    {
        Dictionary<BodyPart, IItems> GearSlots { get; set; }
        List<IItems> Inventory { get; set; }
        Attributes Attributes { get; set; }
        string Name { get; set; }
        double HitPoints { get; set; }
        int Energy { get; set; }
        void BattleCry();
    }

    //Remove unit since even npcs should implement IGladiator

    public class Doctore : IGladiator
    {
        private Dictionary<BodyPart, IItems> gearSlots;

        private Attributes attributes;

        private List<IItems> inventory;

        private string name;

        private double hitPoints;

        private int energy;

        public Attributes Attributes
        {
            get { return attributes; } 
            set { attributes = value; }
        }

        public Dictionary<BodyPart, IItems> GearSlots 
        {
            get { return gearSlots; }
            set { gearSlots = value; }
        }

        public List<IItems> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public string Name
        { 
            get { return name; } 
            set { name = value; }
        }

        public double HitPoints
        {
            get { return hitPoints; }
            set { hitPoints = value; }
        }

        public int Energy
        {
            get { return energy; }
            set { energy = value; }
        }

        public void BattleCry()
        {
            Console.WriteLine("From the Darkness!");
        }

        public double SpecialAttack()
        {
            IItems weapon;
            GearSlots.TryGetValue(BodyPart.MainHand, out weapon);
            if (new Random().Next(0, 100) < Attributes.CritChance)
                return weapon.DmgOutput()*2;
            else
                return weapon.DmgOutput();
        }

        public Doctore(Attributes attributes, Dictionary<BodyPart, IItems> gearSlots, List<IItems> inventory, string name, double hitPoints, int energy)
        {
            this.Attributes = attributes;
            this.GearSlots = gearSlots;
            this.Inventory = inventory;
            this.Name = name;
            this.HitPoints = hitPoints;
            this.Energy = energy;
        }

        public Doctore()
        {
            this.Attributes = new Attributes()
            {
                Strength = 25, Agility = 50, CritChance = 25, Vitality = 100
            };

            this.GearSlots = new Dictionary<BodyPart, IItems>
            {
                {BodyPart.MainHand, new Mace()}
            };

            this.Inventory = new List<IItems>();

            this.Name = "Doctore";

            this.HitPoints = 100;

            this.Energy = 100;
        }
    }
}