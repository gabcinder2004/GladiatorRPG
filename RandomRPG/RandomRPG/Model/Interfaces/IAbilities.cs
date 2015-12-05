using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IAbilities
    {
        //will need to change this Execute method
        int Execute(Dictionary<BodyPart, IWeapon> weaponSet, List<IAttribute> attributes);
        string AbilityName { get; set; }
        string AbilityType { get; set; }
        int EnergyCost { get; set; }
    }
}
