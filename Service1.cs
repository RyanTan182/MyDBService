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
        public int CreateAccount(string username, string email, string contactno, string passwordhash, string passwordsalt, string usertype, string verificationcode, string accountstatus, string resetpasswordcode, DateTime expirycode)
        {
            Account emp = new Account(username, email, contactno, passwordhash, passwordsalt, usertype, verificationcode, accountstatus, resetpasswordcode, expirycode);
            return emp.Insert();
        }
        public int CreatePromotion(string name, string overview, string promotionimage, DateTime expirydate, double minimumspend, string code, string promotionstatus, int discount)
        {
            Promotion emp = new Promotion(name, overview, promotionimage, expirydate, minimumspend, code, promotionstatus , discount);
            return emp.Insert();
        }
        public int CreateActivity(string duration, double price, string details, string tag, string activityname, string image)
        {
            Activity emp = new Activity(duration, price, details, tag, activityname, image);
            return emp.Insert();
        }
        public int CreatePayment(double cardnumber, int cvv, string date)
        {
            Payment emp = new Payment(cardnumber, cvv, date);
            return emp.Insert();
        }
        public int CreateCart(int quantity, double totalprice, string username, string time, double price, string name, string desc, string image)
        {
            Cart emp = new Cart(quantity, totalprice, username, time, price, name, desc, image);
            return emp.Insert();
        }
        public List<Account> GetAllAccount()
        {
            Account act = new Account();
            return act.SelectAll();
        }
        public List<Promotion> GetAllPromotion()
        {
            Promotion pro = new Promotion();
            return pro.SelectAll();
        }

        public List<Promotion> GetAllPromotionsByPromotionStatus(string promotionstatus)
        {
            Promotion pro = new Promotion();
            return pro.SelectAllbyPromotionStatus(promotionstatus);
        }

        public List<Promotion> GetAllAvailablePromotions()
        {
            Promotion pro = new Promotion();
            return pro.SelectAllAvailablePromotion();
        }

        public List<Promotion> GetAllExpiredPromotion()
        {
            Promotion pro = new Promotion();
            return pro.SelectAllExpiredPromotion();
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

        public Account GetAccountByEmail(string email)
        {
            Account act = new Account();
            return act.SelectByEmail(email);
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
            return act.UpdateAccountDetails(username, email, contactno);
        }

        public int UpdateEmail(string username, string email)
        {
            Account act = new Account();
            return act.UpdateEmail(username, email);
        }

        public int UpdateContact(string username, string contactno)
        {
            Account act = new Account();
            return act.UpdateContact(username, contactno);
        }

        public int UpdateAccountPassword(string username, string passwordhash)
        {
            Account act = new Account();
            return act.UpdatePassword(username, passwordhash);
        }

        public int UpdateAccountPasswordByEmail(string email, string passwordhash)
        {
            Account act = new Account();
            return act.UpdatePasswordByEmail(email, passwordhash);
        }
        public int UpdateUserType(string username, string usertype)
        {
            Account act = new Account();
            return act.UpdateUserType(username, usertype);
        }

        public int UpdateResetPasswordCode(string email, string resetpasswordcode)
        {
            Account act = new Account();
            return act.UpdateResetPasswordCode(email, resetpasswordcode);
        }

        public int UpdateExpiryCode(string username, DateTime expirycode)
        {
            Account act = new Account();
            return act.UpdateExpiryCode(username, expirycode);
        }

        public int UpdateVerificationCode(string username, string verificationcode)
        {
            Account act = new Account();
            return act.UpdateVerificationCode(username, verificationcode);
        }

        public int UpdateAccountStatus(string username, string accountstatus)
        {
            Account act = new Account();
            return act.UpdateAccountStatus(username, accountstatus);
        }
        public int UpdateActivity(int id, string duration, double price, string details, string tag, string activityname, string image)
        {
            Activity act = new Activity();
            return act.UpdateActivity(id, duration, price, details, tag, activityname, image);
        }
        public Activity SelectById(int id)
        {
            Activity act = new Activity();
            return act.SelectById(id);
        }
        //public int deleteactivity(int id)
        //{
        //    activity act = new activity();
        //    return act.deleteactivity(id);
        //}
        public int UpdateCode(string name, string code)
        {
            Promotion pro = new Promotion();
            return pro.UpdateCode(name, code);
        }

        public int UpdatePromotionStatus(string name, string promotionstatus)
        {
            Promotion pro = new Promotion();
            return pro.UpdateCode(name, promotionstatus);
        }

        public int UpdatePromotionStatusAndCode(string name, string code, string promotionstatus)
        {
            Promotion pro = new Promotion();
            return pro.UpdatePromotionStatusAndCode(name, code, promotionstatus);
        }

        public int UpdatePromotionDetails(string name, string overview, string promotionimage, DateTime expirydate, double minimumspend, string code, string promotionstatus , int discount)
        {
            Promotion pro = new Promotion();
            return pro.UpdatePromotionDetails(name, overview, promotionimage, expirydate, minimumspend, code, promotionstatus , discount);
        }

        //Uwais Alqarni

        public List<Post> GetAllPost()
        {
            Post post = new Post();
            return post.SelectAll();
        }

        public int CreatePost(string title, string image, string type, string location, string description, string username)
        {
            Post post = new Post(title, image, type, location, description, 0, false, username);
            return post.Insert();
        }

        public int UpdatePost(int postID, string title, string image, string type, string location, string description, Boolean bookmark)
        {
            Post post = new Post();
            return post.UpdateAPost(postID, title, image, type, location, description, bookmark);
        }

        public Post GetAPost(int postID)
        {
            Post post = new Post();
            return post.GetAPost(postID);
        }

        public Post GetPostByUsername(string username)
        {
            Post post = new Post();
            return post.GetPostByUsername(username);
        }

        // Mengxi
        public int CreatePlan(string timecreated, string username, string planname)
        {
            Plan plan = new Plan(timecreated, username, planname);
            return plan.Insert();
        }
        public List<Plan> GetPlanByUsername(string username)
        {
            Plan act = new Plan();
            return act.SelectPlanByUsername(username);
        }
        public int UpdatePlanname(int planid, string planname)
        {
            Plan act = new Plan();
            return act.UpdatePlanname(planid, planname);
        }
        public int DeletePlan(int planid)
        {
            Plan act = new Plan();
            return act.DeletePlan(planid);
        }
    }
}