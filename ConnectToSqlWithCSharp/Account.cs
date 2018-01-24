using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectToSqlWithCSharp;
using static System.Console;
using System.Reflection;

namespace ConnectToSqlWithCSharp
{
    public class Account
    {
        #region private attributes
        private int accountid;
        private int accountno;
        private double saldo;
        private int customerid;
        private string customername;
        private int accounttypeid;
        private string accounttypename;
        private DateTime created;
        private bool active;
        private double interestrate;
        private double InterestAmount;
        private double InterestTotal;
        #endregion

        #region getters & setters
        public int AccountID
        {
            get
            {
                return accountid;
            }
            set
            {
                accountid = value;
            }
        }

        public double InterestRate
        {
            get
            {
                return interestrate;
            }
            set
            {
                interestrate = value;
            }
        }

        public int CustomerID
        {
            get
            {
                return customerid;
            }
            set
            {
                customerid = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return customername;
            }
            set
            {
                customername = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return created;
            }
            set
            {
                created = value;
            }
        }

        public int AccountNo
        {
            get
            {
                return accountno;
            }
            set
            {
                accountno = value;
            }
        }

        public int AccountTypeID
        {
            get
            {
                return accounttypeid;
            }
            set
            {
                accounttypeid = value;
            }
        }

        public string AccountTypeName
        {
            get
            {
                return accounttypename;
            }
            set
            {
                accounttypename = value;
            }
        }

        public double Saldo
        {
            get
            {
                return saldo;
            }
            set
            {
                saldo = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        #endregion

        //Clean constructor explicitly written, good practice
        public Account()
        {
        }

        public void ShowAccounts()
        {

            Console.WriteLine("Indtast kontonr. for at se en specifik konto");
            Console.WriteLine("Tryk ENTER for at se alle konti");
            Console.WriteLine("Indtast \"overtræk\" eller \"ot\" for udelukkende at se konti med overtræk");

            //Input variable
            string showAccountsChoice = Console.ReadLine();
            //Standard sql string
            string sqlAccountsString = "SELECT AccountID, Customers.CustomerId ,CONCAT(firstname,' ',lastname) as CustomerName, Accounts.Created, AccountNo, AccountTypes.AccountTypeId, AccountTypeName, Saldo, Accounts.Active, InterestRate " +
                                        "FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid";

            List<Account> listOfAccounts = new List<Account>();

            SqlConnection conn = VoresServere.WhichServer(Program.Navn); // sæt connection string
            SqlCommand cmd = new SqlCommand("", conn);
            conn.Open();

            //Integer input (show specific)
            if (int.TryParse(showAccountsChoice, out int showSpecificAccount)) 
            {
                //Add specification to sql string
                cmd.CommandText = sqlAccountsString + " WHERE AccountNo=@showSpecificAccount"; // lav en SQL kommando
                cmd.Parameters.Add(new SqlParameter("@showSpecificAccount", showSpecificAccount));
            }

            //Empty input (show all)
            else if (showAccountsChoice == "") 
            {
                cmd.CommandText = sqlAccountsString; 
            }

            //If input is overtræk
            else if (showAccountsChoice.ToLower() == "overtræk" || showAccountsChoice.ToLower() == "ot")
            {
                cmd.CommandText = sqlAccountsString + " WHERE Saldo < 0";
            }

            else /*if (!int.TryParse(showAccountsChoice, out showSpecificAccount)) // hvis brugerens indtastning ikke kan parses til int, antag at input ikke er et tal*/
            {
                Console.WriteLine("\nIndtast et gyldigt kontonummer\n");
            }

            //Get the data
            SqlDataReader reader = cmd.ExecuteReader();
            //If no data to read
            if (!reader.HasRows)
            {
                Console.WriteLine("\nIngen konto med kontonummer {0} fundet\n", showAccountsChoice); 
            }
            //Read data
            while (reader.Read())
            {
                listOfAccounts.Add(new Account
                {
                    AccountID = (int)reader.GetValue(0),
                    CustomerID = (int)reader.GetValue(1),
                    CustomerName = (string)reader.GetValue(2),
                    Created = (DateTime)reader.GetValue(3),
                    AccountNo = (int)reader.GetValue(4),
                    AccountTypeID = (int)reader.GetValue(5),
                    AccountTypeName = (string)reader.GetValue(6),
                    Saldo = (double)reader.GetValue(7),
                    Active = (bool)reader.GetValue(8),
                    InterestRate = (double)reader.GetValue(9),
                    //TODO: make sure interestamount is correct, otherwise use: InterestAmount = (double)reader.GetValue(5) * (double)reader.GetValue(7)
                    InterestAmount = Saldo * InterestRate,
                    InterestTotal = Saldo + InterestAmount
                });
            }
            reader.Close();
            //Sort by Account number in the created list
            listOfAccounts.Sort((x, y) => x.AccountNo.CompareTo(y.AccountNo));
            //Print it
            for (int i = 0; i < listOfAccounts.Count(); i++)
            {
                listOfAccounts[i].PrintAccountInfo(true, i + 1);
            }
            conn.Close();
        }

        public void PrintAccountInfo(bool showOwner = false, int? count = null, bool tabulate = false)
        {
            Console.WriteLine("");
            if (tabulate)
            {
                //If-statements are for checking if arguments are null
                if (count != null)
                {
                    Console.WriteLine("\t{0}:", count);
                }
                Console.WriteLine("\tKontonummer: \t\t{0}", AccountNo.ToString());
                if (showOwner)
                {
                    Console.WriteLine("\tKontoens ejer: \t\t{0}", CustomerName);
                }
                Console.WriteLine("\tOprettelsesdato: \t{0}", Created.ToString("MMMM dd, yyyy"));
                Console.WriteLine("\tKontotype: \t\t{0}", AccountTypeName);
                Console.WriteLine("\tSaldo: \t\t\t{0:C}", Saldo.ToString());
                Console.WriteLine("\tStatus: \t\t{0}", Active ? "Aktiv" : "Inaktiv");
                Console.WriteLine("\tRentesats: \t\t{0:P}", InterestRate.ToString());
                if (Saldo > 0 && InterestRate > 0)
                {
                    Console.WriteLine("\tRentebeløb: \t\t{0:C} (+{1:C})", InterestTotal, InterestAmount);
                }
            }
            else
            {
                 //This is exactly the same as above but with a tabulation 
                if (count != null)
                {
                    Console.WriteLine("{0}:", count);
                }
                Console.WriteLine("Kontonummer: \t\t{0}", AccountNo.ToString());
                if (showOwner)
                {
                    Console.WriteLine("Kontoens ejer: \t\t{0}", CustomerName);
                }
                Console.WriteLine("Oprettelsesdato: \t{0}", Created.ToString("MMMM dd, yyyy"));
                Console.WriteLine("Kontotype: \t\t{0}", AccountTypeName);
                Console.WriteLine("Saldo: \t\t\t{0:C}", Saldo.ToString());
                Console.WriteLine("Status: \t\t{0}", Active ? "Aktiv" : "Inaktiv");
                Console.WriteLine("Rentesats: \t\t{0:P}", InterestRate.ToString());
                if (Saldo > 0 && InterestRate > 0)
                {
                    Console.WriteLine("Rentebeløb: \t\t{0:C} (+{1:C})", InterestTotal, InterestAmount);
                }
            }
        }

        public void AddAccount()
        {

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            conn.Open();
            SqlCommand checkCustID = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE CustomerID = @custCheck", conn);
            checkCustID.Parameters.Add(new SqlParameter("@custCheck", CustomerID));

            int checkedCust = (int)checkCustID.ExecuteScalar();
            if (checkedCust != 1)
            {
                Console.WriteLine("\nKunde eksisterer ikke");
                return;
            }

            Write("\nIndtast den kontotype du vil oprette: \n");

            // udskriv alle kontotyper
            SqlCommand selAccTypes = new SqlCommand("SELECT AccountTypeName FROM AccountTypes", conn);
            SqlDataReader reader = selAccTypes.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0)); //udskriv resultaterne
            }

            reader.Close();

            Write("\nIndtastning: ");
            string accTypeSelection = Console.ReadLine().ToLower();
            //selected account type
            SqlCommand selAccType = new SqlCommand("SELECT AccountTypeID FROM AccountTypes WHERE AccountTypeName=@accTypeSelection", conn);
            selAccType.Parameters.Add(new SqlParameter("@accTypeSelection", accTypeSelection));
            int accTypeID = (int)selAccType.ExecuteScalar();

            //Get highest existing Account Number
            SqlCommand selAccMax = new SqlCommand("SELECT TOP (1) [AccountNo] FROM [dbo].[Accounts] ORDER BY [AccountNo] DESC", conn);

            int? accMax = (int?)selAccMax.ExecuteScalar() + 1; 

            if (accMax == null)
            {
                accMax = 1000;
            }

            //Insert
            SqlCommand addAccCMD = new SqlCommand("INSERT INTO Accounts (CustomerID, AccountNo, AccountTypeId) VALUES (@custID, @accMax, @accTypeID)", conn);
            addAccCMD.Parameters.Add(new SqlParameter("@custID", CustomerID));
            addAccCMD.Parameters.Add(new SqlParameter("@accTypeId", accTypeID));
            addAccCMD.Parameters.Add(new SqlParameter("@accMax", accMax));
            addAccCMD.ExecuteNonQuery();
            Console.WriteLine("\nDu har oprettet en konto af typen {0}, med kontonummer {1}", accTypeSelection, accMax);

            conn.Close();
        }

        public void DeleteAccount()
        {
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);
            SqlCommand selAccIDCmd = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn); // hent accountno fra databasen
            selAccIDCmd.Parameters.Add(new SqlParameter("@accNo", AccountNo));
            conn.Open();

            try
            {
                int accountDeleted = (Int32)selAccIDCmd.ExecuteScalar();

                Write("\nEr du sikker på at du vil slette kontunummer {0}?\n", AccountNo);
                Write("Tast JA for at fortsætte, tast NEJ for at afbryde: ");
                string confirmDeletion = Console.ReadLine();



                if (confirmDeletion == "JA") // spørg om man er sikker på sletning på kunde
                {
                    if (accountDeleted > 0) // hvis SQL kommandoen "selAccIDCmd" returnere en værdi, antag at denne værdi er et kontonummer
                    {
                        SqlCommand delTrans = new SqlCommand("DELETE FROM Transactions WHERE AccountID=@accountDeleted", conn); // slet alle transaktioner tilhørende kontonummeret
                        delTrans.Parameters.Add(new SqlParameter("@accountDeleted", accountDeleted));
                        delTrans.ExecuteNonQuery();

                        SqlCommand delAcc = new SqlCommand("DELETE FROM Accounts WHERE AccountID=@accountDeleted", conn); // slet til sidst kontoen
                        delAcc.Parameters.Add(new SqlParameter("@accountDeleted", accountDeleted));
                        delAcc.ExecuteNonQuery();

                        Console.WriteLine("\nKontonummer {0} blev slettet", AccountNo);
                    }
                }
                else if (confirmDeletion == "NEJ")
                {
                    Console.WriteLine("\nDu har afbrudt sletning");
                }
                else
                {
                    Console.WriteLine("\nDu skal enten indtaste JA eller NEJ, (små bogstaver er ikke tilladt)");
                }
            }

            catch
            {
                Console.WriteLine("Konto ikke fundet");
            }
            conn.Close();
        }
    }
}


