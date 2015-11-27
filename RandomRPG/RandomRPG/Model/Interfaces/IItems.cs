namespace RandomRPG.Model
{
    public interface IItems
    {
        string Name { get; }
    }

    public interface IEquippable : IItems
    {
        int Durability { get; set; }
    }
}