using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RandomRPG.Model.ArmorMitigation;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Factories;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Zones;
using RandomRPG.Utilities;

namespace RandomRPG.Model
{    
    public class Gladiator : IGladiator
    {
        //possibly consider class having a set of attack abilities acquired etc... 
        //Need to start handling actions in players suck as equip, unequip, view inventory, leveling up, dodge etc..
        //Add armor into the mix with dmg mitigation
        //Think about making some of this private when we decide what we dont want available
        //Think about how to validate equipping in correct slots
        public event EventHandler<EventArgs> DeathEvent;
        public Attributes Attributes { get; set; }
        public Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public string Name { get; set; }
        public GladiatorTypes Type { get; set; }
        //Just storing this for now
        public int Kills = 0;
        public List<IAbilities> AbilityList { get; set; }
        public bool IsAlive { get; set; }
 
        public IZone CurrentZone { get; set; }
        public ITile CurrentTile { get; set; }
        //prob need an alive flag
        //change to add event on death notify observers

        public void SetTargetGladiator(IGladiator gladiator)
        {
            Target = gladiator;
            gladiator.Target = this;
            Target.DeathEvent += DeathEventHandler;
            this.DeathEvent += DeathEventHandler;
        }

        private void DeathEventHandler(object o, EventArgs e)
        {
            Target.IsAlive = false;
            Text.ColorWriteLine("Dead! " + Target.Name + " has been slained by " + ((IGladiator)o).Name + "!", ConsoleColor.White);
        }

        public void DisplayAbilityOptions()
        {
            for (int i = 0; i < AbilityList.Count; i++)
            {
                Text.ColorWriteLine(i+1 + ") " + AbilityList[i], ConsoleColor.Magenta);
            }
        }
        public int Attack(string command)
        {
            //Add some sort of Class for Armor Logic similar to attack, temp implementation, also need to add dodge etc.. evasion type classes.
            if (Target != null)
            {
                int grossDmg = AttackLogic.AttackActionHandler(command, WeaponSet, Type, Attributes);
                //If an active command line below will need to chanhe
                int mitigatedDmg = ArmorMitigationLogic.DefendActionHandler(Target.Armor, Target.Type, Target.Attributes, "block");
                int netDmg = grossDmg - mitigatedDmg;
                if (netDmg > 0)
                {
                    Target.Attributes.HitPoints -= netDmg;
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + command + "!!", ConsoleColor.Yellow);
                    //DisplayHPValues();
                    if (Target.Attributes.HitPoints <= 0)
                    {
                        //Move to resource possibly
                        DeathEventHandler(this, EventArgs.Empty);
                        Target.Target = null;
                        Target.DeathEvent -= DeathEventHandler;
                        this.DeathEvent -= DeathEventHandler;
                        Target = null;
                        Kills ++;
                    }
                    return netDmg;
                }
                return 0;
            }
            //move to resource possibly
            Console.WriteLine("No Target");

            return -1;
        }

        //Most liekly will move this out somewhere else, Gladiator should not be responsible for this.
        private void DisplayHPValues()
        {
            Text.ColorWriteLine(Target.Name + "- Hit Points: " + Target.Attributes.HitPoints, ConsoleColor.Green);
            Text.ColorWriteLine(Name + "- Hit Points: " + Attributes.HitPoints + "\n", ConsoleColor.Red);
        }

        public int NpcAttack()
        {
            Random rand = new Random();
            int ability = rand.Next(0, AbilityList.Count);
            string command = AbilityList[ability].ToString();
             if (Target != null)
            {
                int grossDmg = AttackLogic.AttackActionHandler(command, WeaponSet, Type, Attributes);
                //If an active command line below will need to chanhe
                int mitigatedDmg = ArmorMitigationLogic.DefendActionHandler(Target.Armor, Target.Type, Target.Attributes, "block");
                int netDmg = grossDmg - mitigatedDmg;
                if (netDmg > 0)
                {
                    Target.Attributes.HitPoints -= netDmg;
                    DisplayHPValues();
                    if (Target.Attributes.HitPoints <= 0)
                    {
                        //Move to resource possibly
                        DeathEventHandler(this, EventArgs.Empty);
                        Console.WriteLine(Target.Name + " Has been slain!");
                        Target.Target = null;
                        Target.DeathEvent -= DeathEventHandler;
                        this.DeathEvent -= DeathEventHandler;
                        Target = null;
                        Kills ++;
                        return netDmg;
                    }
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + command + "!", ConsoleColor.Red);
                    return netDmg;
                }
                return 0;
            }
            //move to resource possibly
            Console.WriteLine("No Target");
            return -1;

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
            this.IsAlive = true;
        }

        public Gladiator(string name, GladiatorTypes gladType)
        {
            this.Attributes = AttributeFactory.GetInstance(gladType);
            this.WeaponSet = WeaponFactory.GetBaseWeaponInstances(gladType);
            this.Armor = ArmorFactory.GetBaseArmorInstances(gladType);
            this.AbilityList = AbilityFactory.GetBaseAbilityList(gladType);
            this.Type = gladType;
            this.Inventory = new List<IItems>();
            this.Name = name.ToUpper();
            this.IsAlive = true;
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