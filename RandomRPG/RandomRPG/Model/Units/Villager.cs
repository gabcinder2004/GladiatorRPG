using System;
using System.Collections.Generic;
using RandomRPG.Controllers;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Model.Gladiators
{
    public class Villager : ICivilian
    {
        public string Name { get; set; }
        public List<IAttribute> Attributes { get; set; }
        public ITile CurrentTile { get; set; }
        public Reputation Reputation { get; set; }
        public List<string> Prompts { get; set; }

        public Action<IGladiator> InteractionTriggered { get; set; }

        public Villager(string name, List<IAttribute> attributes, Reputation reputation, List<string> prompts)
        {
            Name = name;
            Attributes = attributes;
            Reputation = reputation;
            Prompts = prompts;
            InteractionTriggered += OnInteractionTriggered;
        }

        private void OnInteractionTriggered(IGladiator unit)
        {
            InteractionController.Interact(unit, this);
        }

        public string GetRandomPrompt()
        {
            return Prompts[new Random().Next(0, Prompts.Count-1)];
        }
    }
}