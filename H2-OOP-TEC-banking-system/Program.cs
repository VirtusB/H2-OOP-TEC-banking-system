using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2_OOP_TEC_banking_system
{
    class Program
    {
        static void Main(string[] args)
        {

            string showAccountsChoice = Console.ReadLine();



            if (int.TryParse(showAccountsChoice, out int parsedSuccess))
            {
                Console.WriteLine("specific");
                Console.WriteLine(parsedSuccess);
            }
            else
            {
                Console.WriteLine("alle");
            }

            Console.ReadKey();
        }
        
    }
}
