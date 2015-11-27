using System.Collections.Generic;

namespace RandomRPG.Model.Interfaces
{
    public interface IGladiator : IEntity
    {
        IWeapon LeftHand { get; set; }
        IWeapon RightHand { get; set; }

        Dictionary<BodyPart, IArmor> Armor { get; set; }
        //Dictionary<BodyPart, IItems> GearSlots { get; set; }

        List<IItems> Inventory { get; set; }
        Attributes Attributes { get; set; }
        int Energy { get; set; }
        void BattleCry();
    }
}