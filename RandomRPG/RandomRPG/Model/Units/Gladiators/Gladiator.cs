using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Controllers;
using RandomRPG.Model.ArmorMitigation;
using RandomRPG.Model.Attacks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Factories;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Units
{
    public abstract class Gladiator : IUnit
    {
        public event EventHandler<EventArgs> DeathEvent;
        public int MaxEnergyValue { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        public Dictionary<BodyPart, IArmor> Armor { get; set; }
        public List<IItems> Inventory { get; set; }
        public string Name { get; set; }
        public int Kills { get; set; }
        public List<IAbilities> AbilityList { get; set; }
        public bool IsAlive { get; set; }
        public IAbilities LastDefensiveAbility { get; set; }
        public int DmgMitigated { get; set; }
        public IZone CurrentZone { get; set; }
        public ITile CurrentTile { get; set; }
        public Reputation Reputation { get; set; }
        public Action<Gladiator> InteractionTriggered { get; set; }
        public IUnit Target { get; set; }

        public Gladiator TargetGladiator => Target as Gladiator;

        public abstract int GetBaseAttackDmg(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes);
        public abstract int GetBaseDmgMitigation(Dictionary<BodyPart, IArmor> armorSet, List<IAttribute> attributes);

         public IAttribute GetAttribute(AttributeType type)
        {
            return Attributes.FirstOrDefault(x => x.Type == type);
        }

        public void SetTargetGladiator(Gladiator gladiator)
        {
            Target = gladiator;
            gladiator.Target = this;
            TargetGladiator.DeathEvent += DeathEventHandler;
            this.DeathEvent += DeathEventHandler;
        }

        protected void DeathEventHandler(object o, EventArgs e)
        {
            TargetGladiator.IsAlive = false;
            this.RestoreMaxEnergy();
            Text.ColorWriteLine("Dead! " + Target.Name + " has been slained by " + ((Gladiator)o).Name + "!", ConsoleColor.White);
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

        public virtual int Attack(int command)
        {
            if (Target != null)
            {
                if (AbilityList[command] is IOffensiveAbilities)
                {
                    LastDefensiveAbility = null;
                    DmgMitigated = 0;
                    return HandleOffensiveAbility(command);
                }
                else
                {
                    //hit with base Attack
                    int baseAttackDmg = this.GetBaseAttackDmg(this.WeaponSet, this.Attributes);
                    int mitigatedTargetBase = TargetGladiator.GetBaseDmgMitigation(TargetGladiator.Armor, Target.Attributes);
                    int netDmg = baseAttackDmg - mitigatedTargetBase;
                    LastDefensiveAbility = AbilityList[command];
                    this.DmgMitigated = ((IDefensiveAbilities)AbilityList[command]).Execute(Armor, Attributes);
                    this.RegenerateEnergy();
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
                            Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " 
                                + Target.Name + " used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Yellow);
                            Text.ColorWriteLine(
                                "You attempt to " + AbilityList[command].AbilityName + " the next attack!",
                                ConsoleColor.Yellow);
                        } 
                        return netDmg;
                    }
                    
                    //no damage
                    Text.ColorWriteLine("You miss your attack on " + Target.Name + "!", ConsoleColor.Yellow);
                    return netDmg;
                }
            }
            Text.ColorWriteLine("No Target!", ConsoleColor.Yellow);
            return 1;
        }

        protected virtual int HandleOffensiveAbility(int command)
        {
            int grossDmg;
            int mitigatedTargetBase;
            int netDmg;
            grossDmg = ((IOffensiveAbilities)AbilityList[command]).Execute(this.WeaponSet, this.Attributes);
            this.RegenerateEnergy();

            //base dmg mitigation of target
            mitigatedTargetBase = TargetGladiator.GetBaseDmgMitigation(TargetGladiator.Armor, Target.Attributes);
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
                    Program.GameState = GameState.GameOver;
                    return netDmg;
                };

                if (TargetGladiator.LastDefensiveAbility == null)
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!!",
                    ConsoleColor.Yellow);
                }
                else
                {
                    Text.ColorWriteLine("You have hit " + Target.Name + " for " + netDmg + " with " + AbilityList[command].AbilityName + "!! " 
                        + Target.Name + " used " + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Yellow);
                }
                return netDmg;
            }

              Text.ColorWriteLine("You miss your attack on " + Target.Name + "!", ConsoleColor.Yellow);  
            
            return netDmg;
        }
     
        //Create a factory test object to pass in here and set values then create a constructor which takes that factory as a parameter
        //public Gladiater(List<IAttribute> attributes, Dictionary<BodyPart, IWeapon> weaponSet, Dictionary<BodyPart, IArmor> armor, List<IItems> inventory, GladiatorTypes gladType)
        //{
        //    this.Attributes = attributes;
        //    this.WeaponSet = weaponSet;
        //    this.Armor = armor;
        //    this.Inventory = inventory;
        //    this.Name = gladType.ToString();
        //    this.IsAlive = true;
        //    this.DmgMitigated = 0;
        //    this.Kills = 0;
        //    this.Reputation = Reputation.Hostile;
        //    InteractionTriggered += OnInteractionTriggered;
        //}

        public Gladiator(string name, GladiatorTypes gladType)
        {
            this.Attributes = AttributeFactory.GetInstance(gladType.ToString());
            this.WeaponSet = WeaponFactory.GetBaseWeaponInstances(gladType);
            this.Armor = ArmorFactory.GetBaseArmorInstances(gladType);
            this.AbilityList = AbilityFactory.GetBaseAbilityList(gladType);
            this.Inventory = new List<IItems>();
            this.Name = name.ToUpper();
            this.IsAlive = true;
            this.DmgMitigated = 0;
            this.Kills = 0;
            this.MaxEnergyValue = this.GetAttribute(AttributeType.Energy).Value;
            this.Reputation = Reputation.Hostile;
            InteractionTriggered += OnInteractionTriggered;
        }

        private void OnInteractionTriggered(Gladiator unit)
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
