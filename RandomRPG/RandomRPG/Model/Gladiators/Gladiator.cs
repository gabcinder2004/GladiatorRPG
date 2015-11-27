using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model
{    
    public class Gladiator : IGladiator
    {
        //possibly consider class having a set of attack abilities acquired etc... 
        //Need to start handling actions in players suck as equip, unequip, view inventory, leveling up, dodge etc..
        //Add armor into the mix with dmg mitigation
        public Attributes Attributes { get; set; }
        public Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public string Name { get; set; }
        public GladiatorTypes type { get; set; }
        
        public int Attack(string command)
        {
            return AttackLogic.AttackActionHandler(command, WeaponSet, type, Attributes);
        }

        public Gladiator(Attributes attributes, Dictionary<BodyPart, IWeapon> weaponSet, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, GladiatorTypes gladType)
        {
            this.Attributes = attributes;
            this.WeaponSet = weaponSet;
            this.Armor = armor;
            this.Inventory = inventory;
            this.Name = gladType.ToString();
            this.type = gladType;
        }

        public Gladiator(GladiatorTypes gladType)
        {
            this.Attributes = AttributeFactory.GetInstance(gladType);

            this.WeaponSet = WeaponFactory.GetBaseWeaponInstances(gladType);

            this.Armor = ArmorFactory.GetBaseArmorInstances(gladType);

            this.type = gladType;
            this.Inventory = new List<IItems>();
            this.Name = gladType.ToString();
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