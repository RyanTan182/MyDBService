using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDBService.Entity
{
    public class Payment
    {
        public double CardNumber { get; set; }
        public int CVV { get; set; }
        public string Date { get; set; }

        public Payment(double cardnumber, int cvv, string date)
        {
            CardNumber = cardnumber;
            CVV = cvv;
            Date = date;

        }
        public Payment()
        {

        }
        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO Payment (CardNumber, CVV, CardExpiration) " +
                "VALUES (@paraCardNumber, @paraCVV, @paraCardExpiration)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraCardNumber", CardNumber);
            sqlCmd.Parameters.AddWithValue("@paraCVV", CVV);
            sqlCmd.Parameters.AddWithValue("@paraCardExpiration", Date);
            
            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
    }
}
