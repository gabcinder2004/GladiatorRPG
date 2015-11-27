using System.Collections.Generic;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// Represents what will make a gladiator
    /// </summary>
    public interface IGladiator : IUnit
    {
        IWeapon LeftHand { get; set; }
        IWeapon RightHand { get; set; }
        Dictionary<BodyPart, IArmor> Armor { get; set; }
        List<IItems> Inventory { get; set; }
        void BattleCry();
        int Attack(string command);
    }
}