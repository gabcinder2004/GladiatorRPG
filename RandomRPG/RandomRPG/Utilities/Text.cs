using System;
using System.Linq;
using RandomRPG.Model;
using RandomRPG.Model.Enums;

namespace RandomRPG.Utilities
{
    public enum ConsoleSide
    {
        Left,
        Right
    }

    public static class Text
    {
        public static void WriteLine(ConsoleSide side, int top, string output)
        { 
            var left = (side == ConsoleSide.Left) ? 1 : Console.WindowWidth / 2;
            Console.SetCursorPosition(left, top);
            Console.WriteLine(output);
        }

        public static void Write(string output)
        {
            Console.Write(output);
        }

        public static char PromptCharacter(string output)
        {
            WriteLine(output);

            ConsoleKeyInfo result = Console.ReadKey();
            return result.KeyChar;
        }

        public static T Prompt<T>(string output)
        {
            WriteLine(output);
            T result;

            try
            {
                result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
            }
            catch (Exception)
            {
                Clear();
                ColorWriteLine("Please enter a valid option", ConsoleColor.Red);
                Divider();
                result = Prompt<T>(output);
            }

            return result;
        }

        public static void Divider()
        {
            Console.WriteLine("===========================================================================");
        }

        public static string Prompt(string output)
        {
            WriteLine(output);
            return Console.ReadLine();
        }

        public static void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public static void ColorWriteLine(string output, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Clear()
        {
            Console.Clear();
            Header.PrintHeader();
            //if (Player.Instance.CurrentGladiator != null && Program.GameState == GameState.Playing)
            //{
            //    ColorWriteLine(Player.Instance.CurrentGladiator.Name+ "-" + Player.Instance.CurrentGladiator.Attributes.HitPoints + " (HP)", ConsoleColor.Green);
            //    ColorWriteLine(Player.Instance.CurrentGladiator.Name + "-" + Player.Instance.CurrentGladiator.Attributes.Energy + " (Energy)", ConsoleColor.Yellow);
            //    ColorWriteLine(Player.Instance.CurrentGladiator.Name + "-" + Player.Instance.CurrentGladiator.Kills + " (Kills)", ConsoleColor.Red);
            //}
            Divider();

        }

        public static void ClearWithAbilities()
        {
            Console.Clear();
            Header.PrintHeader();
            Divider();
            //display over max
            WriteLine(Player.Instance.CurrentGladiator.Name + "'s Abilities:\n");

            Player.Instance.CurrentGladiator.DisplayAbilityOptions();
            Divider();
            ColorWriteLine("You have encountered a " + Player.Instance.CurrentGladiator.Target.Name + "!!!", ConsoleColor.Cyan);
            Divider();
        }
    }
}