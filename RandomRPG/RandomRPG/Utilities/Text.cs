using System;
using System.Collections.Generic;
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

        public static char PromptCharacter(string output, List<MenuOption> options, bool error = false)
        {
            Clear();
            if (error)
            {
                ColorWriteLine("Invalid input", ConsoleColor.Red);    
            }

            WriteLine(output);

            foreach (var menuOption in options)
            {
                WriteLine($"{menuOption.Choice}) {menuOption.Value}");
            }

            ConsoleKeyInfo result = Console.ReadKey();

            if (options.All(x => x.Choice != result.KeyChar))
            {
                return PromptCharacter(output, options, true);
            }

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

        public static string Prompt(string output, bool error = false)
        {
            Clear();
            if (error)
            {
                ColorWriteLine("Invalid input", ConsoleColor.Red);
            }

            WriteLine(output);
            var result = Console.ReadLine();

            return string.IsNullOrEmpty(result) ? Prompt(output, true) : result;
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
            Divider();
        }

        public static void ClearWithAbilities()
        {
            Console.Clear();
            Header.PrintHeader();
            Console.SetCursorPosition(0, Player.Instance.CurrentGladiator.Attributes.Count + 2);
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