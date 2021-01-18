using MyDBService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public int CreateAccount(string username, string email, string contactno, string passwordhash, string passwordsalt, string usertype)
        {
            Account emp = new Account(username, email, contactno, passwordhash, passwordsalt, usertype);
            return emp.Insert();
        }
        public int CreatePromotion(string name, string overview, DateTime expirydate, double minimumspend)
        {
            Promotion emp = new Promotion(name, overview, expirydate, minimumspend);
            return emp.Insert();
        }
        public int CreateActivity(string duration, double price, string details, string tag, string activityname)
        {
            Activity emp = new Activity(duration, price, details, tag, activityname);
            return emp.Insert();
        }
        public int CreatePayment(double cardnumber,int cvv,string date)
        {
            Payment emp = new Payment(cardnumber, cvv, date);
            return emp.Insert();
        }
        public List<Account> GetAllAccount ()
        {
            Account act=new Account();
            return act.SelectAll();
        }
        public List<Promotion> GetAllPromotion()
        {
            Promotion pro = new Promotion();
            return pro.SelectAll();
        }
        public List<Activity> GetAllActivity()
        {
            Activity act = new Activity();
            return act.SelectAll();
        }
        public Account GetAccountByUsername(string username)
        {
            Account act = new Account();
            return act.SelectByUsername(username);
        }
        public Account GetAccountDetail(string username)
        {
            Account act = new Account();
            return act.SelectAccountDetail(username);
        }
        public Promotion GetPromotionByName(string name)
        {
            Promotion pro = new Promotion();
            return pro.SelectByName(name);
        }
        public int UpdateAccountDetails(string username, string email, string contactno)
        {
            Account act = new Account();
            return act.UpdateAccountDetails(username,email,contactno);
        }

        public int UpdateAccountPassword(string username, string passwordhash, string passwordsalt)
        {
            Account act = new Account();
            return act.UpdatePassword(username, passwordhash,passwordsalt);
        }
        public int UpdateUserType(string username, string usertype)
        {
            Account act = new Account();
            return act.UpdateUserType(username,usertype);
        }

        //Uwais Alqarni

        public List<Post> GetAllPost()
        {
            Post post = new Post();
            return post.SelectAll();
        }

        public int CreatePost(string title, Byte[] image, string type, string location, string description, string username)
        {
            Post post = new Post(title, image, type, location, description, 0, false, username);
            return post.Insert();
        }


    }
}
