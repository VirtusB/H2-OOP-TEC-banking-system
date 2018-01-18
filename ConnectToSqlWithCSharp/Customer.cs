using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ConnectToSqlWithCSharp
{
    public class Customer
    {

        #region setters & getters
        private int customerid;
        private DateTime created;
        private string firstname;
        private string lastname;
        private string address;
        private string city;
        private int postalcode;
        private bool active;


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

        public string FirstName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public int PostalCode
        {
            get
            {
                return postalcode;
            }
            set
            {
                postalcode = value;
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

        public Customer()
        {
            CustomerID = 0;
            Created = DateTime.Now;
            FirstName = "Intet fornavn";
            LastName = "Intet efternavn";
            Address = "Ingen adresse";
            City = "Ingen by";
            PostalCode = 0000;
            Active = false;
        }

        public void ShowCustomer()
        {
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);
            var listOfCustomers = new List<Customer>();
            var listOfAccounts = new List<Account>();
            var customerAccounts = new List<Account>();
            SqlCommand customerCmd = new SqlCommand("SELECT * FROM Customers", conn);
            SqlCommand accountCmd = new SqlCommand("SELECT * FROM Accounts INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId", conn);
            conn.Open();
            SqlDataReader reader = customerCmd.ExecuteReader();
            
            while (reader.Read())
            {

                listOfCustomers.Add(new Customer
                {
                    CustomerID = reader.GetInt32(0),
                    Created = reader.GetDateTime(1),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3),
                    Address = reader.GetString(4),
                    City = reader.GetString(5),
                    PostalCode = reader.GetInt32(6),
                    Active = reader.GetBoolean(7)
                });

            }

            reader.Close(); // luk reader brugt til Customers

            SqlDataReader accountReader = accountCmd.ExecuteReader();

            while (accountReader.Read())
            {
                listOfAccounts.Add(new Account
                {
                    AccountID = accountReader.GetInt32(0),
                    CustomerID = accountReader.GetInt32(1),
                    Created = accountReader.GetDateTime(2),
                    AccountNo = accountReader.GetInt32(3),
                    AccountTypeID = accountReader.GetInt32(4),
                    Saldo = accountReader.GetDouble(5),
                    Active = accountReader.GetBoolean(6),
                    AccountTypeName = accountReader.GetString(8),
                    InterestRate = accountReader.GetDouble(9)
                });

            }
           
            for (int i = 0; i < listOfCustomers.Count(); i++)
            {

                WriteLine("Kunde {0}:\n", i+1);
                Console.WriteLine("Fornavn: \t {0}", listOfCustomers[i].firstname);                              
                Console.WriteLine("Efternavn: \t {0}", listOfCustomers[i].lastname);
                Console.WriteLine("Adresse: \t {0}", listOfCustomers[i].address);
                Console.WriteLine("By: \t\t {0}", listOfCustomers[i].city);
                Console.WriteLine("Postnr: \t {0}", listOfCustomers[i].postalcode);
                Console.WriteLine("Oprettet: \t {0}", listOfCustomers[i].created.ToString("MMMM dd, yyyy").ToUpper() );
                Console.WriteLine("Aktiv: \t\t {0}", listOfCustomers[i].active ? "Ja" : "Nej");
                
                customerAccounts = listOfAccounts.Where(x => x.CustomerID == listOfCustomers[i].CustomerID).ToList();

                Console.WriteLine("\nKonti tilhørende {0} {1}: \n", listOfCustomers[i].firstname, listOfCustomers[i].lastname);
                for (int x = 0; x < customerAccounts.Count(); x++)
                {
                    WriteLine("\nKonto {0}:\n", x+1);
                    //Console.WriteLine("AccountID: \t {0}", customerAccounts[x].AccountID); Ligegyldigt?
                    //Console.WriteLine("CustomerID: \t {0}", customerAccounts[x].CustomerID); Ligegyldigt?
                    Console.WriteLine("Oprettet: \t {0}", customerAccounts[x].Created.ToString("MMMM dd, yyyy").ToUpper());
                    Console.WriteLine("Kontonummer: \t {0}", customerAccounts[x].AccountNo);
                    Console.WriteLine("Kontotype: \t {0}", customerAccounts[x].AccountTypeName);
                    Console.WriteLine("Saldo: \t\t {0:C}", customerAccounts[x].Saldo);
                    Console.WriteLine("Aktiv: \t\t {0}", customerAccounts[x].Active ? "Ja" : "Nej");
                    Console.WriteLine("Rentesats: \t {0:P}", customerAccounts[x].InterestRate);
                }
                Console.WriteLine("\n");
                WriteLine(Program.linjeFormat);
                Console.WriteLine("\n");
            }
            accountReader.Close(); 
            conn.Close();
        }

        public void AddCustomer()
        {

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            SqlCommand cmd = new SqlCommand("INSERT INTO Customers (firstname, lastname, address, city, postalcode) VALUES ('" + firstname + "', '" + lastname + "', '" + address + "', '" + city + "', '" + postalcode + "')", conn);

            conn.Open();

            int customerOprettet = cmd.ExecuteNonQuery();
            if (customerOprettet > 0)
            {
                Console.WriteLine("Oprettet kunden: " + firstname + " " + lastname);

            }
            else
            {
                Console.WriteLine("Fejl i kundeoprettelse");

            }
            conn.Close();
        }

        public void DeleteCustomer()
        {

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            SqlCommand cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID=@cID", conn);

            cmd.Parameters.Add("@cID", System.Data.SqlDbType.Int);
            cmd.Parameters["@cID"].Value = customerid;
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
