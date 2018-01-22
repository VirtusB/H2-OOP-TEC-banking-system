using System;
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
        private int accountid;
        private int customerid;
        private DateTime created;
        private int accountno;
        private int accounttypeid;
        private double saldo;
        private bool active;
        private string accounttypename;
        private double interestrate;

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

            string showAccountsChoice = Console.ReadLine(); // hvis int, vis specifik konto ellers vis alle konti

            if (int.TryParse(showAccountsChoice, out int showSpecificAccount)) //hvis showAccountsChoice kan parses til int, vis kontoen tilhørende det indtaste kontonr.
            {

                SqlConnection conn = VoresServere.WhichServer(Program.Navn); // sæt connection string

                SqlCommand cmd = new SqlCommand("SELECT AccountID, CONCAT(firstname,' ', lastname, ' (ID: ', accounts.customerid, ')') as CustomerId, Accounts.Created, AccountNo, AccountTypeName, Saldo, case when Accounts.Active = 1 THEN 'Ja' When Accounts.Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid WHERE AccountNo=@showSpecificAccount", conn); // lav en SQL kommando
                cmd.Parameters.Add("@showSpecificAccount", System.Data.SqlDbType.Int);
                cmd.Parameters["@showSpecificAccount"].Value = showSpecificAccount;
                conn.Open(); // åben forbindelsen

                SqlDataReader reader = cmd.ExecuteReader();

                if(!reader.HasRows)
                {      
                    Console.WriteLine("\nIngen konto med kontonummer {0} fundet", showAccountsChoice); // udskriv dette hvis der ikke bliver fundet et kontonummer som stemmer overens
                } else
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\nAccountID: \t\t{0}\nKontoens ejer: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                    }
                }

                

                reader.Close();
                conn.Close();
            } else if (showAccountsChoice == "") // hvis brugeren klikker enter, udskriv alle konti
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT AccountID, CONCAT(firstname,' ',lastname, ' (ID: ', accounts.customerid, ')') as CustomerId, Accounts.Created, AccountNo, AccountTypeName, Saldo, case when Accounts.Active = 1 THEN 'Ja' When Accounts.Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId INNER JOIN customers ON accounts.customerid = customers.customerid", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("AccountID: \t\t{0}\nKontoens ejer: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}\n", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                }
                //mangler dato formattering
                reader.Close();
                conn.Close();
            } else if (!int.TryParse(showAccountsChoice, out showSpecificAccount)) // hvis brugerens indtastning ikke kan parses til int, antag at input ikke er et tal
            {
                Console.WriteLine("\nIndtast et gyldigt kontonummer");
            }         
        }

        public void AddAccount()
        {
         
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            conn.Open();
            SqlCommand checkCustID = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE CustomerID = @custCheck", conn);
            checkCustID.Parameters.Add("@custCheck", System.Data.SqlDbType.NVarChar); // tilføj parameter til vores SQL string
            checkCustID.Parameters["@custCheck"].Value = customerid;


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

            SqlCommand selAccType = new SqlCommand("SELECT AccountTypeID FROM AccountTypes WHERE AccountTypeName=@accTypeSelection", conn);
            selAccType.Parameters.Add("@accTypeSelection", System.Data.SqlDbType.NVarChar); // tilføj parameter til vores SQL string
            selAccType.Parameters["@accTypeSelection"].Value = accTypeSelection;
            int accTypeID = (int)selAccType.ExecuteScalar();

            SqlCommand selAccMax = new SqlCommand("SELECT TOP (1) [AccountNo] FROM [dbo].[Accounts] ORDER BY [AccountNo] DESC", conn);
           
            int? accMax = (int?)selAccMax.ExecuteScalar() + 1; // fejler hvis der ikke eksisterer nogen konti i forvejen

            if (accMax == null)
            {
                accMax = 1000;
            } 


            SqlCommand addAccCMD = new SqlCommand("INSERT INTO Accounts (CustomerID, AccountNo, AccountTypeId) VALUES (@custID, @accMax, @accTypeID)", conn);
            addAccCMD.Parameters.Add("@custID", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            addAccCMD.Parameters["@custID"].Value = customerid;

            addAccCMD.Parameters.Add("@accTypeID", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            addAccCMD.Parameters["@accTypeID"].Value = accTypeID;

            addAccCMD.Parameters.Add("@accMax", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            addAccCMD.Parameters["@accMax"].Value = accMax;
            
            addAccCMD.ExecuteNonQuery();

            Console.WriteLine("\nDu har oprettet en {0}, med kontonummer {1}", accTypeSelection, accMax);

            conn.Close();



        }
        
        public void DeleteAccount()
        {
            

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            //TODO error checks
            // TODO spørg om mange er sikker
            // info om hvad man sletter

            SqlCommand selAccID = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn);

            selAccID.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            selAccID.Parameters["@accNo"].Value = accountno;
            conn.Open();
            try
            {
                
                int accountDeleted = (Int32)selAccID.ExecuteScalar();

                if (accountDeleted > 0)
                {
                    SqlCommand delTrans = new SqlCommand("DELETE FROM Transactions WHERE AccountID=@accountDeleted", conn);

                    delTrans.Parameters.Add("@accountDeleted", System.Data.SqlDbType.Int);
                    delTrans.Parameters["@accountDeleted"].Value = accountDeleted;
                    delTrans.ExecuteNonQuery();

                    SqlCommand delAcc = new SqlCommand("DELETE FROM Accounts WHERE AccountID=@accountDeleted", conn);

                    delAcc.Parameters.Add("@accountDeleted", System.Data.SqlDbType.Int);
                    delAcc.Parameters["@accountDeleted"].Value = accountDeleted;
                    delAcc.ExecuteNonQuery();

                    Console.WriteLine("Kontonummer {0} blev slettet", accountno);
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


