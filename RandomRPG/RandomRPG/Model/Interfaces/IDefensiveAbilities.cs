using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IDefensiveAbilities : IAbilities
    {
        int Execute(Dictionary<BodyPart, IArmor> itemSet, List<IAttribute> attributes);
    }
}
