using MyDBService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyDBService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        int CreateAccount(string username, string email, string contactno, string passwordhash, string passwordsalt, string usertype, string verificationcode, string accountstatus, string resetpasswordcode, DateTime expirycode);

        [OperationContract]
        int CreatePromotion(string name, string overview, string promotionimage, DateTime expirydate, double minimumspend, string code, string promotionstatus, int discount);

        [OperationContract]
        int CreatePayment(double cardnumber, int cvv, string date);

        [OperationContract]
        int CreateCart(int quantity, double totalprice, string username, string time, double price, string name, string desc, string image);
        [OperationContract]
        List<Account> GetAllAccount();

        [OperationContract]
        List<Account> GetAllDeletedAccount();

        [OperationContract]
        List<Account> GetAllStaffAccount();

        [OperationContract]
        List<Account> GetAllCustomerAccount();

        [OperationContract]
        List<Promotion> GetAllPromotion();

        [OperationContract]
        List<Promotion> GetAllPromotionsByPromotionStatus(string promotionstatus);

        [OperationContract]
        List<Promotion> GetAllAvailablePromotions();

        [OperationContract]
        List<Promotion> GetAllExpiredPromotion();

        [OperationContract]
        List<Activity> GetAllActivity();

        [OperationContract]
        List<Cart> GetAllCart(string username);
        [OperationContract]
        Account GetAccountByUsername(string username);

        [OperationContract]
        Account GetAccountByEmail(string email);

        [OperationContract]
        Account GetAccountByEmailAndUsername(string username, string email);

        [OperationContract]
        Account GetAccountDetail(string username);

        [OperationContract]
        Promotion GetPromotionByName(string name);
        // TODO: Add your service operations here

        [OperationContract]
        Activity SelectById(int id);

        [OperationContract]
        List<Activity> SelectBySearch(string word);

        [OperationContract]
        int UpdateAccountDetails(string username, string email, string contactno);

        [OperationContract]
        int UpdateEmail(string username, string email);

        [OperationContract]
        int UpdateUserTypeAndAccountStatus(string username, string usertype,string accountstatus);
            
        [OperationContract]
        int UpdateContact(string username, string contactno);

        [OperationContract]
        int UpdateAccountPassword(string username, string passwordhash);

        [OperationContract]
        int UpdateAccountPasswordByEmail(string email, string passwordhash);

        [OperationContract]
        int UpdateUserType(string username, string usertype);

        [OperationContract]
        int UpdateResetPasswordCode(string email, string resetpasswordcode);

        [OperationContract]
        int UpdateVerificationCode(string username, string verificationcode);

        [OperationContract]
        int UpdateExpiryCode(string username, DateTime expirycode);

        [OperationContract]
        int UpdateAccountStatus(string username, string accountstatus);

        [OperationContract]
        int UpdateCode(string name, string code);

        [OperationContract]
        int UpdatePromotionStatus(string name, string promotionstatus);


        [OperationContract]
        int UpdatePromotionStatusAndCode(string name, string code, string promotionstatus);

        [OperationContract]
        int UpdatePromotionDetails(string name, string overview, string promotionimage, DateTime expirydate, double minimumspend, string code, string promotionstatus, int discount);


        //Uwais Alqarni

        [OperationContract]
        List<Post> GetAllPost();

        [OperationContract]
        List<Post> GetAllPostStaff();

        [OperationContract]
        int CreatePost(string title, string image, string type, string location, string description, string username, string userReported, string bookmarkedBy);

        [OperationContract]
        int UpdatePost(int PostID, string title, string image, string type, string location, string description, Boolean bookmark);

        [OperationContract]
        Post GetAPost(int postID);

        [OperationContract]
        List<Post> GetPostByUsername(string username);

        [OperationContract]
        int DeletePost(int postID);

        [OperationContract]
        int UpdateBookmark(int postID, Boolean bookmark);

        [OperationContract]
        Boolean GetBookmark(int postID);

        [OperationContract]
        int UpdateReport(int postID, int report);

        [OperationContract]
        int GetReport(int postID);

        [OperationContract]
        int UpdateUserReported(int postID, string userReported);

        [OperationContract]
        string GetUserReported(int postID);

        [OperationContract]
        int UpdateBookmarkedBy(int postID, string bookmarkedBy);

        [OperationContract]
        string GetBookmarkedBy(int postID);

        //Yongsheng

        [OperationContract]
        int CreateActivity(string duration, double price, string details, string tag, string activityname, string image);

        [OperationContract]
        int UpdateActivity(int id, string duration, double price, string details, string tag, string activityname, string image);

        [OperationContract]
        int DeleteActivity(int id);
        [OperationContract]
        int DeleteCart(int id);
        [OperationContract]
        List<Activity> SelectByTag(string word);

        //Mengxi

        [OperationContract]
        int CreatePlan(string timecreated, string username, string planname);

        [OperationContract]
        List<Plan> GetPlanByUsername(string username);

        [OperationContract]
        int UpdatePlanname(int planid, string planname);

        [OperationContract]
        int DeletePlan(int planid);
        [OperationContract]
        int AddToPlan(string planid, string activityname, string date, string booked, string qty, double unitprice, double totalprice, string image);


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "MyDBService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
