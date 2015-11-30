using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG.Model.Interfaces
{
    public interface IAbilities
    {
        int Execute();
        string AbilityName { get; set; }
        string AbilityType { get; set; }
        int EnergyCost { get; set; }
    }
}
