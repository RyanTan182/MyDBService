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
    public class Plan
    {
        public int Planid { get; set; }
        public string Timecreated { get; set; }
        public string Username { get; set; }
        public string Planname { get; set; }

        public Plan() { }

        public Plan( string timecreated, string username, string planname)
        {
            Timecreated = timecreated;
            Username = username;
            Planname = planname;
        }
        

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO [Plan] (TimeCreated, Username, Planname) " +
                "VALUES (@paraTimecreated, @paraUserid, @paraPlanname)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraTimecreated", Timecreated);
            sqlCmd.Parameters.AddWithValue("@paraUserid", Username);
            sqlCmd.Parameters.AddWithValue("@paraPlanname", Planname);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public int DeletePlan(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Delete From [Plan] where Planid = @paraid";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraid", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;

        }
        public int UpdatePlanname(int planid, string planname)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE [Plan] SET planname = @paraPlanname where planid =  @paraPlanid";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPlanid", planid);
            sqlCmd.Parameters.AddWithValue("@paraPlanname", planname);
            


            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        //public Plan SelectPlanByUsername(string username)
        //{
        //    //Step 1 -  Define a connection to the database by getting
        //    //          the connection string from App.config
        //    string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
        //    SqlConnection myConn = new SqlConnection(DBConnect);

        //    //Step 2 -  Create a DataAdapter to retrieve data from the database table
        //    string sqlStmt = "Select * from [Plan] where Username=@paraUsername";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
        //    da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);

        //    //Step 3 -  Create a DataSet to store the data to be retrieved
        //    DataSet ds = new DataSet();

        //    //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
        //    da.Fill(ds);

        //    //Step 5 -  Read data from DataSet.
        //    Plan act = null;
        //    int rec_cnt = ds.Tables[0].Rows.Count;
        //    if (rec_cnt == 3)
        //    {
        //        DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
        //        string timecreated = row["TimeCreated"].ToString();
        //        string planname = row["Planname"].ToString();
        //        act = new Plan(timecreated, username, planname);
        //    }
        //    return act;
        //}
        public List<Plan> SelectPlanByUsername(string username)
        {
            System.Diagnostics.Debug.WriteLine("username: "+username);
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from [Plan] where Username=@paraUsername";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            List<Plan> planList = new List<Plan>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                
                string timecreated = row["TimeCreated"].ToString();
                string planname = row["Planname"].ToString();
                System.Diagnostics.Debug.WriteLine("timecreated: " + timecreated);
                System.Diagnostics.Debug.WriteLine("planname: " + planname);
                Plan obj = new Plan( timecreated, username, planname);

                obj.Planid = Convert.ToInt32(row["Planid"]);
                planList.Add(obj);
            }
            return planList;
        }
        //public List<Plan> SelectAll()
        //{
        //    //Step 1 -  Define a connection to the database by getting
        //    //          the connection string from App.config
        //    string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
        //    SqlConnection myConn = new SqlConnection(DBConnect);

        //    //Step 2 -  Create a DataAdapter object to retrieve data from the database table
        //    string sqlStmt = "Select * from [Plan]";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

        //    //Step 3 -  Create a DataSet to store the data to be retrieved
        //    DataSet ds = new DataSet();

        //    //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
        //    da.Fill(ds);

        //    //Step 5 -  Read data from DataSet to List
        //    List<Plan> planList = new List<Plan>();
        //    int rec_cnt = ds.Tables[0].Rows.Count;
        //    for (int i = 0; i < rec_cnt; i++)
        //    {
        //        DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
        //        string username = row["Username"].ToString();
        //         string timecreated = row["TimeCreated"].ToString();
        //        string planname = row["Planname"].ToString();
        //        Plan obj = new Plan(timecreated, username, planname);
        //        planList.Add(obj);
        //    }
        //    return planList;
        //}
    }
    
    
    
}
