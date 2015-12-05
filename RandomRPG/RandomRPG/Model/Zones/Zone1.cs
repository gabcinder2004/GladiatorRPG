using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using RandomRPG.Controllers;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Factories;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Zones
{
    class Zone1 : IZone, IDisposable
    {
        private static Zone1 _instance;
        public ZoneMap Map { get; set; }
        private Zone1()
        {
            StateChanged += OnStateChanged;
            Map = new ZoneMap(10, 10);
            PopulateMap();
        }

        private void PopulateMap()
        {
            int x = 0;
            int y = 0;
            int counter = 1;
            var gladTypes = EnumUtil.GetValues<GladiatorTypes>().ToList();
            while (counter < 8)
            {
                Random random = new Random();
                x = random.Next(0, Map.MapHeight);
                y = random.Next(0, Map.MapWidth);
                //Need a random name generator
                if (Map.GetTile(x, y) == null)
                {
                    if (counter < 4)
                    {
                        Gladiator glad = NpcFactory.GetRandomNpcGladiatorInstance();
                        Map.SetTile(x, y, glad);
                    }
                    else
                    {
                        ICivilian villager = NpcFactory.GetVillager();
                        Map.SetTile(x, y, villager);
                    }
                    counter++;
                }
            }

            bool playerInserted = false;
            while (!playerInserted)
            {
                Random rand = new Random();
                x = rand.Next(0, Map.MapHeight);
                y = rand.Next(0, Map.MapWidth);
                if (Map.GetTile(x, y) == null)
                {
                    Map.SetTile(x, y, Player.Instance.CurrentGladiator);
                    playerInserted = true;
                }
            }
        }

        public Action<GameEvent> StateChanged { get; set; }
        public List<IZone> ConnectedZones { get; set; }
        public static Zone1 Instance
        {
            get { return _instance ?? (_instance = new Zone1()); }
            private set
            {
                _instance = value;
            }
        }

        private void OnStateChanged(GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.ZoneEnter:
                    EnteredZone();
                    break;
                case GameEvent.ZoneLeave:
                    LeaveZone();
                    break;
            }
        }

        private void EnteredZone()
        {
            //Wierd that clear reloads map
            Text.Clear();
            Text.WriteLine("Entered Zone1");
            //Console.Read();
        }

        private void LeaveZone()
        {
            Text.WriteLine("Entered Zone2");
        }

        public void Dispose()
        {
            Instance = null;
        }
    }
}
