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
    class Plan
    {
        public string Planid { get; set; }
        public string Timecreated { get; set; }
        public string Username { get; set; }
        public string Planname { get; set; }
        public Plan(string planid, string timecreated, string username, string planname)
        {
            Planid = planid;
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
            string sqlStmt = "INSERT INTO Plan (Planid, Timecreated, Userid, Planname) " +
                "VALUES (@paraPlanid, @paraTimecreated, @paraUserid, @paraPlanname)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraPlanid", Planid);
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

            string sqlStmt = "Delete From Plan where Planid = @paraid";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraid", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;

        }
        public int UpdatePlanname(string planid, string planname)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Plan SET planname = @paraPlanname where planid =  @paraPlanid";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPlanid", planid);
            sqlCmd.Parameters.AddWithValue("@paraPlanname", planname);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
    }
    
    
    
}
