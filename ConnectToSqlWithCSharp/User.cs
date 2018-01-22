using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;

namespace ConnectToSqlWithCSharp
{
    public class User
    {
        private int userid;
        private string username;
        private string userpassword;

        #region getters & setters
        public int UserID
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }
        
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string UserPassword
        {
            get
            {
                return userpassword;
            }
            set
            {
                userpassword = value;
            }
        }
        #endregion

        public bool UserCheck()
        {

            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            conn.Open();
            SqlCommand checkExistence = new SqlCommand("SELECT COUNT(*) Username FROM Users WHERE Username = @uName", conn);
            checkExistence.Parameters.Add(new SqlParameter("@uName", username));

            int userExist = (int)checkExistence.ExecuteScalar();

            conn.Close();

            if (userExist != 1)
            {
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBruger eksisterer ikke\n");
                ResetColor();
                return false;
            } else
            {
                return true;
            }         
            
        }

        public bool UserAuth()
        {
            SqlConnection conn = VoresServere.WhichServer(Program.Navn);

            string encryptedPassword = Encryptor.MD5Hash(userpassword);

            conn.Open();
            SqlCommand checkPassword = new SqlCommand("SELECT COUNT(*) FROM Users WHERE UserPassword = @uPass AND Username = @uName ", conn);

            checkPassword.Parameters.Add(new SqlParameter("@uPass", encryptedPassword));
            checkPassword.Parameters.Add(new SqlParameter("@uName", username));

            int passwordCorrect = (int)checkPassword.ExecuteScalar();

            conn.Close();

            if (passwordCorrect != 1)
            {
                ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nForkert adgangskode");
                ResetColor();
                return false;
            } else
            {
                return true;
            }

        }

        

    }
}
