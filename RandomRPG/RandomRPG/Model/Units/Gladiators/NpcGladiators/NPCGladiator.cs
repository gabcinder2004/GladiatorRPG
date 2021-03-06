﻿using System;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Units
{
    public abstract class NpcGladiator : Gladiator
    {
        public NpcGladiator(string name, GladiatorTypes gladType)
            : base(name, gladType)
        {         
        }

        //Keep this here for now in case we want to feed specific attack commands to npc. Then we can just implement this method to use base
        public override int Attack(int command)
        {
            //base.Attack();
            return Attack();
        }

        public int Attack()
        {
            Random rand = new Random();
            int ability = rand.Next(0, AbilityList.Count);

            if (Target != null)
            {
                if (AbilityList[ability] is IOffensiveAbilities)
                {
                    LastDefensiveAbility = null;
                    DmgMitigated = 0;
                    return HandleOffensiveAbility(ability);
                }
                else
                {
                    //hit with base Attack
                    int baseAttackDmg = this.GetBaseAttackDmg(this.WeaponSet, this.Attributes);
                    int mitigatedTargetBase = TargetGladiator.GetBaseDmgMitigation(TargetGladiator.Armor, Target.Attributes);
                    int netDmg = baseAttackDmg - mitigatedTargetBase - TargetGladiator.DmgMitigated;
                    LastDefensiveAbility = AbilityList[ability];
                    this.DmgMitigated = ((IDefensiveAbilities)AbilityList[ability]).Execute(this.Armor, this.Attributes);
                    this.RegenerateEnergy();
                    if (netDmg > 0)
                    {
                        var hp = TargetGladiator.GetAttribute(AttributeType.HitPoints);
                        hp.Value -= netDmg;
                        Text.ClearWithAbilities();
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
                            Text.ColorWriteLine(Name + " will attempt to " + AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                        }
                        else
                        {
                            Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[ability].AbilityName + "! You used " + 
                                TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Red);
                            Text.ColorWriteLine(Name + " will attempt to " + AbilityList[ability].AbilityName + " the next attack!", ConsoleColor.Red);
                        }
                        return netDmg;
                    }
                    //no damage
                    Text.ColorWriteLine(Name + " has missed their attack on you!", ConsoleColor.Red);
                    return netDmg;
                }
            }
            //move to resource possibly
            Text.ColorWriteLine("No Target!", ConsoleColor.Red);
            return 1;
        }

        protected override int HandleOffensiveAbility(int command)
        {
            int grossDmg;
            int mitigatedTargetBase;
            int netDmg;
            grossDmg = ((IOffensiveAbilities)AbilityList[command]).Execute(this.WeaponSet, this.Attributes);
            this.RegenerateEnergy();
            //base dmg mitigation of target
            mitigatedTargetBase = TargetGladiator.GetBaseDmgMitigation(TargetGladiator.Armor, Target.Attributes);
            netDmg = grossDmg - mitigatedTargetBase - TargetGladiator.DmgMitigated;
            if (netDmg > 0)
            {
                var hp = TargetGladiator.GetAttribute(AttributeType.HitPoints);
                hp.Value -= netDmg;
                Text.ClearWithAbilities();
                if (hp.Value <= 0)
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
                    Text.ColorWriteLine("YOU HAVE BEEN SLAIN!", ConsoleColor.Red);
                    DeathEventHandler(this, EventArgs.Empty);
                    return netDmg;
                }
                if (TargetGladiator.LastDefensiveAbility == null)
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "!", ConsoleColor.Red);
                }
                else
                {
                    Text.ColorWriteLine(this.Name + " has attacked you for " + netDmg + " damage with " + AbilityList[command].AbilityName + "! You used " 
                        + TargetGladiator.LastDefensiveAbility.AbilityName + " to mitigate " + TargetGladiator.DmgMitigated + " damage!", ConsoleColor.Red);
                }
                return netDmg;
            }

            Text.ColorWriteLine(Name + " has missed their attack on you!", ConsoleColor.Red);

            return netDmg;
        }
    }
}
