namespace RandomRPG.Model
{
    public class Attributes
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Vitality { get; set; }
        public int CritChance { get; set; }
        // I think we need to consider putting HP in attributes
        public double HitPoints { get; set; }
        // I think we need to consider putting Energy in attributes
        public int Energy { get; set; }
    }
}