using System.Collections.Generic;

namespace RandomRPG.Model
{
    public abstract class Gladiator : Unit
    {
        public Dictionary<BodyPart, Item> GearSlots { get; set; }
        public List<Item> Inventory { get; set; }
        public Attributes Attributes { get; set; }
    }
}