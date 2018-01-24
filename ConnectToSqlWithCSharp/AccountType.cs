using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToSqlWithCSharp
{
    public class AccountType
    {
        private int accounttypeid;
        private string accounttypename;
        private double interestrate;

        #region getters & setters
        public int AccountTypeID
        {
            get
            {
                return accounttypeid;
            }
            set
            {
                accounttypeid = value;
            }
        }

        public string AccountTypeName
        {
            get
            {
                return accounttypename;
            }
            set
            {
                accounttypename = value;
            }
        }

        public double InterestRate
        {
            get
            {
                return interestrate;
            }
            set
            {
                interestrate = value;
            }
        }
        #endregion

        public void AddAccountType()
        {
            //metode som tilføjer en ny kontotype
            //navn og rentesats skal indtastes

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            conn.Open();
            SqlCommand addAccType = new SqlCommand("INSERT INTO AccountTypes (AccountTypeName, InterestRate) VALUES (@accTypeName, @accTypeInterestRate)", conn);
            addAccType.Parameters.Add(new SqlParameter("@accTypeName", accounttypename));
            addAccType.Parameters.Add(new SqlParameter("@accTypeInterestRate", interestrate));

            addAccType.ExecuteNonQuery();

            Console.WriteLine("\nKontotype {0} med rentesats på {1:P} tilføjet", accounttypename, interestrate);

        }

    }


}
