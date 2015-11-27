namespace RandomRPG.Model
{
    /// <summary>
    /// The abstraction that will define an item being equippable
    /// </summary>
    public interface IEquippable : IItems
    {
        int Durability { get; set; }
    }
}