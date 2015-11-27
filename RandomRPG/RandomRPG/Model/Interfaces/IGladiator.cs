using System.Collections.Generic;
using RandomRPG.Model.Enums;

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
        int Attack(string command);
        GladiatorTypes type { get; set; }
    }
}