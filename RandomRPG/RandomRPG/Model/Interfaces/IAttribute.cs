namespace RandomRPG.Model
{
    public interface IAttribute
    {
        AttributeType Type { get; }
        int Value { get; set; }

    }
}