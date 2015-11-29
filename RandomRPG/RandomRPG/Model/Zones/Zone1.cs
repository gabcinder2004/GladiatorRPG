using System;
using System.Collections.Generic;
using RandomRPG.Controllers;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Zones
{
    class Zone1 : IZone
    {
        private static IZone _instance;
        public ZoneMap Map { get; set; }
        private Zone1()
        {
            StateChanged += OnStateChanged;
            Map = new ZoneMap(10, 10);
            IGladiator doctore = new Gladiator("Doctore", GladiatorTypes.Doctore);
            Map.SetTile(5, 5, doctore);
            Map.SetTile(9, 5, Player.Instance.CurrentGladiator);
        }

        public Action<GameEvent> StateChanged { get; set; }
        public List<IZone> ConnectedZones { get; set; }
        public static IZone Instance => _instance ?? (_instance = new Zone1());

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
    }
}
