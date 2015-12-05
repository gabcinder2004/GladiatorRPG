using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IOffensiveAbilities : IAbilities
    {
        int Execute(Dictionary<BodyPart, IWeapon> itemSet, List<IAttribute> attributes);
    }
}
