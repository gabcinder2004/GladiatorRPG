namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// The abstraction that will define an item being equippable
    /// </summary>
    public interface IEquippable<T> : IItems
    {
        int Durability { get; set; }
        T Type { get; set; }
    }
}