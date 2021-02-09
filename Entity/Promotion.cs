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
        public string PromotionImage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double MinimumSpend { get; set; }
        public string Code { get; set; }
        public string PromotionStatus { get; set; }

        public Promotion(string name,string overview, string promotionimage, DateTime expirydate,double minimumspend,string code , string promotionstatus)
        {
            Name = name;
            Overview = overview;
            PromotionImage = promotionimage;
            ExpiryDate = expirydate;
            MinimumSpend = minimumspend;
            Code = code;
            PromotionStatus = promotionstatus;
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
            string sqlStmt = "INSERT INTO Promotion (Name, Overview, PromotionImage, ExpiryDate , MinimumSpend, Code , PromotionStatus) " +
                "VALUES (@paraName, @paraOverview, @paraPromotionImage, @paraExpiryDate, @paraMinimumSpend, @paraCode, @paraPromotionStatus)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraName", Name);
            sqlCmd.Parameters.AddWithValue("@paraOverview", Overview);
            sqlCmd.Parameters.AddWithValue("@paraPromotionImage", PromotionImage);
            sqlCmd.Parameters.AddWithValue("@paraExpiryDate", ExpiryDate);
            sqlCmd.Parameters.AddWithValue("@paraMinimumSpend", MinimumSpend);
            sqlCmd.Parameters.AddWithValue("@paraCode", Code);
            sqlCmd.Parameters.AddWithValue("@paraPromotionStatus", PromotionStatus);

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
                string promotionimage = row["PromotionImage"].ToString();
                DateTime expirydate = Convert.ToDateTime(row["ExpiryDate"].ToString());
                double minimumspend = Double.Parse(row["MinimumSpend"].ToString());
                string code = row["Code"].ToString();
                string promotionstatus = row["PromotionStatus"].ToString();
                pro = new Promotion(name, overview,promotionimage, expirydate, minimumspend,code,promotionstatus);
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
                string promotionimage = row["PromotionImage"].ToString();
                DateTime expirydate = Convert.ToDateTime(row["ExpiryDate"].ToString());
                double minimumspend = Double.Parse(row["MinimumSpend"].ToString());
                string code = row["Code"].ToString();
                string promotionstatus = row["PromotionStatus"].ToString();
                Promotion pro = new Promotion(name, overview,promotionimage, expirydate, minimumspend,code,promotionstatus);
                proList.Add(pro);
            }
            return proList;
        }

        public List<Promotion> SelectAllbyPromotionStatus(string promotionstatus)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Promotion where PromotionStatus=@paraPromotionStatus";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPromotionStatus", promotionstatus);

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
                string promotionimage = row["PromotionImage"].ToString();
                DateTime expirydate = Convert.ToDateTime(row["ExpiryDate"].ToString());
                double minimumspend = Double.Parse(row["MinimumSpend"].ToString());
                string code = row["Code"].ToString();
                Promotion pro = new Promotion(name, overview, promotionimage, expirydate, minimumspend, code, promotionstatus);
                proList.Add(pro);
            }
            return proList;
        }
        public int UpdateCode(string name, string code)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Promotion SET code = @paraCode where name =  @paraName";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraName", name);
            sqlCmd.Parameters.AddWithValue("@paraCode", code);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        public int UpdatePromotionStatus(string name, string promotionstatus)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Promotion SET PromotionStatus = @paraPromotionStatus where name =  @paraName";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraName", name);
            sqlCmd.Parameters.AddWithValue("@paraPromotionStatus", promotionstatus);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int UpdatePromotionStatusAndCode(string name, string code, string promotionstatus)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Promotion SET PromotionStatus = @paraPromotionStatus, Code = @paraCode where name =  @paraName";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraName", name);
            sqlCmd.Parameters.AddWithValue("@paraPromotionStatus", promotionstatus);
            sqlCmd.Parameters.AddWithValue("@paraCode", code);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
        public int UpdatePromotionDetails(string name, string overview, string promotionimage, DateTime expirydate, double minimumspend, string code, string promotionstatus)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Promotion SET Name= @paraName, Overview = @paraOverview, PromotionImage = @paraPromotionImage , ExpiryDate = @paraExpiryDate , MinimumSpend = @paraMinimumSpend , Code = @paraCode , PromotionStatus = @paraPromotionStatus where name =  @paraName";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraName", name);
            sqlCmd.Parameters.AddWithValue("@paraOverview", overview);
            sqlCmd.Parameters.AddWithValue("@paraPromotionImage", promotionimage);
            sqlCmd.Parameters.AddWithValue("@paraExpiryDate", expirydate);
            sqlCmd.Parameters.AddWithValue("@paraMinimumSpend", minimumspend);
            sqlCmd.Parameters.AddWithValue("@paraCode", code);
            sqlCmd.Parameters.AddWithValue("@paraPromotionStatus", promotionstatus);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
    }
}
