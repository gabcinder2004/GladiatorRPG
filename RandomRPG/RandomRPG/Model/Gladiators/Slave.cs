using System.Collections.Generic;
using RandomRPG.Model.Armor;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Gladiators
{
    class Slave : IGladiator
    {
        public Slave(string name)
        {
            Name = name;
            Attributes = new Attributes()
            {
                Agility = 20,
                CritChance = 0,
                Energy = 100,
                HitPoints = 100,
                Strength = 10,
                Vitality = 10
            };
            Armor = new Dictionary<BodyPart, IArmor>()
               {
                   {BodyPart.Pants, new Underwear() }
               };
        }

        public string Name { get; set; }
        public Attributes Attributes { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public int Attack(string command)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<BodyPart, IWeapon> WeaponSet
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public GladiatorTypes type
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
