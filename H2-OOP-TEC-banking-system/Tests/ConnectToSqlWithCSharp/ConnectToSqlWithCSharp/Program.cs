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

        static void DeleteCustomer()
        {
            Console.Write("Indtast customer ID: ");
            int cID = Convert.ToInt32(Console.ReadLine());

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
            }
            else
            {
                Console.WriteLine("Ikke fundet -try enter");
                Console.ReadKey();
            }
            conn.Close();
        }



        static void Main(string[] args)
        {

            string[] menuItems =
             {
                "List all customers",
                "Add customer",
                "Delete customer",
                "Edit customer",
                "Exit"
            };


            var selection = ShowMenu(menuItems);

            if (selection == 1)
            {
                Console.WriteLine("List all customers");
                ShowData();
            }
            else if (selection == 2)
            {
                Console.WriteLine("Add customer");
            }
            else if (selection == 3)
            {
                Console.WriteLine("Delete customer");
                DeleteCustomer();
            }
            else if (selection == 4)
            {
                Console.WriteLine("Edit customer");
            }
            else if (selection == -1)
            {

            }
            else
            {
                Console.WriteLine("Goodbye");
            }








            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }

        static int ShowMenu(string[] menuItems)
        {
            Console.Clear();
            Console.WriteLine("Choose option\n");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ") " + menuItems[i]);
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 5)
            {
                Console.WriteLine("Choose a number between 1-5");
            }
            Console.WriteLine("Selection: " + selection);
            return selection;
        }
    }
}
