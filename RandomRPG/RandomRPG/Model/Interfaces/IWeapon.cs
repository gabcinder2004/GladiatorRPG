using System.Collections.Generic;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IWeapon : IEquippable<WeaponTypes>
    {
        int DamageOutput();
        int MinDamage { get; set; }
        int MaxDamage { get; set; }
        List<BodyPart> EquipLocations { get; set; }
    }
}