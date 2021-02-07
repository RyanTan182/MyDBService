using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDBService.Entity
{
    class Cart
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
            string sqlStmt = "INSERT INTO Activity (Quantity, TotalPrice , Username, Time, Price, Name, Desc, Image) " +
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

    }
}
