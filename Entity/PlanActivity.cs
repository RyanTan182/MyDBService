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
    public class PlanActivity
    {
        public int id { get; set; }
        public string ActivityName { get; set; }
        public string Date { get; set; }
        public string Booked { get; set; }
        public string Qty { get; set; }
        public double Unitprice { get; set; }
        public double Totalprice { get; set; }
        public string Planid { get; set; }
        public string Image { get; set; }
        public string Duration { get; set; }
        public string Desc { get; set; }
        public string Tag { get; set; }
        public PlanActivity(string planid, string activityname, string date, string booked, string qty, double unitprice, double totalprice, string image, string duration, string desc, string tag)
        {
            Planid = planid;
            ActivityName = activityname;
            Date = date;
            Booked = booked;
            Qty = qty;
            Unitprice = unitprice;
            Totalprice = totalprice;
            Image = image;
            Duration = duration;
            Desc = desc;
            Tag = tag;
        }
        public PlanActivity(){}

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO PlanActivity (ActivityName, Date, Booked, Qty, Unitprice, Totalprice, Planid, Image, Duration, [Desc], Tag) " +
                "VALUES (@paraActivityName, @paraDate, @paraBooked, @paraQty, @paraUnitprice, @paraTotalprice, @paraPlanid, @paraImage, @paraDuration, @paraDesc, @paraTag)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            
            sqlCmd.Parameters.AddWithValue("@paraActivityName", ActivityName);
            sqlCmd.Parameters.AddWithValue("@paraDate", Date);
            sqlCmd.Parameters.AddWithValue("@paraBooked", Booked);
            sqlCmd.Parameters.AddWithValue("@paraQty", Qty);
            sqlCmd.Parameters.AddWithValue("@paraUnitprice", Unitprice);
            sqlCmd.Parameters.AddWithValue("@paraTotalprice", Totalprice);
            sqlCmd.Parameters.AddWithValue("@paraPlanid", Planid);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);
            sqlCmd.Parameters.AddWithValue("@paraDuration", Duration);
            sqlCmd.Parameters.AddWithValue("@paraDesc", Desc);
            sqlCmd.Parameters.AddWithValue("@paraTag", Tag);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public List<PlanActivity> SelectActivityByPlanid(string planid)
        {
            System.Diagnostics.Debug.WriteLine("planid: " + planid);
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from PlanActivity where Planid=@paraPlanid";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPlanid", planid);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            List<PlanActivity> planactList = new List<PlanActivity>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record

                int iD = int.Parse(row["Planid"].ToString());
                string date = row["Date"].ToString();
                string actname = row["ActivityName"].ToString();
                string booked = row["Booked"].ToString();
                string qty = row["Qty"].ToString();
                double uprice = Double.Parse(row["Unitprice"].ToString());
                double tprice = Double.Parse(row["Totalprice"].ToString());
                string image = row["Image"].ToString();
                string duration = row["Duration"].ToString();
                string desc = row["Desc"].ToString();
                string tag = row["Tag"].ToString();

                PlanActivity obj = new PlanActivity(planid,actname, date, booked, qty, uprice, tprice, image, duration, desc, tag);

                obj.id = Convert.ToInt32(row["id"]);
                planactList.Add(obj);
            }
            return planactList;
        }
        public int UpdatePlanActivity(int id, string date, string qty,  double totalprice )
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE PlanActivity SET date = @paraDate, qty = @paraQty, totalprice = @paraTotalprice where id =  @paraid";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraid", id);
            sqlCmd.Parameters.AddWithValue("@paraDate", date);
            sqlCmd.Parameters.AddWithValue("@paraQty", qty);
            sqlCmd.Parameters.AddWithValue("@paraTotalprice", totalprice);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        public int DeletePlanActivity(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Delete From PlanActivity where id = @paraid";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraid", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;
        }
    }
}
