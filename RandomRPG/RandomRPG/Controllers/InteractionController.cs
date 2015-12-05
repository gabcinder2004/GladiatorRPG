using System;
using System.Collections.Generic;
using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Model.Units;
using RandomRPG.Utilities;

namespace RandomRPG.Controllers
{
    class InteractionController
    {
        public static void Interact(Gladiator player, IUnit target)
        {
            switch (target.Reputation)
            {
                case Reputation.Hostile:
                    Player.Instance.CurrentGladiator.SetTargetGladiator(target as Gladiator);
                    Program.GameState = GameState.Battle;
                    return;
                case Reputation.Friendly:
                case Reputation.Neutral:
                    Text.Clear();
                    InteractWithFriendly(target);
                    break;
            }
        }

        private static void InteractWithFriendly(IUnit target)
        {
            var civilian = target as ICivilian;

            var prompt = civilian.GetRandomPrompt();

            // Need to find a good way to customize options... Hardcoding for now
            var options = new List<MenuOption> { new MenuOption('1', "Tell me more..."), new MenuOption('2', "Okay, bye.") };

            switch (Text.PromptCharacter(prompt, options))
            {
                case '2':
                    {
                        Program.GameState = GameState.Playing;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
