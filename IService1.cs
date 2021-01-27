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
        int CreateAccount(string username, string email, string contactno, string passwordhash, string passwordsalt, string usertype);

        [OperationContract]
        int CreatePromotion(string name,string overview, string promotionimage, DateTime expirydate, double minimumspend, string code);

        [OperationContract]
        int CreatePayment(double cardnumber, int cvv, string date);
        [OperationContract]
        List<Account> GetAllAccount();

        [OperationContract]
        List<Promotion> GetAllPromotion();

        [OperationContract]
        List<Activity> GetAllActivity();

        [OperationContract]
        Account GetAccountByUsername(string username);

        [OperationContract]
        Account GetAccountByEmail(string email);

        [OperationContract]
        Account GetAccountDetail(string username);

        [OperationContract]
        Promotion GetPromotionByName(string name);
        // TODO: Add your service operations here

        [OperationContract]
        Activity SelectById(int id);

        [OperationContract]
        int UpdateAccountDetails(string username, string email, string contactno);

        [OperationContract]
        int UpdateAccountPassword(string username, string passwordhash, string passwordsalt);

        [OperationContract]
        int UpdateAccountPasswordByEmail(string email, string passwordhash, string passwordsalt);

        [OperationContract]
        int UpdateUserType(string username, string usertype);

        [OperationContract]
        int UpdateCode(string name, string code);

        //Uwais Alqarni

        [OperationContract]
        List<Post> GetAllPost();

        [OperationContract]
        int CreatePost(string title, string image, string type, string location, string description, string username);

        [OperationContract]
        int UpdatePost(int PostID, string title, string image, string type, string location, string description, Boolean bookmark);

        [OperationContract]
        Post GetAPost(int postID);

        [OperationContract]
        Post GetPostByUsername(string username);

        //Yongsheng

        [OperationContract]
        int CreateActivity(string duration, double price, string details, string tag, string activityname,string image);

        [OperationContract]
        int UpdateActivity(int id,string duration, double price, string details, string tag, string activityname,string image);

        [OperationContract]
        int DeleteActivity(int id);


        //Mengxi
        [OperationContract]
        int CreatePlan(string timecreated, string username, string planname);
        [OperationContract]
        List<Plan> GetPlanByUsername(string username);
        [OperationContract]
        int UpdatePlanname(int planid, string planname);
        [OperationContract]
        int DeletePlan(int planid);


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
