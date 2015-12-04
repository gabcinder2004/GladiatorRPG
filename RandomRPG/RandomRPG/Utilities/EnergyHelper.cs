using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Utilities
{
    public static class EnergyHelperExtension
    {
        public static void RegenerateEnergy(this IGladiator glad)
        {
            int currentEnergy = glad.GetAttribute(AttributeType.Energy).Value;
            if(currentEnergy == glad.MaxEnergyValue)
                return;

            int energyRegenPerTurn = Convert.ToInt32(glad.GetAttribute(AttributeType.Vitality).Value*.10);

            if (currentEnergy + energyRegenPerTurn >= glad.MaxEnergyValue)
            {
                glad.GetAttribute(AttributeType.Energy).Value = glad.MaxEnergyValue;
            }
            else
            {
                glad.GetAttribute(AttributeType.Energy).Value += energyRegenPerTurn;
            }      
        }

        public static void RestoreMaxEnergy(this IGladiator glad)
        {
            glad.GetAttribute(AttributeType.Energy).Value = glad.MaxEnergyValue;
        }
    }
}
