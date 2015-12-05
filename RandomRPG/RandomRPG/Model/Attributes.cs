using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;

namespace RandomRPG.Model.Units
{
    public class Strength : IAttribute
    {
        public AttributeType Type => AttributeType.Strength;
        public int Value { get; set; }
    }

    public class Agility : IAttribute
    {
        public AttributeType Type => AttributeType.Agility;
        public int Value { get; set; }
    }

    public class Vitality : IAttribute
    {
        public AttributeType Type => AttributeType.Vitality;
        public int Value { get; set; }
    }

    public class CritChance : IAttribute
    {
        public AttributeType Type => AttributeType.CritChance;
        public int Value { get; set; }
    }

    public class Energy: IAttribute
    {
        public AttributeType Type => AttributeType.Energy;
        public int Value { get; set; }
    }

    public class HitPoints : IAttribute
    {
        public AttributeType Type => AttributeType.HitPoints;
        public int Value { get; set; }
    }
}