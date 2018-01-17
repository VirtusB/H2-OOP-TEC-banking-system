using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;



namespace ConnectToSqlWithCSharp
{
    static class Program
    {
       

        static void Main(string[] args)
        {


            Customer customer = new Customer();
            Account account = new Account();


            // Menu start
            bool done = false;
            do
            {
                
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("\t1) Vis konti");
                Console.WriteLine("\t2) Tilføj kunde");
                Console.WriteLine("\t3) Slet kunde");
                Console.WriteLine("\t4) Rediger kunde");
                Console.WriteLine("\t5) Tilføj konto til kunde");
                Console.WriteLine("\t6) Slet konto fra kunde");
                Console.Write("Indtast valgmulighed (0 for at afslutte): ");
                string strSelection = Console.ReadLine();
                int iSel;
                try
                {
                    iSel = int.Parse(strSelection);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\r\nHvad?\r\n");
                    continue;
                }
                Console.Clear();
                Console.WriteLine("Du valgte " + iSel);
                switch (iSel)
                {
                    case 0:
                        done = true;
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Kundeoversigt");
                        account.ShowAccounts();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Tilføj en kunde");
                        
                        
                        customer.AddCustomer();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Slet en kunde");
                        customer.DeleteCustomer();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Rediger en kunde");
                        break;
                    case 6:
                        account.DeleteAccount();
                        break;
    
                    default:
                        Console.WriteLine("Forkert, vælg en korrekt mulighed: {0}\r\n", iSel);
                        continue;
                }
                Console.WriteLine();
            } while (!done);

            Console.WriteLine("\nFarvel!");
        }
        // Menu slut


       

    
        }

        
    }

