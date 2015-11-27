using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IArmor : IEquippable
    {
        BodyPart EquipLocation { get; }
    }
}