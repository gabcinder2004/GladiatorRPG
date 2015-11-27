namespace RandomRPG.Model
{
    public interface IArmor : IEquippable
    {
        BodyPart EquipLocation { get; }
    }
}