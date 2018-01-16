using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;


namespace ConnectToSqlWithCSharp
{
    static class Program
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
                Console.WriteLine("AccountID: \t\t{0}\nCustomerId: \t\t{1}\nOprettelsesdato: \t{2}\nKontonummer: \t\t{3}\nKontotype: \t\t{4}\nSaldo: \t\t\t{5}\nKonto aktiv?: \t\t{6}\n", reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3), reader.GetValue(4), reader.GetValue(5), reader.GetValue(6)); //udskriv resultaterne
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
                Console.WriteLine("Slettet");

            }
            else
            {
                Console.WriteLine("Ikke fundet");

            }
            conn.Close();
        }

        static void AddCustomer()
        {
            Console.Write("Indtast fornavn: ");
            string cFornavn = Console.ReadLine();


            Console.Write("\nIndtast efternavn: ");
            string cEfternavn = Console.ReadLine();

            Console.Write("\n Adresse: ");
            string cAdresse = Console.ReadLine();

            Console.Write("\n By: ");
            string cByNavn = Console.ReadLine();

            Console.Write("\n Postnr: ");
            string cPostnr = Console.ReadLine();



            SqlConnection conn = new SqlConnection(); // lav en forbindelse til serveren og databasen
            conn.ConnectionString =
                "Data Source=DESKTOP-TN867E5;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";



            conn.Close();
            SqlCommand cmd = new SqlCommand("INSERT INTO Customers (firstname, lastname, address, city, postalcode) VALUES ('" + cFornavn + "', '" + cEfternavn + "', '" + cAdresse + "', '" + cByNavn + "', '" + cPostnr + "')", conn);


            conn.Open();

            int customerOprettet = cmd.ExecuteNonQuery();
            if (customerOprettet > 0)
            {
                Console.WriteLine("Oprettet kunden: " + cFornavn);

            }
            else
            {
                Console.WriteLine("Fejl i kundeoprettelse");

            }
            conn.Close();
        }

        static void Main(string[] args)
        {

            // Menu start
            bool done = false;
            do
            {
                
                Console.WriteLine("Vælg en mulighed:");
                Console.WriteLine("\t1) Vis kundeoversigt");
                Console.WriteLine("\t2) Tilføj kunde");
                Console.WriteLine("\t3) Slet kunde");
                Console.WriteLine("\t4) Rediger kunde");
                Console.WriteLine("\t5) Tilføj konto til kunde");
                Console.WriteLine("\t6) Slet konto fra kunde");
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
                        ShowData();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Tilføj en kunde");
                        AddCustomer();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Slet en kunde");
                        DeleteCustomer();
                        break;
                    case 4:
                        
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

