namespace RandomRPG.Model
{
    /// <summary>
    /// Base entity in the game. This can be any particular unit.
    /// </summary>
    public interface IEntity
    {
        string Name { get; set; }
        double HitPoints { get; set; }
    }
}