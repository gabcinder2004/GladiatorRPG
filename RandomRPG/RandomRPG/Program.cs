using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomRPG.Model;

namespace RandomRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RPG Game v0.01");
            Doctore doc = new Doctore();
            Console.WriteLine(doc.SpecialAttack());
            Console.Read();
        }
    }
}
