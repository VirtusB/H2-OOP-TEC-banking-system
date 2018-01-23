using System;
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
        public static string linjeFormat = "──────────────────────────────────────────────────────────────────────────────────────"; //linje som bruges til formattering
        public static string Navn; // navn til SQL connection metode i 'VoresServere' klassen
        public static bool userExist = true; //global variabel
        public static int valueToEdit;

        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            #region Select server
            Console.WriteLine("Please enter the number of which user server you are using, to make connectionstring correct.\n1: Virtus\n2: Bjarke\n3: Morten");
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

            DateTime timeLoggedIn = DateTime.Now;
            string currentlyLoggedIn = user.Username;

            #endregion

            Customer customer = new Customer(); //initialiser customer objekt
            Account account = new Account(); // initialiser account object
            Transaction transaction = new Transaction(); // initialiser transation objekt
            AccountType accountType = new AccountType(); // initialiser accountType objekt

            // Menu start
            bool done = false;  // udskriv menuen så længe at done ikke er true
            do
            {
                //Console.Clear();
                Console.WriteLine("{1} \tLogget ind som: {0}", currentlyLoggedIn, timeLoggedIn); // udskriv nuværende bruger samt dato for login
                Console.WriteLine(Program.linjeFormat);
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
                        Console.WriteLine("Rediger en kunde");
                        #region kode + errorchecks
                        Write("\nIndtast kunde nummer, som du vil redigere: ");
                        string strCustID = Console.ReadLine();

                        if (int.TryParse(strCustID, out int tempCustID))
                        {
                            customer.CustomerID = tempCustID;
                        }
                        else
                        {
                            Console.WriteLine("Indtast et gyldigt kunde nummer");
                        }

                        bool customerEditDone = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Rediger en kunde");
                            WriteLine("1) Rediger fornavn\n2) Rediger efternavn\n3) Rediger adresse\n4) Rediger by\n5) Rediger postnr.\n0) Afslut redigering\n");

                            Write("Indtastning: ");
                            string strValueToEdit = Console.ReadLine();

                            try
                            {
                                valueToEdit = int.Parse(strValueToEdit);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("\r\nIkke et gyldigt valg?\r\n");
                                continue;
                            }

                            switch (valueToEdit)
                            {
                                case 1:
                                    Write("\nFornavn: ");
                                    customer.FirstName = Console.ReadLine();
                                    customer.EditCustomer();
                                    Thread.Sleep(2000);

                                    break;
                                case 2:
                                    Write("\nEfternavn: ");
                                    customer.LastName = Console.ReadLine();
                                    customer.EditCustomer();
                                    Thread.Sleep(2000);
                                    break;
                                case 3:
                                    Write("\nAdresse: ");
                                    customer.Address = Console.ReadLine();
                                    customer.EditCustomer();
                                    Thread.Sleep(2000);
                                    break;
                                case 4:
                                    Write("\nBy: ");
                                    customer.City = Console.ReadLine();
                                    customer.EditCustomer();
                                    Thread.Sleep(2000);
                                    break;
                                case 5:
                                    Write("\nPostnr: ");
                                    //customer.PostalCode = Convert.ToInt32(Console.ReadLine());
                                    tempPostalCode = Console.ReadLine();
                                    int.TryParse(tempPostalCode, out postalValid);
                                    if (postalValid.ToString().Length == 4)
                                    {
                                        customer.PostalCode = postalValid;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nUgyldigt postnummer. Skal være 4 tal");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    customer.EditCustomer();
                                    Thread.Sleep(2000);
                                    break;
                                case 0:
                                    Console.Clear();
                                    customerEditDone = true;
                                    break;
                                default:
                                    Console.WriteLine("Ikke et gyldigt valg");
                                    continue;
                            }
                        } while (!customerEditDone);
                        #endregion
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

                        strCustID = Console.ReadLine();
                        if (int.TryParse(strCustID, out tempCustID))
                        {
                            account.CustomerID = tempCustID;
                            account.AddAccount();
                        }
                        else
                        {
                            Console.WriteLine("\nIndtast et gyldigt kunde nummer");
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

                        #region Tjek om bruger eksisterer inkl. error checks
                        do
                        {
                            Write("Indtast brugernavn: ");
                            string inputUsername = Console.ReadLine();

                            if (inputUsername == "")
                            {
                                Console.WriteLine("Brugernavn må ikke være tomt");
                            }
                            else if (inputUsername.Length < 4)
                            {
                                Console.WriteLine("Brugernavn skal være på mindst 4 karakterer");
                            }
                            else if (User.IsAllLettersOrDigits(inputUsername) != true)
                            {
                                Console.WriteLine("Brugernavn må ikke indeholde specielle tegn");
                            }
                            else
                            {
                                user.Username = inputUsername;
                                user.AddUserCheck();
                            }
                        } while (Program.userExist == true);
                        #endregion

                        #region Tjek kodeord og tilføj bruger


                        string duplicatePass = "placeholderDup";
                        string inputPassword = "placeholder";

                        while (duplicatePass != inputPassword)
                        {
                            Write("\nIndtast adgangskode: ");
                            inputPassword = User.GetConsolePassword();

                            if (inputPassword == "")
                            {
                                Console.WriteLine("Adgangskode må ikke være blank");
                            }
                            else if (inputPassword.Length < 6)
                            {
                                Console.WriteLine("Adgangskode skal være på mindst 6 karakterer");
                            }
                            else
                            {
                                Console.Write("\nBekræft adgangskode: ");
                                duplicatePass = User.GetConsolePassword();
                                if (duplicatePass == inputPassword)
                                {
                                    user.UserPassword = inputPassword;
                                    user.AddUser();
                                }
                                else
                                {
                                    ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nIndtastede adgangskoder ikke ens");
                                    ResetColor();

                                }
                            }
                        }

                        #endregion

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

