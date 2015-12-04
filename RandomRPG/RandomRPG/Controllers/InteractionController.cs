using RandomRPG.Model;
using RandomRPG.Model.Enums;
using RandomRPG.Model.Interfaces;
using RandomRPG.Utilities;

namespace RandomRPG.Controllers
{
    class InteractionController
    {
        public static void Interact(IGladiator player, IUnit target)
        {
            switch (target.Reputation)
            {
                case Reputation.Hostile:
                    Player.Instance.CurrentGladiator.SetTargetGladiator(target as IGladiator);
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
