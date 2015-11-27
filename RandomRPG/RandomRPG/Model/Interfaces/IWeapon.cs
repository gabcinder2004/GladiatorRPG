namespace RandomRPG.Model.Interfaces
{
    public interface IWeapon : IEquippable
    {
        double DamageOutput();
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
    }
}