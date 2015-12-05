using System;
using System.Collections.Generic;
using System.Linq;
using RandomRPG.Controllers;
using RandomRPG.Model.ArmorMitigation;
using RandomRPG.Model.Attacks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Factories;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Units
{
    public class Gladiator : IGladiator
    {
        //possibly consider class having a set of attack abilities acquired etc... 
        //Need to start handling actions in players suck as equip, unequip, view inventory, leveling up, dodge etc..
        //Add armor into the mix with dmg mitigation
        //Think about making some of this private when we decide what we dont want available
        //Think about how to validate equipping in correct slots
        public event EventHandler<EventArgs> DeathEvent;
        public int MaxEnergyValue { get; set; }
        public List<IAttribute> Attributes { get; set; } 
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
        public IUnit Target { get; set; }

        private IGladiator TargetGladiator => Target as IGladiator;
        public IZone CurrentZone { get; set; }
        public ITile CurrentTile { get; set; }
        public Reputation Reputation { get; set; }
        public Action<IGladiator> InteractionTriggered { get; set; }
        //prob need an alive flag
        //change to add event on death notify observers

        public IAttribute GetAttribute(AttributeType type)
        {
            return Attributes.FirstOrDefault(x => x.Type == type);
        }

        public void SetTargetGladiator(IGladiator gladiator)
        {
            Target = gladiator;
            gladiator.Target = this;
            TargetGladiator.DeathEvent += DeathEventHandler;
            this.DeathEvent += DeathEventHandler;
        }

        private void DeathEventHandler(object o, EventArgs e)
        {
            TargetGladiator.IsAlive = false;
            this.RestoreMaxEnergy();
            Text.ColorWriteLine("Dead! " + Target.Name + " has been slained by " + ((IGladiator)o).Name + "!", ConsoleColor.White);
            TargetGladiator.Target = null;
            Kills ++;
            TargetGladiator.DeathEvent -= DeathEventHandler;
            this.DeathEvent -= DeathEventHandler;
            Target = null;
        }

        public void DisplayAbilityOptions()
        {
            for (int i = 0; i < AbilityList.Count; i++)
            {
                Text.ColorWriteLine(i+1 + ") " + AbilityList[i].AbilityName + "(" + AbilityList[i].EnergyCost + ")", ConsoleColor.Magenta);
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
                    int mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(TargetGladiator.Armor, TargetGladiator.Type, Target.Attributes);
                    int netDmg = baseAttackDmg - mitigatedTargetBase;
                    LastDefensiveAbility = AbilityList[command];
                    this.DmgMitigated = ArmorMitigationLogic.DefendActionHandler(Armor, Type, Attributes, AbilityList[command].AbilityName);
                    if (netDmg > 0)
                    {
                        var hp = Target.Attributes.First(x => x.Type == AttributeType.HitPoints);
                        hp.Value -= netDmg;

                        if (hp.Value <= 0)
                        {
                             Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with your base attack" + "!!", ConsoleColor.Yellow);
                             DeathEventHandler(this, EventArgs.Empty);
                             return netDmg;
                        }
                        if (TargetGladiator.LastDefensiveAbility == null)
                        {
                            Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with your base attack" + "!!",
                            ConsoleColor.Yellow);
                            Text.ColorWriteLine(
                                "You attempt to " + AbilityList[command].AbilityName + " the next attack!",
                                ConsoleColor.Yellow);
                        }
                        else
                        {
                            Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " + Target.Name + " used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Yellow);
                            Text.ColorWriteLine(
                                "You attempt to " + AbilityList[command].AbilityName + " the next attack!",
                                ConsoleColor.Yellow);
                        } 
                        return netDmg;
                    }
                    
                    //no damage
                    Text.ColorWriteLine("You miss your attack on " + Target.Name + "! You do not have enough energy!", ConsoleColor.Yellow);
                    return netDmg;
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
            mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(TargetGladiator.Armor, TargetGladiator.Type, Target.Attributes);
            netDmg = grossDmg - mitigatedTargetBase;
            if (netDmg > 0)
            {
                var hp = Target.Attributes.First(x => x.Type == AttributeType.HitPoints);
                hp.Value -= netDmg;
                
                if (hp.Value <= 0)
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!!",
                    ConsoleColor.Yellow);
                    DeathEventHandler(this, EventArgs.Empty);
                    return netDmg;
                };

                if (TargetGladiator.LastDefensiveAbility == null)
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!!",
                    ConsoleColor.Yellow);
                }
                else
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " + Target.Name + " used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Yellow);
                }
                return netDmg;
            }

              Text.ColorWriteLine("You miss your attack on " + Target.Name + "! You do not have enough energy!", ConsoleColor.Yellow);  
            
            return netDmg;
        }

        private int HandleOffensiveAbilityNPC(int command)
            {
            int grossDmg;
            int mitigatedTargetBase;
            int netDmg;
            grossDmg = AttackLogic.AttackActionHandler(AbilityList[command].AbilityName, WeaponSet, Type, Attributes);
            //base dmg mitigation of target
            mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(TargetGladiator.Armor, TargetGladiator.Type,
                Target.Attributes);
            netDmg = grossDmg - mitigatedTargetBase - TargetGladiator.DmgMitigated;
            if (netDmg > 0)
            {
                var hp = TargetGladiator.GetAttribute(AttributeType.HitPoints);
                hp.Value -= netDmg;
                if (hp.Value <= 0)
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
                    Text.ColorWriteLine("YOU HAVE BEEN SLAIN!", ConsoleColor.Red);
                    DeathEventHandler(this, EventArgs.Empty);
                    Program.GameState = GameState.GameOver;
                    return netDmg;
                }
                if (TargetGladiator.LastDefensiveAbility == null)
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
                }
                else
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "! You used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Red);
                }
                return netDmg;
            }

            Text.ColorWriteLine(Name + " has missed their attack on you! " + Name + " is low on energy!", ConsoleColor.Red);

            return netDmg;
        }

        public int NpcAttack()
        {
            this.RegenerateEnergy();
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
                    int mitigatedTargetBase = ArmorMitigationLogic.GetBaseDmgMitigation(TargetGladiator.Armor, TargetGladiator.Type, Target.Attributes);
                     int netDmg = baseAttackDmg - mitigatedTargetBase - TargetGladiator.DmgMitigated;
                    LastDefensiveAbility = AbilityList[ability];
                    this.DmgMitigated = ArmorMitigationLogic.DefendActionHandler(Armor, Type, Attributes, AbilityList[ability].AbilityName);
                    if (netDmg > 0)
                    {
                        var hp = TargetGladiator.GetAttribute(AttributeType.HitPoints);
                        hp.Value -= netDmg;

                        if (hp.Value <= 0)
                        {
                            Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "!", ConsoleColor.Red);
                            Text.ColorWriteLine("YOU HAVE BEEN SLAIN!", ConsoleColor.Red);
                             DeathEventHandler(this, EventArgs.Empty);
                             return netDmg;
                        }
                        if (TargetGladiator.LastDefensiveAbility == null)
                        {
                            Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "!", ConsoleColor.Red);
                            Text.ColorWriteLine(Name + " will attempt to " +  AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                        }
                        else
                        {
                            Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "! You used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Red);
                            Text.ColorWriteLine(Name + " will attempt to " +  AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                        }
                        return netDmg;
                    }
                    //no damage
                     Text.ColorWriteLine(Name + " has missed their attack on you! " + Name + " is low on energy!", ConsoleColor.Red);
                    return netDmg;
                 }
            }
            //move to resource possibly
            Text.ColorWriteLine("No Target!", ConsoleColor.Red);
            return 1;

        }


        public Gladiator(List<IAttribute> attributes, Dictionary<BodyPart, IWeapon> weaponSet, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, GladiatorTypes gladType)
        {
            this.Attributes = attributes;
            this.WeaponSet = weaponSet;
            this.Armor = armor;
            this.Inventory = inventory;
            this.Name = NameGenerator.GenerateRandom();
            this.Type = gladType;
            this.IsAlive = true;
            this.DmgMitigated = 0;
            this.Kills = 0;
            this.Reputation = Reputation.Hostile;
            InteractionTriggered += OnInteractionTriggered;
        }

        public Gladiator(string name, GladiatorTypes gladType)
        {
            this.Attributes = AttributeFactory.GetInstance(gladType.ToString());
            this.WeaponSet = WeaponFactory.GetBaseWeaponInstances(gladType);
            this.Armor = ArmorFactory.GetBaseArmorInstances(gladType);
            this.AbilityList = AbilityFactory.GetBaseAbilityList(gladType);
            this.Type = gladType;
            this.Inventory = new List<IItems>();
            this.Name = name.ToUpper();
            this.IsAlive = true;
            this.DmgMitigated = 0;
            this.Kills = 0;
            this.MaxEnergyValue = this.GetAttribute(AttributeType.Energy).Value;
            this.Reputation = Reputation.Hostile;
            InteractionTriggered += OnInteractionTriggered;
        }

        private void OnInteractionTriggered(IGladiator unit)
        {
            InteractionController.Interact(unit, this);
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