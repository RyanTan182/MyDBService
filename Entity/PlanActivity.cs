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
    class PlanActivity
    {
        public string Planid { get; set; }
        public string ActivityName { get; set; }
        public string Date { get; set; }
        public string Booked { get; set; }
        public string Qty { get; set; }
        public double Unitprice { get; set; }
        public double Totalprice { get; set; }
        public PlanActivity(string planid, string activityname, string date, string booked, string qty, double unitprice, double totalprice)
        {
            Planid = planid;
            ActivityName = activityname;
            Date = date;
            Booked = booked;
            Qty = qty;
            Unitprice = unitprice;
            Totalprice = totalprice;
        }
        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO PlanActivity (Planid, ActivityName, Date, Booked, Qty, Unitprice, Totalprice) " +
                "VALUES (@paraPlanid, @paraActivityName, @paraDate, @paraBooked, @paraQty, @paraUnitprice, @paraTotalprice)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraPlanid", Planid);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", ActivityName);
            sqlCmd.Parameters.AddWithValue("@paraDate", Date);
            sqlCmd.Parameters.AddWithValue("@paraBooked", Booked);
            sqlCmd.Parameters.AddWithValue("@paraQty", Qty);
            sqlCmd.Parameters.AddWithValue("@paraUnitprice", Unitprice);
            sqlCmd.Parameters.AddWithValue("@paraTotalprice", Totalprice);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public int UpdatePlan(string planid, string date)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Plan SET date = @paraDate where planid =  @paraPlanid";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPlanid", planid);
            sqlCmd.Parameters.AddWithValue("@paraPlanname", date);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        public int DeletePlanActivity(int id)
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
    }
}
