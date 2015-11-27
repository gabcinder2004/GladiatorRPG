using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model;

namespace RandomRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Resources.Version);
            Console.WriteLine(Resources.Introduction);
            Console.WriteLine(Resources.MainMenu);
            var userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Console.Clear();
                Console.WriteLine(Resources.CharacterCreation_Intro);
                Console.WriteLine(Resources.CharacterCreation_Name);
            }
            Doctore doc = new Doctore();
            Console.WriteLine(doc.SpecialAttack());
            Console.Read();
        }
    }
}
