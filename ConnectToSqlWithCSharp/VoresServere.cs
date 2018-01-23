using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToSqlWithCSharp
{
    public class VoresServere
    {
        public static SqlConnection WhichServer(string Navn)
        {
            SqlConnection conn = new SqlConnection();
            if (Navn == "Virtus")
            {
                conn.ConnectionString =
                "Data Source=DESKTOP-TN867E5;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";
            }
            else if (Navn == "Bjarke")
            {
                conn.ConnectionString =
                "Data Source=EU300632;" +
                "Initial Catalog=H2_OOP_TEC_Banking_System;" +
                "Integrated Security=SSPI;";
            }
            else if (Navn == "Morten")
            {
                // Morten connection string
            }
            return conn;
        }

    }
}
