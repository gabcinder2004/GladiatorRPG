using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Zones;

namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// Represents what will make a gladiator
    /// </summary>
    public interface IGladiator : IUnit
    {
        Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        Dictionary<BodyPart, IArmor> Armor { get; set; }
        List<IItems> Inventory { get; set; }
        int Attack(int command);
        IGladiator Target { get; set; }
        int NpcAttack();
        IZone CurrentZone { get; set; }
        event EventHandler<EventArgs> DeathEvent;
        void SetTargetGladiator(IGladiator gladiator);
        int MaxEnergyValue { get; set; }
        IAttribute GetAttribute(AttributeType type);
        List<IAbilities> AbilityList { get; set; }
        void DisplayAbilityOptions();
        bool IsAlive { get; set; }
        IAbilities LastDefensiveAbility { get; set; }
        int DmgMitigated { get; set; }
        int Kills { get; set; }
        GladiatorTypes Type { get; set; }
    }
}