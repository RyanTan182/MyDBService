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
        public string id { get; set; }
        public string ActivityName { get; set; }
        public string Date { get; set; }
        public string Booked { get; set; }
        public string Qty { get; set; }
        public double Unitprice { get; set; }
        public double Totalprice { get; set; }
        public string Planid { get; set; }
        public string Image { get; set; }
        public PlanActivity(string planid, string activityname, string date, string booked, string qty, double unitprice, double totalprice, string image)
        {
            Planid = planid;
            ActivityName = activityname;
            Date = date;
            Booked = booked;
            Qty = qty;
            Unitprice = unitprice;
            Totalprice = totalprice;
            Image = image;

        }
        public PlanActivity(){}

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO PlanActivity (Planid, ActivityName, Date, Booked, Qty, Unitprice, Totalprice, Image) " +
                "VALUES (@paraPlanid, @paraActivityName, @paraDate, @paraBooked, @paraQty, @paraUnitprice, @paraTotalprice @paraImage)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraPlanid", Planid);
            sqlCmd.Parameters.AddWithValue("@paraActivityName", ActivityName);
            sqlCmd.Parameters.AddWithValue("@paraDate", Date);
            sqlCmd.Parameters.AddWithValue("@paraBooked", Booked);
            sqlCmd.Parameters.AddWithValue("@paraQty", Qty);
            sqlCmd.Parameters.AddWithValue("@paraUnitprice", Unitprice);
            sqlCmd.Parameters.AddWithValue("@paraTotalprice", Totalprice);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        //public List<PlanActivity> SelectPlanByPlanid(string planid)
        //{
        //    System.Diagnostics.Debug.WriteLine("planid: " + planid);
        //    //Step 1 -  Define a connection to the database by getting
        //    //          the connection string from App.config
        //    string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
        //    SqlConnection myConn = new SqlConnection(DBConnect);

        //    //Step 2 -  Create a DataAdapter to retrieve data from the database table
        //    string sqlStmt = "Select * from PlanActivity where Planid=@paraPlanid";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
        //    da.SelectCommand.Parameters.AddWithValue("@paraPlanid", planid);

        //    //Step 3 -  Create a DataSet to store the data to be retrieved
        //    DataSet ds = new DataSet();

        //    //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
        //    da.Fill(ds);

        //    //Step 5 -  Read data from DataSet.
        //    List<PlanActivity> planactList = new List<PlanActivity>();
        //    int rec_cnt = ds.Tables[0].Rows.Count;
        //    for (int i = 0; i < rec_cnt; i++)
        //    {
        //        DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record

        //        int iD = int.Parse(row["Planid"].ToString());
        //        string date = row["Date"].ToString();
        //        double price = Double.Parse(row["Price"].ToString());
        //        string booked = row["Booked"].ToString();
        //        string qty = row["Qty"].ToString();
        //        double uprice = Double.Parse(row["Unitprice"].ToString());
        //        double tprice = Double.Parse(row["Total"].ToString());


        //        string image = row["Image"].ToString();
        //        string actname = row["ActiviyName"].ToString();
        //        PlanActivity obj = new PlanActivity( image, actname);

        //        obj.id = Convert.ToInt32(row["id"]);
        //        planactList.Add(obj);
        //    }
        //    return planactList;
        //}
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
