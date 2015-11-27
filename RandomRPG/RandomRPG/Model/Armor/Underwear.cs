using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Armor
{
    class Underwear : IArmor
    {
        public Underwear()
        {
            Name = "Underwear";
            Durability = 10;
            EquipLocation = BodyPart.Pants;
        }

        public string Name { get; set; }
        public int Durability { get; set; }
        public BodyPart EquipLocation { get; }
    }
}
