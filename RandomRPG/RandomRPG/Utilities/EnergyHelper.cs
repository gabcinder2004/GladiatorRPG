using System;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;

namespace RandomRPG.Utilities
{
    public static class EnergyHelperExtension
    {
        public static void RegenerateEnergy(this Gladiator glad)
        {
            int currentEnergy = glad.GetAttribute(AttributeType.Energy).Value;
            if(currentEnergy == glad.MaxEnergyValue)
                return;

            int energyRegenPerTurn = Convert.ToInt32(glad.GetAttribute(AttributeType.Vitality).Value*.05);

            if (currentEnergy + energyRegenPerTurn >= glad.MaxEnergyValue)
            {
                glad.GetAttribute(AttributeType.Energy).Value = glad.MaxEnergyValue;
            }
            else
            {
                glad.GetAttribute(AttributeType.Energy).Value += energyRegenPerTurn;
            }      
        }

        public static void RestoreMaxEnergy(this Gladiator glad)
        {
            glad.GetAttribute(AttributeType.Energy).Value = glad.MaxEnergyValue;
        }
    }
}
