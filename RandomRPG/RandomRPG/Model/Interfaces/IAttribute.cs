using RandomRPG.Model.Enums;

namespace RandomRPG.Model.Interfaces
{
    public interface IAttribute
    {
        AttributeType Type { get; }
        int Value { get; set; }

    }
}