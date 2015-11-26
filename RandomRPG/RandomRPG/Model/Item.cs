namespace RandomRPG
{
    public abstract class Item
    {
        public string Name { get; set; }
        public int Durability { get; set; }
        public BodyPart EquipLocation { get; set; }
    }
}