﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using static System.Console;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Security;

namespace ConnectToSqlWithCSharp
{
    public static class Program
    {



        public static string linjeFormat = "──────────────────────────────────────────────────────────────────────────────────────";
        public static string Navn; // navn til SQL connection metode i 'VoresServere' klassen 


        public static void Main()
        {


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            

            #region Select server
            Console.WriteLine("Please enter the number of which users server you are using, to make connectionstring correct.\n1: Virtus\n2: Bjarke\n3: Morten");
            string navnInput = Console.ReadLine();
            try
            {
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
                        var serverSelErr = String.Join(
                                         Environment.NewLine,
                                         "\nIndtast en gyldig " +
                                         "valgmulighed\n");

                        throw new Exception(serverSelErr);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToUpper());
                Main();
            }

            #endregion

            #region Login

            User user = new User();

            do
            {
                Console.Write("Indtast brugernavn: ");
                user.Username = Console.ReadLine();              
            } while (user.UserCheck() != true);


            do
            {          
                Console.Write("\nIndtast adgangskode: ");
                //user.UserPassword = Console.ReadLine();       
                user.UserPassword = User.GetConsolePassword();
            } while (user.UserAuth() != true);

            ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nLogger ind ...\n");
            ResetColor();
            Thread.Sleep(850);
            Console.Clear();

            #endregion
            
            

            

            Customer customer = new Customer();
            Account account = new Account();
            Transaction transaction = new Transaction();
            AccountType accountType = new AccountType();
            


            


            // Menu start
            bool done = false;
            do
            {
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("\t1) Vis kunde");
                Console.WriteLine("\t2) Tilføj kunde");
                Console.WriteLine("\t3) Slet kunde");
                Console.WriteLine("\t4) Rediger kunde - IKKE IMPLEMENTERET");
                Console.WriteLine("\t5) Vis konti");
                Console.WriteLine("\t6) Opret Konto");
                Console.WriteLine("\t7) Slet konto fra kunde");
                Console.WriteLine("\t8) Vis transaktioner");
                Console.WriteLine("\t9) Opret transaktion");
                Console.WriteLine("\t10) Opret ny kontotype");
                Console.WriteLine("\t11) Opret ny bruger");             
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
                        Console.WriteLine("Tilføj en kunde\n");
                        #region kode + errorchecks
                        Console.Write("Indtast fornavn: ");
                        // tjek om fornavn er gyldigt
                        string CustFName = Console.ReadLine();
                        if (CustFName.Length >= 2 && !double.TryParse(CustFName, out double tempCustFName) && tempCustFName != 1)
                        {
                            customer.FirstName = CustFName;
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldigt fornavn");
                            break;
                        }

                        Console.Write("\nIndtast efternavn: ");
                        // tjek om efternavn er gyldigt
                        string CustLName = Console.ReadLine();
                        if (CustLName.Length >= 2 && !double.TryParse(CustLName, out double tempCustLName) && tempCustLName != 1)
                        {
                            customer.LastName = CustLName;
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldigt efternavn");
                            break;
                        }

                        Console.Write("\nAdresse: ");
                        // tjek om adresse er gyldig
                        string CustAddr = Console.ReadLine();
                        if (CustAddr.Length >= 4 && !double.TryParse(CustAddr, out double tempCustAddr) && tempCustAddr != 1)
                        {
                            customer.Address = CustAddr;
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldig adresse");
                            break;
                        }

                        Console.Write("\nBy: ");
                        // tjek om bynavn er gyldigt
                        string CustCity = Console.ReadLine();
                        if (CustCity.Length >= 2 && !double.TryParse(CustCity, out double tempCustCity) && tempCustCity != 1)
                        {
                            customer.City = CustCity;
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldigt bynavn");
                            break;
                        }

                        Console.Write("\nPostnr: ");
                        // tjek om postnummeret er gyldigt
                        string tempPostalCode = Console.ReadLine();
                        int postalValid;
                        int.TryParse(tempPostalCode, out postalValid);
                        if (postalValid.ToString().Length == 4)
                        {
                            customer.PostalCode = postalValid;
                            customer.AddCustomer();
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldigt postnummer");
                            break;
                        }
                        #endregion
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Slet en kunde");
                        #region kode + errorchecks
                        Console.Write("Indtast customer ID: ");
                        // tjek om cust id er gyldigt
                        string custIDTemp = Console.ReadLine();
                        int custIDValid;
                        if (int.TryParse(custIDTemp, out custIDValid))
                        {
                            customer.CustomerID = custIDValid;
                            customer.DeleteCustomer();
                        }
                        else
                        {
                            Console.WriteLine("\nUgyldigt kunde nummer");
                            break;
                        }
                        #endregion
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Rediger en kunde - IKKE IMPLEMENTERET");
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Vis konti");
                        account.ShowAccounts();
                        break;
                    case 6:
                        Console.Clear();
                        WriteLine("Tilføj en konto til en kunde");
                        #region kode + errorchecks
                        Write("\nIndtast kunde nummer, som du vil oprette en konto for: ");

                        string strCustID = Console.ReadLine();
                        if (int.TryParse(strCustID, out int tempCustID))
                        {
                            account.CustomerID = tempCustID;
                            account.AddAccount();
                        }
                        else
                        {
                            Console.WriteLine("Indtast et gyldigt kunde nummer");
                        }
                        #endregion
                        break;
                    case 7:
                        Console.Clear();
                        Console.Write("Indtast kontonr: ");
                        #region kode + errorchecks
                        // tjek konto nummer
                        string strAccNo = Console.ReadLine();
                        if (int.TryParse(strAccNo, out int tempAccNo))
                        {
                            account.AccountNo = tempAccNo;
                            account.DeleteAccount();
                        }
                        else
                        {
                            Console.WriteLine("Indtast et gyldigt konto nummer");
                        }
                        #endregion
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
                    case 10:
                        Console.Clear();
                        Console.WriteLine("Opret ny kontotype\n");
                        #region kode + errorchecks
                        Write("Indtast navn på ny kontotype: ");
                        string accTypeName = Console.ReadLine();
                        if (accTypeName == "")
                        {
                            Console.WriteLine("\nNavn på kontotypen må ikke være blank");
                            break;
                        }
                        else
                        {
                            accountType.AccountTypeName = accTypeName;
                        }


                        Write("\nIndtast rentesats på kontotype {0} i PROCENT: ", accTypeName);
                        string accInterestRate = Console.ReadLine();
                        if (double.TryParse(accInterestRate, out double tempInterestRate))
                        {
                            accountType.InterestRate = tempInterestRate / 100;
                            accountType.AddAccountType();
                        }
                        else
                        {
                            Console.WriteLine("\nIndtast et gyldigt tal");
                        }
                        #endregion
                        break;
                    case 11:
                        Console.Clear();
                        Console.WriteLine("Opret ny bruger\n");
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

