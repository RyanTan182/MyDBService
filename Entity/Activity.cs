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
        public int Actid { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public string Tag { get; set; }
        public string ActivityName { get; set; }

        public string Image { get; set; }

        public Activity(string duration,double price,string details,string tag,string activityname,string image)
        {
            Duration = duration;
            Price = price;
            Details = details;
            Tag = tag;
            ActivityName = activityname;
            Image = image;

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
            string sqlStmt = "INSERT INTO Activity (Duration, Price, Details , Tag, ActivityName,Image) " +
                "VALUES (@paraDuration, @paraPrice, @paraDetails, @paraTag, @paraActivityName, @paraImage)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraDuration", Duration);
            sqlCmd.Parameters.AddWithValue("@paraPrice", Price);
            sqlCmd.Parameters.AddWithValue("@paraDetails", Details);
            sqlCmd.Parameters.AddWithValue("@paraTag", Tag);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", ActivityName);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);

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
                
                string duration = row["Duration"].ToString();
                double price = Double.Parse(row["Price"].ToString());
                string details = row["Details"].ToString();
                string tag = row["Tag"].ToString();
                string name = row["ActivityName"].ToString();
                string image = row["Image"].ToString();
                Activity Act = new Activity(duration, price, details, tag, name,image);

                Act.Actid = Convert.ToInt32(row["ActivityID"]);
                ActList.Add(Act);
            }
            return ActList;
        }
        public Activity SelectById(int id)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from Activity where ActivityID=@paraid";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraid", id);

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
                int iD = int.Parse(row["ActivityID"].ToString());
                string duration = row["Duration"].ToString();
                double price = Double.Parse(row["Price"].ToString());
                string details = row["Details"].ToString();
                string tag = row["Tag"].ToString();
                string name = row["ActivityName"].ToString();
                string image = row["Image"].ToString();
                act = new Activity(duration, price, details, tag, name,image);
            }
            return act;
        }
        public int UpdateActivity(int id,string duration, double price, string details,string tag,string activityname,string image)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Activity SET Duration = @paraDuration, Price = @paraPrice, Details = @paraDetails, Tag = @paraTag, ActivityName = @paraActivityName,Image = @paraImage where ActivityID = @paraid " ;

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraid", id);
            sqlCmd.Parameters.AddWithValue("@paraDuration", duration);
            sqlCmd.Parameters.AddWithValue("@paraPrice", price);
            sqlCmd.Parameters.AddWithValue("@paraDetails", details);
            sqlCmd.Parameters.AddWithValue("@paraTag", tag);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", activityname);
            sqlCmd.Parameters.AddWithValue("@paraImage", image);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        public List<Activity> SelectBySearch(string word)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "select * from Activity where ActivityName like '" + word + "%'";
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

                string duration = row["Duration"].ToString();
                double price = Double.Parse(row["Price"].ToString());
                string details = row["Details"].ToString();
                string tag = row["Tag"].ToString();
                string name = row["ActivityName"].ToString();
                string image = row["Image"].ToString();
                Activity Act = new Activity(duration, price, details, tag, name, image);

                Act.Actid = Convert.ToInt32(row["ActivityID"]);
                ActList.Add(Act);
            }
            return ActList;
        }
        public int DeleteActivity(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Delete From Activity where ActivityID = @paraid";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraid", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;

        }

    }
}
