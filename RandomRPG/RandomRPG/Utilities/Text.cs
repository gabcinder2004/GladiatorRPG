using System;

namespace RandomRPG.Utilities
{
    public static class Text
    {
        public static T Prompt<T>(string output)
        {
            WriteLine(output);
            T result;

            try
            {
                result = (T) Convert.ChangeType(Console.ReadLine(), typeof (T));
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
            WriteLine("==================================");
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
        }
    }
}