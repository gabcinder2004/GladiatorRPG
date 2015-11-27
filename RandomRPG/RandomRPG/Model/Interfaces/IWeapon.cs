namespace RandomRPG.Model.Interfaces
{
    public interface IWeapon : IEquippable
    {
        int DamageOutput();
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
    }
}