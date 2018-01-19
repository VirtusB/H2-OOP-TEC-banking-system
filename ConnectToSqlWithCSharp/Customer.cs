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
            Console.WriteLine("Indtast navn for at se en specifik kunde");
            Console.WriteLine("Tryk ENTER for at se en komplet kundeoversigt");
            Console.WriteLine("(Tilføj -knr for at sortere efter konro-nr)");

            string showCustomerChoice = Console.ReadLine();
            bool showAll = false;
            bool knrSorting = false;

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            var listOfCustomers = new List<Customer>(); // liste af kunder
            var listOfAccounts = new List<Account>(); // liste af konti
            var customerAccounts = new List<Account>(); // liste af konti som tilhører den pågældende kunde

            SqlCommand customerCmd = new SqlCommand();
            SqlCommand accountCmd = new SqlCommand();
            customerCmd.Connection = conn;
            accountCmd.Connection = conn;
            if (showCustomerChoice == "")
            {
                //TODO: Add sorting if knrSorting is true (if -knr is written)
                //TODO: Mode code that is duplicated into private method
                customerCmd.CommandText = "SELECT * FROM Customers";
                accountCmd.CommandText = "SELECT * FROM Accounts INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId";
                conn.Open();
                SqlDataReader customerReader = customerCmd.ExecuteReader();

                while (customerReader.Read())
                {
                    listOfCustomers.Add(new Customer
                    {
                        CustomerID = customerReader.GetInt32(0),
                        Created = customerReader.GetDateTime(1),
                        FirstName = customerReader.GetString(2),
                        LastName = customerReader.GetString(3),
                        Address = customerReader.GetString(4),
                        City = customerReader.GetString(5),
                        PostalCode = customerReader.GetInt32(6),
                        Active = customerReader.GetBoolean(7)
                    });
                }
                customerReader.Close(); // luk reader brugt til Customers

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
                accountReader.Close();

                for (int i = 0; i < listOfCustomers.Count(); i++)
                {
                    
                    WriteLine("Kunde {0}:\n", listOfCustomers[i].CustomerID);
                    Console.WriteLine("Fornavn: \t {0}", listOfCustomers[i].firstname);
                    Console.WriteLine("Efternavn: \t {0}", listOfCustomers[i].lastname);
                    Console.WriteLine("Adresse: \t {0}", listOfCustomers[i].address);
                    Console.WriteLine("By: \t\t {0}", listOfCustomers[i].city);
                    Console.WriteLine("Postnr: \t {0}", listOfCustomers[i].postalcode);
                    Console.WriteLine("Oprettet: \t {0}", listOfCustomers[i].created.ToString("MMMM dd, yyyy").ToUpper());
                    Console.WriteLine("Aktiv: \t\t {0}", listOfCustomers[i].active ? "Ja" : "Nej");

                    customerAccounts = listOfAccounts.Where(x => x.CustomerID == listOfCustomers[i].CustomerID).ToList();
                    Console.WriteLine("\nKonti tilhørende {0} {1}: ", listOfCustomers[i].firstname, listOfCustomers[i].lastname);
                    for (int x = 0; x < customerAccounts.Count(); x++)
                    {
                        WriteLine("\n\tKonto {0}:\n", x + 1);
                        //Console.WriteLine("AccountID: \t {0}", customerAccounts[x].AccountID); Ligegyldigt?
                        //Console.WriteLine("CustomerID: \t {0}", customerAccounts[x].CustomerID); Ligegyldigt?
                        Console.WriteLine("\tOprettet: \t {0}", customerAccounts[x].Created.ToString("MMMM dd, yyyy").ToUpper());
                        Console.WriteLine("\tKontonummer: \t {0}", customerAccounts[x].AccountNo);
                        Console.WriteLine("\tKontotype: \t {0}", customerAccounts[x].AccountTypeName);
                        Console.WriteLine("\tSaldo: \t\t {0:C}", customerAccounts[x].Saldo);
                        Console.WriteLine("\tAktiv: \t\t {0}", customerAccounts[x].Active ? "Ja" : "Nej");
                        Console.WriteLine("\tRentesats: \t {0:P}", customerAccounts[x].InterestRate);
                    }
                    Console.WriteLine("\n");
                    WriteLine(Program.linjeFormat);
                    Console.WriteLine("\n");
                }
                conn.Close();
            }
            else
            {
                


                    //Build customers query with WHERE statement as parameter
                    customerCmd.CommandText = "SELECT * FROM Customers as c " +
                                              "WHERE CONCAT(c.firstname,' ',c.lastname,' ',c.address, ' ', c.City, ' ', c.PostalCode) LIKE '%' + @showCustomerChoice + '%'";

                    customerCmd.Parameters.Add(new SqlParameter("@showCustomerChoice", showCustomerChoice)); // tilføj parameter til vores SQL string

                    //TODO: The old code below does the same as the above. We should consider if other places has code like this that could be changed to the above? 50% reduction
                    //customerCmd.Parameters.Add("@showCustomerChoice", System.Data.SqlDbType.NVarChar);
                    //customerCmd.Parameters["@showCustomerChoice"].Value = showCustomerChoice;
                    //accountCmd.Parameters.Add("@showCustomerChoice", System.Data.SqlDbType.NVarChar); // tilføj parameter til vores SQL string
                    //accountCmd.Parameters["@showCustomerChoice"].Value = showCustomerChoice;

                    //Add all matching customers to listOfCustomers
                    conn.Open();
                    SqlDataReader customerReader = customerCmd.ExecuteReader();
                    while (customerReader.Read())
                    {
                        listOfCustomers.Add(new Customer
                        {
                            CustomerID = customerReader.GetInt32(0),
                            Created = customerReader.GetDateTime(1),
                            FirstName = customerReader.GetString(2),
                            LastName = customerReader.GetString(3),
                            Address = customerReader.GetString(4),
                            City = customerReader.GetString(5),
                            PostalCode = customerReader.GetInt32(6),
                            Active = customerReader.GetBoolean(7)
                        });
                    }
                    customerReader.Close(); // luk reader brugt til Customers
                do
                {
                    //if (listOfCustomers.Count() > 1)
                    while (listOfCustomers.Count() > 1 && showAll == false)
                    {
                        //knrSorting = false;
                        Console.WriteLine("\nFlere kunder blev fundet. " +
                            "\nIndtast kunde-ID eller et mere præcist navn, for at se data om specifik kunde." +
                            "\nTryk enter for at se data om alle kunder fra denne liste." +
                            "\n(Tilføj -knr for at sortere efter konro-nr)");

                        Console.WriteLine("\n");
                        Console.WriteLine("Kunde-ID\tNavn\tAdresse");
                        foreach (var customer in listOfCustomers)
                        {
                            Console.WriteLine("{0}\t{1}\t{2}",
                                customer.CustomerID.ToString(),
                                customer.firstname + " " + customer.lastname,
                                customer.address + " " + customer.city + " " + customer.postalcode);
                        }
                        
                        showCustomerChoice = Console.ReadLine();
                        if (IsDigitsOnly(showCustomerChoice))
                        {
                            listOfCustomers.RemoveAll(x => x.CustomerID.ToString() != showCustomerChoice);
                        }
                        else if (showCustomerChoice == "")
                        {
                            showAll = true;
                        }
                        else
                        {
                            foreach (var customer in listOfCustomers)
                            {
                                if (customer.firstname.Contains(showCustomerChoice) || 
                                    customer.lastname.Contains(showCustomerChoice) ||
                                    customer.address.Contains(showCustomerChoice) ||
                                    customer.city.Contains(showCustomerChoice) ||
                                    customer.postalcode.ToString().Contains(showCustomerChoice))
                                {
                                    listOfCustomers.Remove(customer);
                                }
                            }
                        }
                    }
                } while (listOfCustomers.Count() > 1 || showAll == true);

                //Build accounts query based on returned customerIds. 
                //1. First build sql
                accountCmd.CommandText = "SELECT a.AccountID, a.CustomerId, a.Created, a.AccountNo, a.AccountTypeId, " +
                                            "a.Saldo, a.Active, at.AccountTypeID, at.AccountTypeName, at.Interestrate " +
                                            "FROM Accounts as a " +
                                            "INNER JOIN AccountTypes as at ON a.AccountTypeId = at.AccountTypeID " +
                                            "WHERE a.CustomerId IN (@customerIdSearchMatches)";

                //2. Then build @customerIdSearchMatches (list of integers, (seperated by comma if multiple))
                string stringWithCustomerIds = "";
                if (listOfCustomers.Count > 1)
                {
                    foreach (int customerid in listOfCustomers.Select(x => x.CustomerID))
                    {
                        stringWithCustomerIds += customerid + ",";
                    }
                    stringWithCustomerIds.Remove(stringWithCustomerIds.Length - 1);
                }
                else if (listOfCustomers.Count == 1)
                {
                    stringWithCustomerIds = listOfCustomers[0].CustomerID.ToString();
                }
                else
                    throw new Exception("WOWOWOWOW");

                accountCmd.Parameters.Add(new SqlParameter("@customerIdSearchMatches", stringWithCustomerIds)); // tilføj parameter til vores SQL string

                //Now get all the relevant accounts into listOfAccounts
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
                accountReader.Close();

                //Now show the accounts.
                for (int i = 0; i < listOfCustomers.Count(); i++)
                {
                    WriteLine("Kunde {0}:\n", i + 1);
                    Console.WriteLine("Fornavn: \t {0}", listOfCustomers[i].firstname);
                    Console.WriteLine("Efternavn: \t {0}", listOfCustomers[i].lastname);
                    Console.WriteLine("Adresse: \t {0}", listOfCustomers[i].address);
                    Console.WriteLine("By: \t\t {0}", listOfCustomers[i].city);
                    Console.WriteLine("Postnr: \t {0}", listOfCustomers[i].postalcode);
                    Console.WriteLine("Oprettet: \t {0}", listOfCustomers[i].created.ToString("MMMM dd, yyyy").ToUpper());
                    Console.WriteLine("Aktiv: \t\t {0}", listOfCustomers[i].active ? "Ja" : "Nej");

                    customerAccounts = listOfAccounts.Where(x => x.CustomerID == listOfCustomers[i].CustomerID).ToList();
                    Console.WriteLine("\nKonti tilhørende {0} {1}: \n", listOfCustomers[i].firstname, listOfCustomers[i].lastname);

                    for (int x = 0; x < customerAccounts.Count(); x++)
                    {
                        WriteLine("\nKonto {0}:\n", x + 1);
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
        }
        //IsNumeric check-method
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
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


            try
            {
                
            }
            catch
            {

            }


            conn.Close();
        }
    }
}
