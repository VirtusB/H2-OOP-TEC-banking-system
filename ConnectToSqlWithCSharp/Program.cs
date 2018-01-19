using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using static System.Console;


namespace ConnectToSqlWithCSharp
{
    static class Program
    {
        public static string linjeFormat = "──────────────────────────────────────────────────────────────────────────────────────";
        public static string Navn; // navn til SQL connection metode i 'VoresServere' klassen 

        static void Main(string[] args)
        {   
            Console.OutputEncoding = System.Text.Encoding.UTF8;
#region Select server
            Console.WriteLine("Please enter the number of which users server you are using, to make connectionstring correct.\n1: Virtus\n2: Bjarke\n3: Morten");
            string navnInput = Console.ReadLine();
            switch (navnInput)
            {
                case "1":
                    Navn = "Virtus";
                    Clear();
                    break;
                case "2":
                    Navn = "Bjarke";
                    Clear();
                    break;
                case "3":
                    Navn = "Morten";
                    Clear();
                    break;
                default:
                    throw new NotImplementedException("You should type a number matching one of the three users, and press enter.");
            }
#endregion
            Customer customer = new Customer();
            Account account = new Account();
            Transaction transaction = new Transaction();

            // Menu start
            bool done = false;
            do
            {               
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("\t1) Vis kunde");
                Console.WriteLine("\t2) Tilføj kunde");
                Console.WriteLine("\t3) Slet kunde");
                Console.WriteLine("\t4) Rediger kunde");
                Console.WriteLine("\t5) Vis konti");
                Console.WriteLine("\t6) Opret Konto");
                Console.WriteLine("\t7) Slet konto fra kunde");
                Console.WriteLine("\t8) Vis transaktioner");
                Console.WriteLine("\t9) Opret transaktion");
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
                        WriteLine(linjeFormat + "\n\n");         
                        customer.ShowCustomer();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Tilføj en kunde");

                        Console.Write("Indtast fornavn: ");
                        customer.FirstName = Console.ReadLine();

                        Console.Write("\nIndtast efternavn: ");
                        customer.LastName = Console.ReadLine();

                        Console.Write("\n Adresse: ");
                        customer.Address = Console.ReadLine();

                        Console.Write("\n By: ");
                        customer.City = Console.ReadLine();

                        Console.Write("\n Postnr: ");
                        customer.PostalCode = Convert.ToInt32(Console.ReadLine());

                        customer.AddCustomer();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Slet en kunde");
                        Console.Write("Indtast customer ID: ");
                        customer.CustomerID = Convert.ToInt32(Console.ReadLine());      
                        customer.DeleteCustomer();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Rediger en kunde");
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Vis konti");
                        account.ShowAccounts();
                        break;
                    case 6:
                        Console.Clear();
                        WriteLine("Tilføj en konto til en kunde");
                        account.AddAccount();
                        break;
                    case 7:
                        Console.Clear();
                        Console.Write("Indtast kontonr: ");                      
                        account.AccountNo = Convert.ToInt32(Console.ReadLine());
                        account.DeleteAccount();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine("Vis transaktioner");
                        Console.WriteLine("\nIndtast kontonr. for den ønskede konto");
                        Console.WriteLine("Tryk ENTER for at se alle transaktioner for alle konti");
                        transaction.ShowTransactions();
                        break;
                    case 9:
                        Console.Clear();
                        transaction.AddTransaction();
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

