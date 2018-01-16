using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ConnectToSqlWithCSharp
{
    class Program
    {

        static void ShowData()
        {
            SqlConnection conn = new SqlConnection(); // lav en forbindelse til serveren og databasen
            conn.ConnectionString =
                "Data Source=DESKTOP-TN867E5;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";


            conn.Open(); // åben forbindelsen
            SqlCommand cmd = new SqlCommand("SELECT AccountID, CustomerId, Created, AccountNo, AccountTypeName, Saldo, Active FROM [dbo].[Accounts] INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId", conn); // lav en SQL kommando
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //

                }
                Console.WriteLine("AccountID: \t{0}\nCustomerId: \t{1}\nCreated: \t{2}\nAccountNo: \t{3}\nKontotype: \t{4}\nSaldo: \t\t{5}\nActive: \t{6}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6)); //udskriv resultaterne
            }

            reader.Close();
            conn.Close();
        }

        static void DeleteCustomer(int cID)
        {
            SqlConnection conn = new SqlConnection(); // lav en forbindelse til serveren og databasen
            conn.ConnectionString =
                "Data Source=DESKTOP-TN867E5;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";


            
            conn.Close();
            SqlCommand cmd = new SqlCommand("DELETE FROM Accounts WHERE CustomerID=@cID", conn);

            cmd.Parameters.Add("@cID", System.Data.SqlDbType.Int);
            cmd.Parameters["@cID"].Value = cID;
            conn.Open();

            int customerSlettet = cmd.ExecuteNonQuery();
            if (customerSlettet > 0)
            {
                Console.WriteLine("Slettet - tryk enter");
                Console.ReadKey();
            } else
            {
                Console.WriteLine("Ikke fundet -try enter");
                Console.ReadKey();
            }
            conn.Close();
        }



        static void Main(string[] args)
        {
            ShowData();

            DeleteCustomer(2);

            if(Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}
