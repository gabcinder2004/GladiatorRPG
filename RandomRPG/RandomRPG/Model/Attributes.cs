using System.Security.Cryptography.X509Certificates;

namespace RandomRPG.Model
{
    //public class Attributes 
    //{
    //    public int Strength { get; set; }
    //    public int Agility { get; set; }
    //    public int Vitality { get; set; }
    //    public int CritChance { get; set; }
    //    public double HitPoints { get; set; }
    //    public int Energy { get; set; }
    //}

    public interface IAttribute
    {
        AttributeType Type { get; }
        int Value { get; set; }

    }

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


    public enum AttributeType
    {
        Strength,
        Agility,
        Vitality,
        CritChance,
        HitPoints,
        Energy
    }
}