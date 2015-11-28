using System.Collections.Generic;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Zones;

namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// Represents what will make a gladiator
    /// </summary>
    public interface IGladiator : IUnit<GladiatorTypes>
    {
        Dictionary<BodyPart, IWeapon> WeaponSet { get; set; }
        Dictionary<BodyPart, IArmor> Armor { get; set; }
        List<IItems> Inventory { get; set; }
        int Attack(string command);
        GladiatorTypes Type { get; set; }
        IGladiator Target { get; set; }

        IZone CurrentZone { get; set; }
    }
}