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

            IGladiator glad = new Doctore();
            glad.BattleCry();
            Console.WriteLine(glad.ToString());

            //gladiator.Attack("string") => calls an attack from some other class.

            Console.Read();
        }
    }
}
