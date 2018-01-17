using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToSqlWithCSharp
{
    class Transaction
    {
        private int transactionid;
        private int accountid;
        private DateTime created;
        private double amount;
        private int transactiontypeid;


        #region getters & setters
        public int TransactionID
        {
            get
            {
                return transactionid;
            }
            set
            {
                transactionid = value;
            }
        }

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

        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }

        public int TransactionTypeID
        {
            get
            {
                return transactiontypeid;
            }
            set
            {
                transactiontypeid = value;
            }
        }
        #endregion




        public void ShowTransactions()
        {

            

            string showTransactionChoice = Console.ReadLine();



            if (int.TryParse(showTransactionChoice, out int showSpecificTransaction))
            {

                SqlConnection conn = VoresServere.WhichServer(Program.Navn);




                SqlCommand cmd = new SqlCommand("SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName FROM Transactions JOIN Accounts ON Accounts.AccountID = Transactions.AccountId JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID WHERE AccountNO=@showSpecificTransaction", conn); // lav en SQL kommando
                cmd.Parameters.Add("@showSpecificTransaction", System.Data.SqlDbType.Int);
                cmd.Parameters["@showSpecificTransaction"].Value = showSpecificTransaction;
                conn.Open(); // åben forbindelsen

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("AccountID: \t\t{0}\nKontonummer: \t\t{1}\nOprettelsesdato: \t{2}\nBeløb: \t\t\t{3}\nType: \t\t\t{4}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4)); //udskriv resultaterne
                }

                reader.Close();
                conn.Close();
            }
            else
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);


                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName FROM Transactions JOIN Accounts ON Accounts.AccountID = Transactions.AccountId JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("AccountID: \t\t{0}\nKontonummer: \t\t{1}\nOprettelsesdato: \t{2}\nBeløb: \t\t\t{3}\nType: \t\t\t{4}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4)); //udskriv resultaterne
                }

                reader.Close();
                conn.Close();
            }
        }



    }
}
