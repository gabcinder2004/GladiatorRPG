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
        //Think about making some of this private when we decide what we dont want available
        public Attributes Attributes { get; set; }
        public Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public string Name { get; set; }
        public GladiatorTypes Type { get; set; }
        //Just storing this for now
        public int Kills = 0;
        //prob need an alive flag

        public int Attack(string command)
        {
            int grossDmg = AttackLogic.AttackActionHandler(command, WeaponSet, Type, Attributes);
            //Add some sort of Class for Armor Logic similar to attack, temp implementation, also need to add dodge etc.. evasion type classes.
            if (Target != null)
            {
                int netDmg = grossDmg - (Target.Armor[BodyPart.Chest].ArmorValue + ((int) (Attributes.Agility*.10)));
                if (netDmg > 0)
                {
                    Target.Attributes.HitPoints -= netDmg;
                    if (Target.Attributes.HitPoints <= 0)
                    {
                        //Move to resource possibly
                        Console.WriteLine(Target.Name + " Has been slain!");
                        Target.Target = null;
                        Target = null;
                        Kills ++;
                    }
                    return netDmg;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //move to resource possibly
                Console.WriteLine("No Target");
                return -1;
            }
        }

        public IGladiator Target { get; set; }

        public Gladiator(Attributes attributes, Dictionary<BodyPart, IWeapon> weaponSet, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, GladiatorTypes gladType)
        {
            this.Attributes = attributes;
            this.WeaponSet = weaponSet;
            this.Armor = armor;
            this.Inventory = inventory;
            this.Name = gladType.ToString();
            this.Type = gladType;
        }

        public Gladiator(GladiatorTypes gladType)
        {
            this.Attributes = AttributeFactory.GetInstance(gladType);

            this.WeaponSet = WeaponFactory.GetBaseWeaponInstances(gladType);

            this.Armor = ArmorFactory.GetBaseArmorInstances(gladType);

            this.Type = gladType;
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

            return $"Gladiator Type: {Name}\nChest: {Armor[BodyPart.Chest].Name}\nHead: {head}\nTotal Kills: {Kills}";
        }
    }
}