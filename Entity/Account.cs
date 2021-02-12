using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDBService.Entity
{
    public class Account
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string UserType { get; set; }
        public string VerificationCode { get; set; }
        public string AccountStatus { get; set; }
        public string ResetPasswordCode { get; set; }
        public DateTime ExpiryCode { get; set; }
        public Account(string username, string email, string contactno, string passwordhash, string passwordsalt, string usertype, string verificationcode, string accountstatus , string resetpasswordcode , DateTime expirycode)
        {
            Username = username;
            Email = email;
            ContactNo = contactno;
            PasswordHash = passwordhash;
            PasswordSalt = passwordsalt;
            UserType = usertype;
            VerificationCode = verificationcode;
            AccountStatus = accountstatus;
            ResetPasswordCode = resetpasswordcode;
            ExpiryCode = expirycode;
        }
        public Account()
        {

        }

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO Account (Username, Email, ContactNo, PasswordHash, PasswordSalt, UserType, VerificationCode , AccountStatus, ResetPasswordCode , ExpiryCode) " +
                "VALUES (@paraUsername, @paraEmail, @paraContactNo, @paraPasswordHash, @paraPasswordSalt, @paraUsertype , @paraVerificationCode , @paraAccountStatus , @paraResetPasswordCode , @paraExpiryCode)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraUsername", Username);
            sqlCmd.Parameters.AddWithValue("@paraEmail", Email);
            sqlCmd.Parameters.AddWithValue("@paraContactNo", ContactNo);
            sqlCmd.Parameters.AddWithValue("@paraPasswordHash", PasswordHash);
            sqlCmd.Parameters.AddWithValue("@paraPasswordSalt", PasswordSalt);
            sqlCmd.Parameters.AddWithValue("@paraUsertype", UserType);
            sqlCmd.Parameters.AddWithValue("@paraVerificationCode", VerificationCode);
            sqlCmd.Parameters.AddWithValue("@paraAccountStatus", AccountStatus);
            sqlCmd.Parameters.AddWithValue("@paraResetPasswordCode", ResetPasswordCode);
            sqlCmd.Parameters.AddWithValue("@paraExpiryCode", ExpiryCode);
            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public Account SelectByUsername(string username)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Account where Username=@paraUsername";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);    
            da.SelectCommand.Parameters.AddWithValue("@paraUsername",username);
                
            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Account act = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                act = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode , accountstatus , resetpasswordcode , expirycode) ;
            }
            return act;
        }

        public Account SelectByEmail(string email)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Account where Email=@paraEmail";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraEmail", email);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Account act = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string username = row["Username"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                act = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode , accountstatus , resetpasswordcode , expirycode);
            }
            return act;
        }

        public Account SelectByEmailAndUsername(string username,string email)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Account where Username<>@paraUsername AND Email=@paraEmail";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraEmail", email);
            da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);
            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Account act = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                act = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode, accountstatus, resetpasswordcode, expirycode);
            }
            return act;
        }

        public List<Account> SelectAll()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Account";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Account> actList = new List<Account>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string username = row["Username"].ToString();
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                Account obj = new Account(username, email, contactno, passwordhash, passwordsalt, usertype ,verificationcode,accountstatus, resetpasswordcode , expirycode);
                actList.Add(obj);
            }
            return actList;
        }

        public List<Account> SelectAllDeletedAccount()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Account where UserType='D'";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Account> actList = new List<Account>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string username = row["Username"].ToString();
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                Account obj = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode, accountstatus, resetpasswordcode, expirycode);
                actList.Add(obj);
            }
            return actList;
        }

        public List<Account> SelectAllStaffAccount()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Account where UserType='S'";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Account> actList = new List<Account>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string username = row["Username"].ToString();
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                Account obj = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode, accountstatus, resetpasswordcode, expirycode);
                actList.Add(obj);
            }
            return actList;
        }

        public List<Account> SelectAllCustomerAccount()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Account where UserType='C'";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Account> actList = new List<Account>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string username = row["Username"].ToString();
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                Account obj = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode, accountstatus, resetpasswordcode, expirycode);
                actList.Add(obj);
            }
            return actList;
        }

        public Account SelectAccountDetail(string username)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select Username,Email,ContactNo from Account";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Account act = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string email = row["Email"].ToString();
                string contactno = row["ContactNo"].ToString();
                string passwordhash = row["PasswordHash"].ToString();
                string passwordsalt = row["PasswordSalt"].ToString();
                string usertype = row["UserType"].ToString();
                string verificationcode = row["VerificationCode"].ToString();
                string accountstatus = row["AccountStatus"].ToString();
                string resetpasswordcode = row["ResetPasswordCode"].ToString();
                DateTime expirycode = Convert.ToDateTime(row["ExpiryCode"]);
                act = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode , accountstatus , resetpasswordcode , expirycode);
            }
            return act;
        }

        public int UpdateAccountDetails(string username, string email, string contact)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET email = @paraEmail, ContactNo= @paraContactNo where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            
            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraEmail", email);
            sqlCmd.Parameters.AddWithValue("@paraContactNo", contact);
           
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateEmail(string username, string email)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET email = @paraEmail where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraEmail", email);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateContact(string username, string contact)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET ContactNo= @paraContactNo where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraContactNo", contact);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdatePassword(string username, string passwordhash)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET passwordhash = @paraPasswordHash where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraPasswordHash", passwordhash);
            
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdatePasswordByEmail(string email , string passwordhash)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET passwordhash = @paraPasswordHash where email =  @paraEmail";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraEmail", email);
            sqlCmd.Parameters.AddWithValue("@paraPasswordHash", passwordhash);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateUserType(string username, string usertype)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET usertype = @paraUserType where username =  @paraUsername";
                        
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraUserType", usertype);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateUserTypeAndAccountStatus(string username, string usertype,string accountstatus)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET usertype = @paraUserType,AccountStatus= @paraAccountStatus where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraUserType", usertype);
            sqlCmd.Parameters.AddWithValue("@paraAccountStatus", accountstatus);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateExpiryCode(string username, DateTime expirycode)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET ExpiryCode = @paraExpiryCode where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraExpiryCode", expirycode);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateResetPasswordCode(string email, string resetpasswordcode)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET ResetPasswordCode = @paraResetPasswordCode where email =  @paraEmail";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraEmail", email);
            sqlCmd.Parameters.AddWithValue("@paraResetPasswordCode", resetpasswordcode);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateVerificationCode(string username, string verificationcode)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET VerificationCode = @paraVerificationCode where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraVerificationCode", verificationcode);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdateAccountStatus(string username, string accountstatus)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Account SET accountstatus = @paraAccountStatus where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraUsername", username);
            sqlCmd.Parameters.AddWithValue("@paraAccountStatus", accountstatus);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
    }
}
   