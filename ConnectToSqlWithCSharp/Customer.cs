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
        #region private attributes
        private int customerid;
        private DateTime created;
        private string firstname;
        private string lastname;
        private string address;
        private string city;
        private int postalcode;
        private bool active;
        #endregion

        #region getters and setters
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
            // denne metode viser enten alle kunder eller en specifik kunde
            // standard soreteing er efter efternavn, -knr kan tilføjes i indtastningen hvilket resulterer i at der bliver sorteret efter kontonummer

            Console.WriteLine("Indtast navn for at se en specifik kunde");
            Console.WriteLine("Tryk ENTER for at se en komplet kundeoversigt");
            Console.WriteLine("(Tilføj -knr for at sortere efter konto-nr)");

            string showCustomerChoice = Console.ReadLine();
            Console.Clear();
            bool showAll = false;
            bool knrSorting = (showCustomerChoice.Contains("-knr"));
            int accountCounter = 1;
            Customer customerForKnrSorting = new Customer();
            bool customerChangedOnIteration = false;
            
            showCustomerChoice = showCustomerChoice.Replace("-knr", "");
            showCustomerChoice = showCustomerChoice.Trim();

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            var listOfCustomers = new List<Customer>(); // liste af kunder
            var listOfAccounts = new List<Account>(); // liste af konti
            var customerAccounts = new List<Account>(); // liste af konti som tilhører den pågældende kunde

            SqlCommand customerCmd = new SqlCommand();
            SqlCommand accountCmd = new SqlCommand();
            customerCmd.Connection = conn;
            accountCmd.Connection = conn;
            conn.Open();

            //Empty - show all
            if (showCustomerChoice == "")
            {
                //TODO: Move code that is duplicated into private method - Now done for printing, readers are a bit more tricky 
                customerCmd.CommandText = "SELECT * FROM Customers";
                accountCmd.CommandText = "SELECT * FROM Accounts INNER JOIN [dbo].[AccountTypes] ON Accounts.AccountTypeID = AccountTypes.AccountTypeId";
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
            }
            //Input contains searchstring:
            else
            {
                //Build customers query with WHERE statement as parameter
                customerCmd.CommandText = "SELECT * FROM Customers as c " +
                                          "WHERE CONCAT(c.firstname,' ',c.lastname,' ',c.address, ' ', c.City, ' ', c.PostalCode) LIKE '%' + @showCustomerChoice + '%'";

                customerCmd.Parameters.Add(new SqlParameter("@showCustomerChoice", showCustomerChoice)); // tilføj parameter til vores SQL string

                //Add all matching customers to listOfCustomers
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

                //Loop for making search more and more specific
                while (listOfCustomers.Count() > 1 && showAll == false)
                {
                    //knrSorting = false;
                    Console.WriteLine("\nFlere kunder blev fundet. " +
                        "\nIndtast kunde-ID eller et mere præcist navn, for at se data om specifik kunde." +
                        "\nTryk enter for at se data om alle kunder fra denne liste." +
                        "\n(Tilføj -knr for at sortere efter konto-nr)");

                    Console.WriteLine("\n");
                    Console.WriteLine("Kunde-ID\tNavn\t\t\tAdresse");
                    foreach (var customer in listOfCustomers)
                    {
                        Console.WriteLine("{0}\t\t{1}\t\t{2}",
                            customer.CustomerID.ToString(),
                            customer.FirstName + " " + customer.LastName,
                            customer.Address + " " + customer.City + " " + customer.PostalCode);
                    }

                    Write("\nIndtastning: ");
                    showCustomerChoice = Console.ReadLine();
                    knrSorting = (showCustomerChoice.Contains("-knr"));

                    showCustomerChoice = showCustomerChoice.Replace("-knr", "");
                    showCustomerChoice = showCustomerChoice.Trim();

                    WriteLine("\n" + Program.linjeFormat);

                    if (showCustomerChoice != "" && IsDigitsOnly(showCustomerChoice))
                    {
                        listOfCustomers.RemoveAll(x => x.CustomerID.ToString() != showCustomerChoice);
                    }
                    else if (showCustomerChoice == "")
                    {
                        showAll = true;
                    }
                    else
                    {
                        foreach (var customer in listOfCustomers.ToList())
                        {
                            //If all fields concatenated with spaces doesn't contain the search string, remove it from the listOfCustomers
                            if (!(customer.FirstName.ToLower() + " " +
                                customer.LastName.ToLower() + " " +
                                customer.Address.ToLower() + " " +
                                customer.City.ToLower() + " " +
                                customer.PostalCode.ToString().ToLower()).Contains(showCustomerChoice.ToLower()))
                            {
                                listOfCustomers.Remove(customer);
                            }
                        }
                    }
                }

                //Build accounts query based on returned customerIds. 
                //1. First build @customerIdSearchMatches (list of integers, (seperated by comma if multiple))
                try
                {
                    string stringWithCustomerIds = "";
                    if (listOfCustomers.Count > 1)
                    {
                        foreach (int customerid in listOfCustomers.Select(x => x.CustomerID))
                        {
                            stringWithCustomerIds += customerid + ",";
                        }
                        stringWithCustomerIds = stringWithCustomerIds.Remove(stringWithCustomerIds.Length - 1);
                    }
                    else if (listOfCustomers.Count == 1)
                    {
                        stringWithCustomerIds = listOfCustomers[0].CustomerID.ToString();
                    }
                    else
                    {
                        //TODO: If we make our own exceptions we can perhaps change this.
                        throw new Exception("\nListen indeholder ikke det du har søgt på. Forfra!");
                    }

                    //2. Then build sql
                    accountCmd.CommandText = "SELECT a.AccountID, a.CustomerId, a.Created, a.AccountNo, a.AccountTypeId, " +
                                                "a.Saldo, a.Active, at.AccountTypeID, at.AccountTypeName, at.Interestrate " +
                                                "FROM Accounts as a " +
                                                "INNER JOIN AccountTypes as at ON a.AccountTypeId = at.AccountTypeID " +
                                                "WHERE a.CustomerId IN (" + stringWithCustomerIds + ")";

                    //Using parameter.Add didn't work, and google informed that using SqlParameters for WHERE xx IN (...) is not the way to go. Therefore we just added the string as a variable in the command...
                    //We could also have made a method for creating a temporary table in the database, and then added a JOIN to that, but that method is a bit too comprehensive; 

                    //we only need it here, and we also expect the user to be good at searching.

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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //SORT IT
            if (knrSorting)
            {
                listOfAccounts.Sort((x, y) => x.AccountNo.CompareTo(y.AccountNo));
            }
            else
            {
                listOfCustomers.Sort((x, y) => x.LastName.CompareTo(y.LastName));
                listOfAccounts.Sort((x, y) => x.AccountNo.CompareTo(y.AccountNo));
            }

            //PRINT IT
            //IF KnrSorting is true, sorty by account number. For this a different logic is needed, in case two customers have account numbers between eachother. 
            //FX Customer 1 might have account 1 and 3 and Customer 2 have account 2 and 4. 
            //we made it so accounts are definately controlling the order, and customernames will appear twice in this case.
            if (knrSorting)
            {
                //Run through list of accounts
                for (int i = 0; i < listOfAccounts.Count(); i++)
                {
                    //On every iteration, check if customer is the same as last iteration. 
                    if (customerForKnrSorting.CustomerID != listOfAccounts[i].CustomerID)
                    {
                        customerChangedOnIteration = true;
                        if (listOfCustomers.Where(x => x.CustomerID == listOfAccounts[i].CustomerID).Count() == 1)
                        {
                            //Save current customer in a variable. (Take him from listofcustomers to get him as an object, so we can print all his info)
                            customerForKnrSorting = listOfCustomers.Where(x => x.CustomerID == listOfAccounts[i].CustomerID).First();
                            //Add 1 to the counter on top of every account-list (cosmetic feature)
                            accountCounter = 1;
                            //Print the new customers info
                            customerForKnrSorting.PrintCustomerInfo();
                        }
                        //This should not be possible:
                        else
                        {
                            throw new Exception("Fejl - flere kunder er tilknyttet samme konto... (Konto-nr: " + listOfAccounts[i].AccountNo + ")");
                        }
                    }
                    else
                    {
                        //Cosmetic feature
                        accountCounter++;
                    }

                    if (customerChangedOnIteration)
                        Console.WriteLine("\nKonti tilhørende {0} {1}: ", customerForKnrSorting.FirstName, customerForKnrSorting.LastName);

                    listOfAccounts[i].PrintAccountInfo(false, accountCounter, true);

                    //Only run the check if at least one iteration is left to prevent error on last iteration
                    if (i + 1 < listOfAccounts.Count())
                    {
                        if (listOfAccounts[i + 1].CustomerID != customerForKnrSorting.CustomerID)
                        {
                            Console.WriteLine("");
                            WriteLine(Program.linjeFormat);
                        }
                    }
                    customerChangedOnIteration = false;
                }
            }
            //Normal sorting
            else
            {
                for (int i = 0; i < listOfCustomers.Count(); i++)
                {
                    listOfCustomers[i].PrintCustomerInfo();

                    customerAccounts = listOfAccounts.Where(x => x.CustomerID == listOfCustomers[i].CustomerID).ToList();
                    Console.WriteLine("\nKonti tilhørende {0} {1}: ", listOfCustomers[i].FirstName, listOfCustomers[i].LastName);
                    for (int x = 0; x < customerAccounts.Count(); x++)
                    {
                        customerAccounts[x].PrintAccountInfo(false, i + 1, true);
                    }
                    Console.WriteLine("\n");
                    WriteLine(Program.linjeFormat);
                }
            }
            conn.Close();
            Console.WriteLine("\nTryk på en vilkårlig tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey(true);
            Console.Clear();
        }

        public void PrintCustomerInfo()
        {
            //Console.WriteLine("\nKunde-ID {0}:\n", CustomerID);
            Console.WriteLine("\nNavn: \t {0} {1} (ID: {2})", FirstName, LastName, CustomerID);
            Console.WriteLine("Adresse: \t {0}", Address);
            Console.WriteLine("By: \t\t {0}", City);
            Console.WriteLine("Postnr: \t {0}", PostalCode);
            Console.WriteLine("Oprettet: \t {0}", Created.ToString("MMMM dd, yyyy").ToUpper());
            Console.WriteLine("Aktiv: \t\t {0}", Active ? "Ja" : "Nej");
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
            //denne metode tilføjer en kunde

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);
            SqlCommand cmd = new SqlCommand("INSERT INTO Customers (firstname, lastname, address, city, postalcode) VALUES ('" + firstname + "', '" + lastname + "', '" + address + "', '" + city + "', '" + postalcode + "')", conn);

            conn.Open();
            int customerOprettet = cmd.ExecuteNonQuery();
            if (customerOprettet > 0)
            {
                Console.WriteLine("\nOprettet kunden: " + firstname + " " + lastname);
            }
            else
            {
                Console.WriteLine("Fejl i kundeoprettelse");
            }
            conn.Close();
        }

        public void DeleteCustomer()
        {
            // denne metode sletter en kunde
            // slet transactions, konti og kunden ud fra customer.customerid
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);
            SqlCommand delCust = new SqlCommand(@"DELETE FROM Transactions WHERE AccountID IN(SELECT DISTINCT AccountID FROM Accounts WHERE accounts.CustomerId=@cID); 
                                                  DELETE FROM Accounts WHERE Accounts.customerId = @cID;
                                                  DELETE FROM Customers where CustomerID = @cID;", conn);
            delCust.Parameters.Add("@cID", System.Data.SqlDbType.Int);
            delCust.Parameters["@cID"].Value = customerid;

            conn.Open();

            Write("\nEr du sikker på at du vil slette kundenummer {0}?\n", customerid);
            Write("Tast JA for at fortsætte, tast NEJ for at afbryde: ");
            string confirmDeletion = Console.ReadLine();
            


            if (confirmDeletion == "JA") // spørg om man er sikker på sletning på kunde
            {
                int custDeletion = (Int32)delCust.ExecuteNonQuery();
                if (custDeletion > 0)
                {
                    Console.WriteLine("\nDu har slettet kundenummer {0}", customerid);
                }
                else
                {
                    Console.WriteLine("\nKunde med kundenummer {0} ikke fundet", customerid);
                }
            } else if (confirmDeletion == "NEJ")
            {
                Console.WriteLine("\nDu har afbrudt sletning");
            }
            else
            {
                Console.WriteLine("\nDu skal enten indtaste JA eller NEJ, småt er ikke tilladt");
            }



            conn.Close();
        }

        public void EditCustomer()
        {
            //denne metode redigerer en kunde
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);
            conn.Open();

            //sqlcommand til fornavn
            SqlCommand editFirstnameCmd = new SqlCommand("UPDATE Customers SET FirstName = @firstName WHERE CustomerID = @custID", conn);
            editFirstnameCmd.Parameters.Add(new SqlParameter("@firstName", firstname));
            editFirstnameCmd.Parameters.Add(new SqlParameter("@custID", customerid));

            //sqlcommand til efternavn
            SqlCommand editLastnameCmd = new SqlCommand("UPDATE Customers SET LastName = @lastName WHERE CustomerID = @custID", conn);
            editLastnameCmd.Parameters.Add(new SqlParameter("@lastName", lastname));
            editLastnameCmd.Parameters.Add(new SqlParameter("@custID", customerid));

            //sqlcommand til adresse
            SqlCommand editAddressCmd = new SqlCommand("UPDATE Customers SET Address = @address WHERE CustomerID = @custID", conn);
            editAddressCmd.Parameters.Add(new SqlParameter("@address", address));
            editAddressCmd.Parameters.Add(new SqlParameter("@custID", customerid));

            //sqlcommand til by
            SqlCommand editCityCmd = new SqlCommand("UPDATE Customers SET City = @city WHERE CustomerID = @custID", conn);
            editCityCmd.Parameters.Add(new SqlParameter("@city", city));
            editCityCmd.Parameters.Add(new SqlParameter("@custID", customerid));

            //sqlcommand til postnr
            SqlCommand editPostalCodeCmd = new SqlCommand("UPDATE Customers SET PostalCode = @postalCode WHERE CustomerID = @custID", conn);
            editPostalCodeCmd.Parameters.Add(new SqlParameter("@postalCode", postalcode));
            editPostalCodeCmd.Parameters.Add(new SqlParameter("@custID", customerid));

            #region error checks og udfør commands
            if (Program.valueToEdit == 1)
            {
                if (firstname.Length >= 2 && !double.TryParse(firstname, out double tempCustFName) && tempCustFName != 1)
                {
                    int firstNameEdited = editFirstnameCmd.ExecuteNonQuery();
                    if (firstNameEdited > 0)
                    {
                        Console.WriteLine("\nSuccess. Fornavn opdateret til {0}", firstname);
                    }
                } else
                {
                    Console.WriteLine("\nIndtast et gyldigt fornavn. Ingen tal og mindst 2 karakterer");
                }
                

            }
            else if (Program.valueToEdit == 2)
            {
                if (lastname.Length >= 2 && !double.TryParse(lastname, out double tempCustLName) && tempCustLName != 1)
                {
                    int lastNameEdited = editLastnameCmd.ExecuteNonQuery();
                    if (lastNameEdited > 0)
                    {
                        Console.WriteLine("\nSuccess. Efternavn opdateret til {0}", lastname);
                    }
                } else
                {
                    Console.WriteLine("\nIndtast et gyldigt efternavn. Ingen tal og mindst 2 karakterer");
                }
                
            }
            else if (Program.valueToEdit == 3)
            {
                if (address.Length >= 4 && !double.TryParse(address, out double tempCustAddr) && tempCustAddr != 1)
                {
                    int addressEdited = editAddressCmd.ExecuteNonQuery();
                    if (addressEdited > 0)
                    {
                        Console.WriteLine("\nSuccess. Adresse opdateret til {0}", address);
                    }
                } else
                {
                    Console.WriteLine("\nIndtast en gyldig adresse. Mindst 4 karakterer");
                }
                
            }
            else if (Program.valueToEdit == 4)
            {
                if (city.Length >= 2 && !double.TryParse(city, out double tempCustCity) && tempCustCity != 1)
                {
                    int cityEdited = editCityCmd.ExecuteNonQuery();
                    if (cityEdited > 0)
                    {
                        Console.WriteLine("\nSuccess. By opdateret til {0}", city);
                    }
                } else
                {
                    Console.WriteLine("\nIndtast en gyldig adresse. Mindst 2 karakterer");
                }
                
            }
            else if (Program.valueToEdit == 5)
            {
                if (postalcode.ToString().Length == 4)
                {
                    int postalCodeEdited = editPostalCodeCmd.ExecuteNonQuery();
                    if (postalCodeEdited > 0)
                    {
                        Console.WriteLine("\nSuccess. Postnr opdateret til {0}", postalcode);
                    }
                } else
                {
                    Console.WriteLine("Indtast et gyldigt postnr. Skal være 4 tal");
                }
                
            } else
            {
                Console.WriteLine("Fejl i redigering");
            }
            #endregion




            conn.Close();

        }
    }
}
