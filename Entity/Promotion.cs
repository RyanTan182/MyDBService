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
    public class Promotion
    {

        public string Name { get; set; }    
        public string Overview { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double MinimumSpend { get; set; }
        public Promotion(string name,string overview, DateTime expirydate,double minimumspend)
        {
            Name = name;
            Overview = overview;
            ExpiryDate = expirydate;
            MinimumSpend = minimumspend;
        }

        public Promotion() 
        {
        }

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO Promotion (Name, Overview, ExpiryDate , MinimumSpend) " +
                "VALUES (@paraName, @paraOverview, @paraExpiryDate, @paraMinimumSpend)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraName", Name);
            sqlCmd.Parameters.AddWithValue("@paraOverview", Overview);
            sqlCmd.Parameters.AddWithValue("@paraExpiryDate", ExpiryDate);
            sqlCmd.Parameters.AddWithValue("@paraMinimumSpend", MinimumSpend);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public Promotion SelectByName(string name)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Promotion where Name = @paraName";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraName", name);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Promotion pro = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string overview = row["Overview"].ToString();
                DateTime expirydate = Convert.ToDateTime(row["ExpiryDate"].ToString());
                double minimumspend = Double.Parse(row["MinimumSpend"].ToString());
                pro = new Promotion(name, overview, expirydate, minimumspend);
            }
            return pro;
        }
        public List<Promotion> SelectAll()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Promotion";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Promotion> proList = new List<Promotion>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                int id = int.Parse(row["PromotionID"].ToString());
                string name = row["Name"].ToString();
                string overview = row["Overview"].ToString();
                DateTime expirydate = Convert.ToDateTime(row["ExpiryDate"].ToString());
                double minimumspend = Double.Parse(row["MinimumSpend"].ToString());
                Promotion pro = new Promotion(name, overview, expirydate, minimumspend);
                proList.Add(pro);
            }
            return proList;
        }
    }
}
