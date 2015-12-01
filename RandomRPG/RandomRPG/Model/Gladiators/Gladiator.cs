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
        public int Kills { get; set; }
        public List<IAbilities> AbilityList { get; set; }
        public bool IsAlive { get; set; }
        public IAbilities LastDefensiveAbility { get; set; }
        public int DmgMitigated { get; set; }
 
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
            Target.Target = null;
            Kills ++;
            Target.DeathEvent -= DeathEventHandler;
            this.DeathEvent -= DeathEventHandler;
            Target = null;
        }

        public void DisplayAbilityOptions()
        {
            for (int i = 0; i < AbilityList.Count; i++)
            {
                Text.ColorWriteLine(i+1 + ") " + AbilityList[i].AbilityName, ConsoleColor.Magenta);
            }
        }

        public int Attack(int command)
        {
            if (Target != null)
            {
                if (AbilityList[command].AbilityType == "Offensive")
                {
                    LastDefensiveAbility = null;
                    DmgMitigated = 0;
                    return HandleOffensiveAbility(command);
                }
                else
                {
                    //hit with base Attack
                    int baseAttackDmg = AttackLogic.GetBaseAttackDmg(WeaponSet, Type, Attributes);
                    int mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(Target.Armor, Target.Type, Target.Attributes);
                    int netDmg = baseAttackDmg - mitigatedTargetBase;
                    LastDefensiveAbility = AbilityList[command];
                    this.DmgMitigated = ArmorMitigationLogic.DefendActionHandler(Armor, Type, Attributes, AbilityList[command].AbilityName);
                    if (netDmg > 0)
                    {
                        Target.Attributes.HitPoints -= netDmg;

                        if (Target.Attributes.HitPoints <= 0)
                        {
                             Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with your base attack" + "!!", ConsoleColor.Yellow);
                             DeathEventHandler(this, EventArgs.Empty);
                             return netDmg;
                        }
                    }
                    if (Target.LastDefensiveAbility == null)
                        {
                            Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with your base attack" + "!!",
                        ConsoleColor.Yellow);
                            Text.ColorWriteLine(
                                "You attempt to " + AbilityList[command].AbilityName + " the next attack!",
                                ConsoleColor.Yellow);
                        }
                    else
                        {
                            Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " + Target.Name + " used " + Target.LastDefensiveAbility.AbilityName + " to mitigate " + Target.DmgMitigated + " damage!", ConsoleColor.Yellow);
                            Text.ColorWriteLine(
                                "You attempt to " + AbilityList[command].AbilityName + " the next attack!",
                                ConsoleColor.Yellow);
                        }
                    //no damage
                    return 0;
                }
            }
            Text.ColorWriteLine("No Target!", ConsoleColor.Yellow);
            return 1;
        }

        private int HandleOffensiveAbility(int command)
        {
            int grossDmg;
            int mitigatedTargetBase;
            int netDmg;
            grossDmg = AttackLogic.AttackActionHandler(AbilityList[command].AbilityName, WeaponSet, Type, Attributes);
            //base dmg mitigation of target
            mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(Target.Armor, Target.Type,
                Target.Attributes);
            netDmg = grossDmg - mitigatedTargetBase;
            if (netDmg > 0)
            {
                Target.Attributes.HitPoints -= netDmg;
                
                if (Target.Attributes.HitPoints <= 0)
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!!",
                    ConsoleColor.Yellow);
                    DeathEventHandler(this, EventArgs.Empty);
                    return netDmg;
                };
            }
            if (Target.LastDefensiveAbility == null)
            {
               Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!!",
                    ConsoleColor.Yellow);
            }
            else
            {
               Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " + Target.Name + " used " + Target.LastDefensiveAbility.AbilityName + " to mitigate " + Target.DmgMitigated + " damage!", ConsoleColor.Yellow);
            }
            return netDmg;
        }

        private int HandleOffensiveAbilityNPC(int command)
            {
            int grossDmg;
            int mitigatedTargetBase;
            int netDmg;
            grossDmg = AttackLogic.AttackActionHandler(AbilityList[command].AbilityName, WeaponSet, Type, Attributes);
            //base dmg mitigation of target
            mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(Target.Armor, Target.Type,
                Target.Attributes);
            netDmg = grossDmg - mitigatedTargetBase - Target.DmgMitigated;
            if (netDmg > 0)
            {
                Target.Attributes.HitPoints -= netDmg;
                DisplayHPValues();
                if (Target.Attributes.HitPoints <= 0)
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
                    Text.ColorWriteLine("YOU HAVE BEEN SLAIN!", ConsoleColor.Red);
                    DeathEventHandler(this, EventArgs.Empty);
                    return netDmg;
                }
            }
            if (Target.LastDefensiveAbility == null)
            {
               Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
            }
            else
            {
               Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "! You used " + Target.LastDefensiveAbility.AbilityName + " to mitigate " + Target.DmgMitigated + " damage!", ConsoleColor.Red);
            }
            return netDmg;
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
            string abilityType = AbilityList[ability].AbilityType;
             if (Target != null)
            {
                 if (abilityType == "Offensive")
                {
                    LastDefensiveAbility = null;
                    DmgMitigated = 0;
                    return HandleOffensiveAbilityNPC(ability);
                }
                 else
                 {
                     //hit with base Attack
                    int baseAttackDmg = AttackLogic.GetBaseAttackDmg(WeaponSet, Type, Attributes);
                    int mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(Target.Armor, Target.Type, Target.Attributes);
                     int netDmg = baseAttackDmg - mitigatedTargetBase - Target.DmgMitigated;
                    LastDefensiveAbility = AbilityList[ability];
                    this.DmgMitigated = ArmorMitigationLogic.DefendActionHandler(Armor, Type, Attributes, AbilityList[ability].AbilityName);
                    if (netDmg > 0)
                    {
                        Target.Attributes.HitPoints -= netDmg;
                        DisplayHPValues();

                        if (Target.Attributes.HitPoints <= 0)
                        {
                            Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "!", ConsoleColor.Red);
                            Text.ColorWriteLine("YOU HAVE BEEN SLAIN!", ConsoleColor.Red);
                             DeathEventHandler(this, EventArgs.Empty);
                             return netDmg;
                        }
                    }
                    if (Target.LastDefensiveAbility == null)
                    {
                        Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "!", ConsoleColor.Red);
                        Text.ColorWriteLine(Name + " will attempt to " +  AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                    }
                    else
                    {
                        Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "! You used " + Target.LastDefensiveAbility.AbilityName + " to mitigate " + Target.DmgMitigated + " damage!", ConsoleColor.Red);
                        Text.ColorWriteLine(Name + " will attempt to " +  AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                    }
                    //no damage
                    return 0;
                 }
            }
            //move to resource possibly
            Text.ColorWriteLine("No Target!", ConsoleColor.Red);
            return 1;

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
            this.DmgMitigated = 0;
            this.Kills = 0;
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
            this.DmgMitigated = 0;
            this.Kills = 0;
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