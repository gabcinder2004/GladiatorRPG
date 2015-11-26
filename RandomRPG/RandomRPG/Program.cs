using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RPG Game v0.01");
        }
    }

    public class World
    {
        public List<Zone> Zones { get; set;}
        public List<Gladiator> Gladiators { get; set; } 
    }

    public class Zone
    {
        public string Name { get; set; }
    }

    public class Unit
    {
        public string Name { get; set; }
        public double HitPoints { get; set; }
        public int Energy { get; set; }
    }

    public class Player
    {
        public List<Gladiator> Gladiators { get; set; } 
    }

    public abstract class Gladiator : Unit
    {
        public Dictionary<BodyPart, Item> GearSlots { get; set; }
        public List<Item> Inventory { get; set; }
        public Attributes Attributes { get; set; }
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public int Durability { get; set; }
        public BodyPart EquipLocation { get; set; }
    }

    public class Attributes
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Vitality { get; set; }
    }

    public enum BodyPart
    {
        Head,
        Chest,
        MainHand,
        OffHand,
        Pants,
    }

}
