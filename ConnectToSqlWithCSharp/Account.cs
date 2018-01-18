using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectToSqlWithCSharp;


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

                SqlCommand cmd = new SqlCommand("SELECT AccountID, CustomerId, Created, AccountNo, AccountTypeName, Saldo, case when Active = 1 THEN 'Ja' When Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId WHERE AccountNo=@showSpecificAccount", conn); // lav en SQL kommando
                cmd.Parameters.Add("@showSpecificAccount", System.Data.SqlDbType.Int);
                cmd.Parameters["@showSpecificAccount"].Value = showSpecificAccount;
                conn.Open(); // åben forbindelsen

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("\nAccountID: \t\t{0}\nCustomerId: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                }

                reader.Close();
                conn.Close();
            }
            else
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT AccountID, CustomerId, Created, AccountNo, AccountTypeName, Saldo, case when Active = 1 THEN 'Ja' When Active = 0 THEN 'Nej' ELSE 'ERROR' end as Aktiv, InterestRate FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("AccountID: \t\t{0}\nCustomerId: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5:C}\nKonto aktiv: \t\t{6}\nRentesats: \t\t{7:P}\n", reader.GetValue(0), reader.GetValue(1), Convert.ToDateTime(reader.GetValue(2)).ToString("MMMM dd, yyyy").ToUpper(), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6), reader.GetValue(7)); //udskriv resultaterne
                }
                //mangler dato formattering
                reader.Close();
                conn.Close();
            }
        }
        

        public void DeleteAccount()
        {
            Console.Write("Indtast kontonr: ");
            int accNo = Convert.ToInt32(Console.ReadLine());

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            //TODO error checks

            SqlCommand selAccID = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn);

            selAccID.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
            selAccID.Parameters["@accNo"].Value = accNo;
            conn.Open();
            SqlDataReader reader = selAccID.ExecuteReader();

            int accountDeleted = reader.GetInt32(0);

            if (accountDeleted > 0)
            {
                SqlCommand delTrans = new SqlCommand("DELETE FROM Transactions WHERE AccountID = @accountDeleted", conn);

                delTrans.Parameters.Add("@accountDeleted", System.Data.SqlDbType.Int);
                delTrans.Parameters["@accountDeleted"].Value = accNo;
                conn.Open();

                Console.WriteLine("Slettet");

            }
            else
            {
                Console.WriteLine("Ikke fundet");

            }
            reader.Close();
            conn.Close();
        }
    }
}


