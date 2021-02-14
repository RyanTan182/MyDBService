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
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Report { get; set; }
        public Boolean Bookmark { get; set; }
        public string Username { get; set; }
        public string UserReported { get; set; }
        public string BookmarkedBy { get; set; }

        public Post()
        {

        }

        public Post(string title, string image, string type, string location, string description, int report, Boolean bookmark, string username, string userReported, string bookmarkedBy)
        {
            Title = title;
            Image = image;
            Type = type;
            Location = location;
            Description = description;
            Report = report;
            Bookmark = bookmark;
            Username = username;
            UserReported = userReported;
            BookmarkedBy = bookmarkedBy;
        }

        public int Insert()
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "INSERT INTO Post (Title, Image, Type, Location, Description, Report, Bookmark, Username, UserReported, BookmarkedBy) " +
                "VALUES (@paraTitle, @paraImage, @paraType, @paraLocation, @paraDescription, @paraReport, @paraBookmark, @paraUsername, @paraUserReported, @paraBookmarkedBy)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraTitle", Title);
            sqlCmd.Parameters.AddWithValue("@paraImage", Image);
            sqlCmd.Parameters.AddWithValue("@paraType", Type);
            sqlCmd.Parameters.AddWithValue("@paraLocation", Location);
            sqlCmd.Parameters.AddWithValue("@paraDescription", Description);
            sqlCmd.Parameters.AddWithValue("@paraReport", Report);
            sqlCmd.Parameters.AddWithValue("@paraBookmark", Bookmark);
            sqlCmd.Parameters.AddWithValue("@paraUsername", Username);
            sqlCmd.Parameters.AddWithValue("@paraUserReported", UserReported);
            sqlCmd.Parameters.AddWithValue("@paraBookmarkedBy", BookmarkedBy);

            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public List<Post> SelectAll()
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection postConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select * from Post ORDER BY PostID DESC";
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
                string userReported = row["UserReported"].ToString();
                string bookmarkedBy = row["BookmarkedBy"].ToString();

                if (report < 5)
                {
                    Post obj = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);
                    obj.PostID = Convert.ToInt32(row["PostID"]);
                    postList.Add(obj);
                }
                else
                {
                    //Post obj = new Post(title, image, type, location, description, report, bookmark, username);
                    //obj.PostID = Convert.ToInt32(row["PostID"]);
                    //postList.Add(obj);
                }

                //Post obj = new Post(title, image, type, location, description, report, bookmark, username);
                //obj.PostID = Convert.ToInt32(row["PostID"]);
                //postList.Add(obj);
            }
            return postList;
        }

        public List<Post> SelectAllStaff()
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
                string userReported = row["UserReported"].ToString();
                string bookmarkedBy = row["BookmarkedBy"].ToString();

                Post obj = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);
                obj.PostID = Convert.ToInt32(row["PostID"]);
                postList.Add(obj);
            }
            return postList;
        }

        public int UpdateAPost(int postID, string title, string image, string type, string location, string description, Boolean bookmark)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET title = @paraTitle, image = @paraImage, type = @paraType, location = @paraLocation, description = @paraDescription, bookmark = @paraBookmark where postID =  @paraPostID";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPostID", postID);
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

        public Post GetAPost(int id)
        {           
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select * from Post where postID=@paraPostID";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPostID", id);

            DataSet ds = new DataSet();

            da.Fill(ds);

            Post post = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string title = row["Title"].ToString();
                string image = row["Image"].ToString();
                string type = row["Type"].ToString();
                string location = row["Location"].ToString();
                string description = row["Description"].ToString();
                string str_report = row["Report"].ToString();
                int report = Convert.ToInt32(str_report);
                Boolean bookmark = Convert.ToBoolean(row["Bookmark"]);
                string username = row["Username"].ToString();
                string userReported = row["UserReported"].ToString();
                string bookmarkedBy = row["BookmarkedBy"].ToString();

                post = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);
            }
            return post;
        }

        public List<Post> GetPostByUsername(string username)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select * from Post where Username=@paraUsername";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);

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
                string userReported = row["UserReported"].ToString();
                string bookmarkedBy = row["BookmarkedBy"].ToString();
                Post postItem = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);

                postItem.PostID = Convert.ToInt32(row["PostID"]);
                postList.Add(postItem);
            }
            return postList;
        }

        //public List<Post> GetPostByUsername(string username)
        //{
        //    string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
        //    SqlConnection myConn = new SqlConnection(DBConnect);

        //    string sqlStmt = "Select * from Post where Username=@paraUsername";
        //    SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
        //    da.SelectCommand.Parameters.AddWithValue("@paraUsername", username);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    List<Post> post = new List<Post>();
        //    int rec_cnt = ds.Tables[0].Rows.Count;
        //    for (int i = 0; i < rec_cnt; i++)
        //    {
        //        DataRow row = ds.Tables[0].Rows[i];
        //        string title = row["Title"].ToString();
        //        string image = row["Image"].ToString();
        //        string type = row["Type"].ToString();
        //        string location = row["Location"].ToString();
        //        string description = row["Description"].ToString();
        //        string str_report = row["Report"].ToString();
        //        int report = Convert.ToInt32(str_report);
        //        Boolean bookmark = Convert.ToBoolean(row["Bookmark"]);
        //        string userReported = row["UserReported"].ToString();
        //        string bookmarkedBy = row["BookmarkedBy"].ToString();
        //        Post postItem = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);

        //        postItem.PostID = Convert.ToInt32(row["PostID"]);
        //        post.Add(postItem);
        //    }
        //    return post;
        //}

        public int DeletePost(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Delete From Post where postID=@paraPostID";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraPostID", id);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();
            return result;

        }

        public int UpdateBookmark(int postID, Boolean bookmark)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET bookmark = @paraBookmark where postID =  @paraPostID";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPostID", postID);
            sqlCmd.Parameters.AddWithValue("@paraBookmark", bookmark);


            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public Boolean GetBookmark(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select Bookmark from Post where postID=@paraPostID";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPostID", id);

            DataSet ds = new DataSet();

            da.Fill(ds);

            Boolean bookmark = false;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                bookmark = Convert.ToBoolean(row["Bookmark"]);
            }
            return bookmark;
        }

        public int UpdateReport(int postID, int report)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET Report = @paraReport where postID =  @paraPostID";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPostID", postID);
            sqlCmd.Parameters.AddWithValue("@paraReport", report);


            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public int GetReport(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select Report from Post where postID=@paraPostID";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPostID", id);

            DataSet ds = new DataSet();

            da.Fill(ds);

            int report = 0;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                report = Convert.ToInt32(row["Report"]);
            }
            return report;
        }

        public int UpdateUserReported(int postID, string userReported)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET UserReported = @paraUserReported where postID =  @paraPostID";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            //string combindedString = string.Join(",", userReported);

            sqlCmd.Parameters.AddWithValue("@paraPostID", postID);
            sqlCmd.Parameters.AddWithValue("@paraUserReported", userReported);


            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public string GetUserReported(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select UserReported from Post where postID=@paraPostID";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPostID", id);

            DataSet ds = new DataSet();

            da.Fill(ds);

            string usersReported = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                usersReported = row["UserReported"].ToString();
            }
            return usersReported;
        }

        public int UpdateBookmarkedBy(int postID, string bookmarkedBy)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "UPDATE Post SET BookmarkedBy = @paraBookmarkedBy where postID =  @paraPostID";

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            sqlCmd.Parameters.AddWithValue("@paraPostID", postID);
            sqlCmd.Parameters.AddWithValue("@paraBookmarkedBy", bookmarkedBy);


            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            myConn.Close();

            return result;
        }

        public string GetBookmarkedBy(int id)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            string sqlStmt = "Select BookmarkedBy from Post where postID=@paraPostID";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraPostID", id);

            DataSet ds = new DataSet();

            da.Fill(ds);

            string bookmarkedBy = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                bookmarkedBy = row["BookmarkedBy"].ToString();
            }
            return bookmarkedBy;
        }

        public List<Post> SelectAllBookmark(string username)
        {
            string DBConnect = ConfigurationManager.ConnectionStrings["teenfun"].ConnectionString;
            SqlConnection postConn = new SqlConnection(DBConnect);


            string sqlStmt = "Select * from Post";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, postConn);

            DataSet ds = new DataSet();

            da.Fill(ds);

            List<Post> postList = new List<Post>();
            List<Post> bookmarkedList = new List<Post>();
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
                string userReported = row["UserReported"].ToString();
                string bookmarkedBy = row["BookmarkedBy"].ToString();

                Post obj = new Post(title, image, type, location, description, report, bookmark, username, userReported, bookmarkedBy);
                obj.PostID = Convert.ToInt32(row["PostID"]);
                postList.Add(obj);
            }

            foreach (Post item in postList.ToList())
            {
                if (item.BookmarkedBy.Contains(username))
                {
                    
                }
                else
                {
                    postList.Remove(item);
                }
            }

            return postList;
        }

    }
}