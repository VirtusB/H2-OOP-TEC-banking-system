﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectToSqlWithCSharp;
using static System.Console;

namespace ConnectToSqlWithCSharp
{
    public class Account
    {
        #region private attributes
        private int accountid;
        private int customerid;
        private DateTime created;
        private int accountno;
        private int accounttypeid;
        private double saldo;
        private bool active;
        private string accounttypename;
        private double interestrate;
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

        public void ShowAccounts()
        {

            Console.WriteLine("Indtast kontonr. for at se en specifik konto");
            Console.WriteLine("Tryk ENTER for at se alle konti");
            Console.WriteLine("Indtast \"overtræk\" for udelukkende at se konti med overtræk");

            string showAccountsChoice = Console.ReadLine(); // hvis int, vis specifik konto ellers vis alle konti

            if (int.TryParse(showAccountsChoice, out int showSpecificAccount)) //hvis showAccountsChoice kan parses til int, vis kontoen tilhørende det indtaste kontonr.
            {

                SqlConnection conn = VoresServere.WhichServer(Program.Navn); // sæt connection string

                SqlCommand cmd = new SqlCommand("SELECT AccountID, CONCAT(firstname,' ', lastname, ' (ID: ', accounts.customerid, ')') as CustomerId, Accounts.Created, AccountNo, AccountTypeName, Saldo, case when Accounts.Active = 1 THEN 'Ja' When Accounts.Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid WHERE AccountNo=@showSpecificAccount", conn); // lav en SQL kommando
                cmd.Parameters.Add("@showSpecificAccount", System.Data.SqlDbType.Int);
                cmd.Parameters["@showSpecificAccount"].Value = showSpecificAccount;
                conn.Open(); // åben forbindelsen

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("\nIngen konto med kontonummer {0} fundet", showAccountsChoice); // udskriv dette hvis der ikke bliver fundet et kontonummer som stemmer overens
                }
                else
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(Program.linjeFormat);
                        Console.WriteLine("\nAccountID: \t\t{0}\nKontoens ejer: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                        double kontoSaldo = (double)reader.GetValue(5);
                        double kontoInterestRate = (double)reader.GetValue(7);
                        double interestAmount = kontoSaldo * kontoInterestRate;
                        double interestTotal = interestAmount + kontoSaldo;
                        if (kontoSaldo > 0 && kontoInterestRate > 0)
                        {
                            Write("Rentebeløb: \t\t{0:C} (+{1:C})\n", interestTotal, interestAmount);
                        }

                        Console.WriteLine("\n" + Program.linjeFormat);

                    }
                }



                reader.Close();
                conn.Close();
            }
            else if (showAccountsChoice == "") // hvis brugeren klikker enter, udskriv alle konti
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT AccountID, CONCAT(firstname,' ',lastname, ' (ID: ', accounts.customerid, ')') as CustomerId, Accounts.Created, AccountNo, AccountTypeName, Saldo, case when Accounts.Active = 1 THEN 'Ja' When Accounts.Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("\n" + Program.linjeFormat);
                while (reader.Read())
                {
                    Console.WriteLine("AccountID: \t\t{0}\nKontoens ejer: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}\n", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                    Console.WriteLine(Program.linjeFormat);
                }
                reader.Close();
                conn.Close();
            }
            else if (showAccountsChoice.ToLower() == "overtræk")
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT AccountID, CONCAT(firstname,' ',lastname, ' (ID: ', accounts.customerid, ')') as CustomerId, Accounts.Created, AccountNo, AccountTypeName, Saldo, case when Accounts.Active = 1 THEN 'Ja' When Accounts.Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid WHERE Saldo < 0", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\n" + Program.linjeFormat);
                while (reader.Read())
                {
                    Console.WriteLine("\nAccountID: \t\t{0}\nKontoens ejer: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}\n", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                    Console.WriteLine(Program.linjeFormat);
                }
                reader.Close();
                conn.Close();
            }
            else if (!int.TryParse(showAccountsChoice, out showSpecificAccount)) // hvis brugerens indtastning ikke kan parses til int, antag at input ikke er et tal
            {
                Console.WriteLine("\nIndtast et gyldigt kontonummer");
            }
        }

        public void PrintAccountInfo(int? count)
        {
            if (count != null)
            {
                Console.WriteLine("\n\tKonto {0}:\n", +1);
            }
            Console.WriteLine("\tKontonummer: \t {0}", AccountNo);
            Console.WriteLine("\tOprettet: \t {0}", Created.ToString("MMMM dd, yyyy").ToUpper());
            Console.WriteLine("\tKontotype: \t {0}", AccountTypeName);
            Console.WriteLine("\tSaldo: \t\t {0:C}", Saldo);
            Console.WriteLine("\tAktiv: \t\t {0}", Active ? "Ja" : "Nej");
            Console.WriteLine("\tRentesats: \t {0:P}", InterestRate);
        }

        public void PrintAccountInfo(Account account, int? count, bool tabulate = false)
        {
            if (count != null)
            {
                Console.WriteLine("\n\tKonto {0}:\n", +1);
            }
            Console.WriteLine("\tKontonummer: \t {0}", account.AccountNo);
            Console.WriteLine("\tOprettet: \t {0}", account.Created.ToString("MMMM dd, yyyy").ToUpper());
            Console.WriteLine("\tKontotype: \t {0}", account.AccountTypeName);
            Console.WriteLine("\tSaldo: \t\t {0:C}", account.Saldo);
            Console.WriteLine("\tAktiv: \t\t {0}", account.Active ? "Ja" : "Nej");
            Console.WriteLine("\tRentesats: \t {0:P}", account.InterestRate);
        }

        public void AddAccount()
        {

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            conn.Open();
            SqlCommand checkCustID = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE CustomerID = @custCheck", conn);
            checkCustID.Parameters.Add(new SqlParameter("@custCheck", customerid));

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
            addAccCMD.Parameters.Add(new SqlParameter("@custID", customerid));
            addAccCMD.Parameters.Add(new SqlParameter("@accTypeId", accTypeID));
            addAccCMD.Parameters.Add(new SqlParameter("@accMax", accMax));
            addAccCMD.ExecuteNonQuery();
            Console.WriteLine("\nDu har oprettet en konto af typen {0}, med kontonummer {1}", accTypeSelection, accMax);

            conn.Close();
        }

        public void DeleteAccount()
        {
            //denne metode sletter en konto

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            // TODO spørg om man er sikker ang. sletning
            // info om hvad man sletter

            SqlCommand selAccID = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn); // hent accountno fra databasen

            selAccID.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            selAccID.Parameters["@accNo"].Value = accountno;
            conn.Open();

            try
            {
                int accountDeleted = (Int32)selAccID.ExecuteScalar();

                Write("\nEr du sikker på at du vil slette kontunummer {0}?\n", accountno);
                Write("Tast JA for at fortsætte, tast NEJ for at afbryde: ");
                string confirmDeletion = Console.ReadLine();



                if (confirmDeletion == "JA") // spørg om man er sikker på sletning på kunde
                {
                    if (accountDeleted > 0) // hvis SQL kommandoen "selAccID" returnere en værdi, antag at denne værdi er et kontonummer
                    {
                        SqlCommand delTrans = new SqlCommand("DELETE FROM Transactions WHERE AccountID=@accountDeleted", conn); // slet alle transaktioner tilhørende kontonummeret

                        delTrans.Parameters.Add("@accountDeleted", System.Data.SqlDbType.Int);
                        delTrans.Parameters["@accountDeleted"].Value = accountDeleted;
                        delTrans.ExecuteNonQuery();

                        SqlCommand delAcc = new SqlCommand("DELETE FROM Accounts WHERE AccountID=@accountDeleted", conn); // slet til sidst kontoen

                        delAcc.Parameters.Add("@accountDeleted", System.Data.SqlDbType.Int);
                        delAcc.Parameters["@accountDeleted"].Value = accountDeleted;
                        delAcc.ExecuteNonQuery();

                        Console.WriteLine("\nKontonummer {0} blev slettet", accountno);
                    }
                }
                else if (confirmDeletion == "NEJ")
                {
                    Console.WriteLine("\nDu har afbrudt sletning");
                } else
                {
                    Console.WriteLine("\nDu skal enten indtaste JA eller NEJ, småt er ikke tilladt");
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


