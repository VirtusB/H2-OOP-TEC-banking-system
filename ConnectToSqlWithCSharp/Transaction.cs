using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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

            //TODO tekst formattering

            string showTransactionChoice = Console.ReadLine(); // hvis input er et kontonummer, vis tilhørende transaktioner; ellers vis alle transaktioner i databasen

            if (int.TryParse(showTransactionChoice, out int showSpecificTransaction))
            {

                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                SqlCommand cmd = new SqlCommand("SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName FROM Transactions JOIN Accounts ON Accounts.AccountID = Transactions.AccountId JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID WHERE AccountNO=@showSpecificTransaction", conn); // lav en SQL kommando
                cmd.Parameters.Add("@showSpecificTransaction", System.Data.SqlDbType.Int);
                cmd.Parameters["@showSpecificTransaction"].Value = showSpecificTransaction;
                conn.Open(); // åben forbindelsen

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("\nIngen transaktioner fundet for konto {0}", showSpecificTransaction);
                } else
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\nTransactionID: \t\t{0}\nKontonummer: \t\t{1}\nOprettelsesdato: \t{2}\nBeløb: \t\t\t{3:C}\nType: \t\t\t{4}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4)); //udskriv resultaterne
                    }
                }

                

                reader.Close();
                conn.Close();
            }
            else if (showTransactionChoice == "")
            {
                SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                conn.Open(); // åben forbindelsen
                SqlCommand cmd = new SqlCommand("SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName FROM Transactions JOIN Accounts ON Accounts.AccountID = Transactions.AccountId JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID", conn); // lav en SQL kommando
                SqlDataReader reader = cmd.ExecuteReader();

                if(!reader.HasRows)
                {
                    Console.WriteLine("Ingen transaktioner fundet");
                } else
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("TransactionID: \t\t{0}\nKontonummer: \t\t{1}\nOprettelsesdato: \t{2}\nBeløb: \t\t\t{3:C}\nType: \t\t\t{4}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4)); //udskriv resultaterne
                    }
                }

                

                reader.Close();
                conn.Close();
            }

            else if (!int.TryParse(showTransactionChoice, out showSpecificTransaction)) // hvis brugerens indtastning ikke kan parses til int, antag at input ikke er et tal
            {
                Console.WriteLine("\nIndtast et gyldigt kontonummer");
            }
            
        }

        public void AddTransaction()
        {
            Console.WriteLine("Vil du hæve eller indsætte penge?");
            Console.Write("Indtats 1 for at hæve, indtast 2 for at indsætte: ");

            int transUserSelection = Convert.ToInt32(Console.ReadKey().Key);

            

            try
            {


                if (transUserSelection == 49)
                {
                    Console.Write("\n\nIndtast kontonr. på den konto du ønsker at hæve fra: ");
                    int accNo = Convert.ToInt32(Console.ReadLine());

                    SqlConnection conn = VoresServere.WhichServer(Program.Navn);

                 

                    SqlCommand selAccID = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn);

                    selAccID.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    selAccID.Parameters["@accNo"].Value = accNo;
                    conn.Open();

                    int accountId = (Int32)selAccID.ExecuteScalar();

                    Write("Hvor meget vil du hæve fra konto {0}: ", accNo);

                    double amountWithdraw = Convert.ToDouble(ReadLine());
                    
                    // opdater saldo på specifik konto
                    SqlCommand updateSaldoCmd = new SqlCommand("UPDATE Accounts SET Saldo = Saldo - @amountWithdraw WHERE AccountNo=@accNo", conn);

                    updateSaldoCmd.Parameters.Add("@amountWithdraw", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    updateSaldoCmd.Parameters["@amountWithdraw"].Value = amountWithdraw;

                    updateSaldoCmd.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    updateSaldoCmd.Parameters["@accNo"].Value = accNo;

                    // opret transaktion i databasen
                    SqlCommand addTransactionDB = new SqlCommand("INSERT INTO Transactions (AccountId, amount, TransactionTypeId) VALUES (@accountId, -@amountWithdraw, 1)", conn);

                    addTransactionDB.Parameters.Add("@accountId", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    addTransactionDB.Parameters["@accountId"].Value = accountId;

                    addTransactionDB.Parameters.Add("@amountWithdraw", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    addTransactionDB.Parameters["@amountWithdraw"].Value = amountWithdraw;

                    // hent nuværende saldo
                    SqlCommand getCurrentSaldo = new SqlCommand("SELECT Saldo FROM Accounts WHERE AccountNo=@accNo", conn);

                    getCurrentSaldo.Parameters.Add("@accNo", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    getCurrentSaldo.Parameters["@accNo"].Value = accNo;


                    // eksekver queries
                    updateSaldoCmd.ExecuteNonQuery();
                    addTransactionDB.ExecuteNonQuery();
                    double currentSaldo = (double)getCurrentSaldo.ExecuteScalar();


                    conn.Close();

                    Write("\nDu har hævet {0:C} fra kontoen {1}. Din nye saldo er nu {2:C}\n", amountWithdraw, accNo, currentSaldo);
                    Console.WriteLine(Program.linjeFormat);

                }

                else if (transUserSelection == 50)
                {
                    Console.Write("\n\nIndtast kontonr. på den konto du ønsker at indsætte på: ");
                    int accNo = Convert.ToInt32(Console.ReadLine());

                    SqlConnection conn = VoresServere.WhichServer(Program.Navn);



                    SqlCommand selAccID = new SqlCommand("SELECT AccountID FROM Accounts WHERE AccountNo=@accNo", conn);

                    selAccID.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    selAccID.Parameters["@accNo"].Value = accNo;
                    conn.Open();

                    int accountId = (Int32)selAccID.ExecuteScalar();

                    Write("Hvor meget vil du indsætte på konto {0}: ", accNo);

                    double amountDeposit = Convert.ToDouble(ReadLine());

                    // opdater saldo på specifik konto
                    SqlCommand updateSaldoCmd = new SqlCommand("UPDATE Accounts SET Saldo = Saldo + @amountDeposit WHERE AccountNo=@accNo", conn);

                    updateSaldoCmd.Parameters.Add("@amountDeposit", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    updateSaldoCmd.Parameters["@amountDeposit"].Value = amountDeposit;

                    updateSaldoCmd.Parameters.Add("@accNo", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    updateSaldoCmd.Parameters["@accNo"].Value = accNo;

                    // opret transaktion i databasen
                    SqlCommand addTransactionDB = new SqlCommand("INSERT INTO Transactions (AccountId, amount, TransactionTypeId) VALUES (@accountId, @amountDeposit, 2)", conn);

                    addTransactionDB.Parameters.Add("@accountId", System.Data.SqlDbType.Int); // tilføj parameter til vores SQL string
                    addTransactionDB.Parameters["@accountId"].Value = accountId;

                    addTransactionDB.Parameters.Add("@amountDeposit", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    addTransactionDB.Parameters["@amountDeposit"].Value = amountDeposit;

                    // hent nuværende saldo
                    SqlCommand getCurrentSaldo = new SqlCommand("SELECT Saldo FROM Accounts WHERE AccountNo=@accNo", conn);

                    getCurrentSaldo.Parameters.Add("@accNo", System.Data.SqlDbType.Float); // tilføj parameter til vores SQL string
                    getCurrentSaldo.Parameters["@accNo"].Value = accNo;


                    // eksekver queries
                    updateSaldoCmd.ExecuteNonQuery();
                    addTransactionDB.ExecuteNonQuery();
                    double currentSaldo = (double)getCurrentSaldo.ExecuteScalar();


                    conn.Close();

                    Write("\nDu har indsat {0:C} på kontoen {1}. Din nye saldo er nu {2:C}\n", amountDeposit, accNo, currentSaldo);
                    Console.WriteLine(Program.linjeFormat);
                } else
                {
                    //throw new Exception("fejl forkert...");
                    WriteLine("\n\nForkert valg! Indtast 1 eller 2");                 
                }
            }
            catch(Exception e)
            {              
                Console.WriteLine("\nFejl: " + e.Message + "\n(Det indastede tal skal være et gyldigt og eksisterende kontonummer)");
            }          
        }
    }
}
