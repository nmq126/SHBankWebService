using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SHBankWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string Login(string accountNumber, string code);

        [OperationContract]
        bool Transfer(string token, string receiverAccountNumber, double amount);

        [OperationContract]
        bool Deposit(string token, double amount);

        [OperationContract]
        bool Withdraw(string token, double amount);

        [OperationContract]
        List<TransactionHistory> FindTransactionHistoriesByToken(string token);

        [OperationContract]
        Account CheckValidAccount(string accountNumber);

        [OperationContract]
        Account CheckToken(string token);

        [OperationContract]
        string CreateToken(string accountNumber);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        private bool boolValue = true;
        private string stringValue = "Hello ";

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