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
    public class Activity
    {
        public string Duration { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public string Tag { get; set; }
        public string ActivityName { get; set; }

        public Activity(string duration,double price,string details,string tag,string activityname)
        {
            Duration = duration;
            Price = price;
            Details = details;
            Tag = tag;
            ActivityName = activityname;

        }
        public Activity()
        {

        }
        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO Activity (Duration, Price, Details , Tag, ActivityName) " +
                "VALUES (@paraDuration, @paraPrice, @paraDetails, @paraTag, @paraActivityName)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraDuration", Duration);
            sqlCmd.Parameters.AddWithValue("@paraPrice", Price);
            sqlCmd.Parameters.AddWithValue("@paraDetails", Details);
            sqlCmd.Parameters.AddWithValue("@paraTag", Tag);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", ActivityName);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }

        public List<Activity> SelectAll()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Activity";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Activity> ActList = new List<Activity>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                int id = int.Parse(row["ActivityID"].ToString());
                string duration = row["Duration"].ToString();
                double price = Double.Parse(row["Price"].ToString());
                string details = row["Details"].ToString();
                string tag = row["Tag"].ToString();
                string name = row["ActivityName"].ToString();
                Activity Act = new Activity(duration, price, details, tag,name);
                ActList.Add(Act);
            }
            return ActList;
        }
        public Activity SelectByname(string activityname)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Activity where ActivityName=@paraActivityName";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraActivityName", activityname);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            Activity act = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                int id = int.Parse(row["ActivityID"].ToString());
                string duration = row["Duration"].ToString();
                double price = Double.Parse(row["Price"].ToString());
                string details = row["Details"].ToString();
                string tag = row["Tag"].ToString();
                string name = row["ActivityName"].ToString();
                act = new Activity(duration, price, details, tag, name);
            }
            return act;
        }
        public int UpdateActivity(string duration, double price, string details,string tag,string activityname)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Activity SET Duration = @paraDuration, Price = @paraPrice, Details = @paraDetails, Tag = @paraTag where ActivityName = @paraActivityName";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraDuration", duration);
            sqlCmd.Parameters.AddWithValue("@paraPrice", price);
            sqlCmd.Parameters.AddWithValue("@paraDetails", details);
            sqlCmd.Parameters.AddWithValue("@paraTag", tag);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", activityname);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
    }
}
