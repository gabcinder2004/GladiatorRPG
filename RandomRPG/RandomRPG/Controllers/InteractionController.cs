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
                    var civilian = target as ICivilian;
                    if (civilian != null)
                        Text.WriteLine(civilian.GetRandomPrompt());
                    break;
            }
        }
    }
}
