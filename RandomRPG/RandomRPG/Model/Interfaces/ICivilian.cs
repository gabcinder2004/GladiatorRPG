using System.Collections.Generic;

namespace RandomRPG.Model.Interfaces
{
    public interface ICivilian : IUnit
    {
        List<string> Prompts { get; set; }

        string GetRandomPrompt();
    }
}