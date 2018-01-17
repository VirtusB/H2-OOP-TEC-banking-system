using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToSqlWithCSharp
{
    public class Customer
    {
        public void AddCustomer()
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

        public void DeleteCustomer()
        {
            Console.Write("Indtast customer ID: ");
            int cID = Convert.ToInt32(Console.ReadLine());



            SqlConnection conn = new SqlConnection(); // lav en forbindelse til serveren og databasen
            conn.ConnectionString =
                "Data Source=DESKTOP-TN867E5;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";




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

    }
}
