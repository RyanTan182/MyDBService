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
    public class Post
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Report { get; set; }
        public Boolean Bookmark { get; set; }
        public string Username { get; set; }

        public Post()
        {

        }

        public Post(string title, string image, string type, string location, string description, int report, Boolean bookmark, string username)
        {
            Title = title;
            Image = image;
            Type = type;
            Location = location;
            Description = description;
            Report = report;
            Bookmark = bookmark;
            Username = username;
        }

        public int Insert()
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "INSERT INTO Post (Title, Image, Type, Location, Description, Report, Bookmark, Username) " +
                "VALUES (@paraTitle, @paraImage, @paraType, @paraLocation, @paraDescription, @paraReport, @paraBookmark, @paraUsername)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraTitle", Title);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);
            sqlCmd.Parameters.AddWithValue("@paraType", Type);
            sqlCmd.Parameters.AddWithValue("@paraLocation", Location);
            sqlCmd.Parameters.AddWithValue("@paraDescription", Description);
            sqlCmd.Parameters.AddWithValue("@paraReport", Report);
            sqlCmd.Parameters.AddWithValue("@paraBookmark", Bookmark);
            sqlCmd.Parameters.AddWithValue("@paraUsername", Username);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public List<Post> SelectAll()
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection postConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select * from Post";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, postConn);

            DataSet ds = new DataSet();

            da.Fill(ds);

            List<Post> postList = new List<Post>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                string title = row["Title"].ToString();
                string image = row["Image"].ToString();
                string type = row["Type"].ToString();
                string location = row["Location"].ToString();
                string description = row["Description"].ToString();
                string str_report = row["Report"].ToString();
                int report = Convert.ToInt32(str_report);
                Boolean bookmark = Convert.ToBoolean(row["Bookmark"]);
                string username = row["Username"].ToString();

                Post obj = new Post(title, image, type, location, description, report, bookmark, username);
                postList.Add(obj);
            }
            return postList;
        }

        public int UpdatePost(string title, string image, string type, string location, string description, Boolean bookmark)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET title = @paraTitle, image= @paraImage, type= @paraType, location= @paraLocation, description= @paraDescription, bookmark= @paraBookmark where username =  @paraUsername";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraTitle", title);
            sqlCmd.Parameters.AddWithValue("@paraImage", image);
            sqlCmd.Parameters.AddWithValue("@paraType", type);
            sqlCmd.Parameters.AddWithValue("@paraLocation", location);
            sqlCmd.Parameters.AddWithValue("@paraDescription", description);
            sqlCmd.Parameters.AddWithValue("@paraBookmark", bookmark);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }
    }
}