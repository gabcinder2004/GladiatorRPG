﻿using System.Collections.Generic;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;

namespace RandomRPG.Model.Factories
{
    public static class AttributeFactory
    {

        public static List<IAttribute> GetInstance(string type)
        {
            switch (type)
            {
                case "Doctore":
                    return new List<IAttribute>()
                    {
                        new Strength() {Value = 25},
                        new Agility() {Value = 50},
                        new CritChance() {Value = 25},
                        new Vitality() {Value = 50},
                        new Energy() {Value = 75},
                        new HitPoints() {Value = 100}
                    };

                case "Slave":
                    return new List<IAttribute>()
                    {
                        new Strength() {Value = 15},
                        new Agility() {Value = 10},
                        new CritChance() {Value = 20},
                        new Vitality() {Value = 50},
                        new Energy() {Value = 75},
                        new HitPoints() {Value = 100}
                    };

                case "Krixus":
                    return new List<IAttribute>()
                    {
                        new Strength() {Value = 50},
                        new Agility() {Value = 10},
                        new CritChance() {Value = 25},
                        new Vitality() {Value = 200},
                        new Energy() {Value = 75},
                        new HitPoints() {Value = 400}
                    };

                case "Villager":
                    return new List<IAttribute>()
                    {
                        new Strength() {Value = 10},
                        new Agility() {Value = 10},
                        new CritChance() {Value = 10},
                        new Vitality() {Value = 25},
                        new Energy() {Value = 75},
                        new HitPoints() {Value = 50}
                    };
                               
                case "MurderDummy":
                    return new List<IAttribute>()
                    {
                        new Strength() {Value = 10},
                        new Agility() {Value = 10},
                        new CritChance() {Value = 10},
                        new Vitality() {Value = 25},
                        new Energy() {Value = 75},
                        new HitPoints() {Value = 50}
                    };

                default:
                    return new List<IAttribute>();
            }
        }
    }
}