using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IArmor : IEquippable<ArmorTypes>
    {
        BodyPart EquipLocation { get; set; }
        int ArmorValue { get; set; }
    }
}