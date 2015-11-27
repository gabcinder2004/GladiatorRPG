namespace RandomRPG.Model.Interfaces
{
    /// <summary>
    /// Base entity in the game for creatures/units. This can be any particular unit.
    /// </summary>
    public interface IUnit
    {
        string Name { get; set; }
        Attributes Attributes { get; set; }
    }
}