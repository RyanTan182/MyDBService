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
    public class Cart
    {
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Username { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }

        public Cart(int quantity,double totalprice,string username,string time,double price,string name,string desc,string image)
        {
            
            Quantity = quantity;
            TotalPrice = totalprice;
            Username = username;
            Time = time;
            Price = price;
            Name = name;
            Desc = desc;
            Image = image;
        }
        public Cart()
        {

        }

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO Cart (Quantity, TotalPrice , Username, Time, price, Name, [Desc], Image) " +
                "VALUES (@paraQuantity, @paraTotalPrice, @paraUsername, @paraTime, @paraPrice, @paraName, @paraDesc, @paraImage)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            
            sqlCmd.Parameters.AddWithValue("@paraQuantity", Quantity);
            sqlCmd.Parameters.AddWithValue("@paraTotalPrice", TotalPrice);
            sqlCmd.Parameters.AddWithValue("@paraUsername", Username);
            sqlCmd.Parameters.AddWithValue("@paraTime", Time);
            sqlCmd.Parameters.AddWithValue("@paraPrice", Price);
            sqlCmd.Parameters.AddWithValue("@paraName", Name);
            sqlCmd.Parameters.AddWithValue("@paraDesc", Desc);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
        public List<Cart> SelectAllByName(string username)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from Cart where Username = @paraUsername";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);
            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<Cart> CartList = new List<Cart>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record

                int quantity = Convert.ToInt32(row["Quantity"].ToString());
                double totalprice = Double.Parse(row["TotalPrice"].ToString());
                string time = row["Time"].ToString();
                double price = Double.Parse(row["price"].ToString());
                string desc = row["Desc"].ToString();
                string user = row["Username"].ToString();
                string name = row["Name"].ToString();
                string image = row["Image"].ToString();
                Cart Car = new Cart(quantity,totalprice,user, time,price, name, desc, image);

                Car.CartID = Convert.ToInt32(row["CartID"]);
                CartList.Add(Car);
            }
            return CartList;
        }
        public int DeleteCart(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Delete From Cart where CartID = @paraid";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraid", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;

        }

    }
}
