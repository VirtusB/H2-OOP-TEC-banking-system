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

        #region setters & getters
        private int customerid;
        private string firstname;
        private string lastname;
        private string address;
        private string city;
        private int postalcode;
        

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
#endregion

        public Customer()
        {
            CustomerID = 0;
            FirstName = "Intet fornavn";
            LastName = "Intet efternavn";
            Address = "Ingen adresse";
            City = "Ingen by";
            PostalCode = 0000;
        }

        public void AddCustomer()
        {


            SqlConnection conn = VoresServere.WhichServer(Program.Navn);



            conn.Close();
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
