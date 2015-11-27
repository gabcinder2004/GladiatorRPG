using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model;
using RandomRPG.Model.Interfaces;

namespace RandomRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                We have to think of a good way to have a clean and constant flow of output. 
                This main method will get flooded with conditionals if we don't find a way to abstract out all the possible scenarios a user can follow.
                This code here is only temporary.
            */

            Console.WriteLine(Resources.Version);
            Console.WriteLine(Resources.Introduction);
            Console.WriteLine(Resources.MainMenu);
            var userInput = Console.ReadLine();
            if (userInput == "1")
            {
                IGladiator gladiator;
                Console.Clear();
                Console.WriteLine(Resources.CharacterCreation_Intro);
                Console.WriteLine(Resources.CharacterCreation_Name);
                var gladiatorName = Console.ReadLine();

                Console.WriteLine(Resources.CharacterCreation_Class);
                var gladiatorClass = Console.ReadLine();

                switch (gladiatorClass)
                {
                    case "Doctore":
                        gladiator = new Doctore { Name = gladiatorName };
                        break;
                }
            }

            //gladiator.Attack("string") => calls an attack from some other class.

            Console.Read();
        }
    }
}
